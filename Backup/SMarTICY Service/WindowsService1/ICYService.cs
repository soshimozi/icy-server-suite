using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Xml;
using System.Reflection;

namespace ICYService
{
    public partial class ICYService : ServiceBase
    {
        public ICYService()
        {
            InitializeComponent();
        }

        SMarTICY.ICYServer icyServer; // = new SMarTICY.ICYServer(false);

        string mountLocation = string.Empty;
        int port = 0;

        protected override void OnStart(string[] args)
        {
            if (!ReadConfig())
            {
                ExitCode = 87;
                Stop();
            }
            else
            {
                icyServer = new SMarTICY.ICYServer(mountLocation);
                try
                {
                    icyServer.StartServer(port);
                    EventLog.WriteEntry("ICYService", string.Format("Server started and listening on port:{0}, mounted on {1}", port, mountLocation));
                }
                catch (Exception ex)
                {
                    icyServer = null;

                    EventLog.WriteEntry("ICYService", string.Format("Server failed to start: {0}", ex.Message));
                    Stop();
                }
            }
        }

        private bool ReadConfig()
        {
            bool status = false;

            string configPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config\\server.xml";
            EventLog.WriteEntry("ICYServer", configPath);
            if (File.Exists(configPath))
            {
                FileStream stream = new FileStream(configPath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                XmlDocument document = new XmlDocument();
                string configXml = reader.ReadToEnd();
                reader.Close();

                int parsedPort = -1;
                try
                {
                    document.LoadXml(configXml);

                    XmlNode node = document.SelectSingleNode("/configuration/port");
                    if (node != null)
                    {
                        string portText = node.InnerText;
                        if (int.TryParse(portText, out parsedPort))
                        {
                            port = parsedPort;

                            node = document.SelectSingleNode("/configuration/mount_location");
                            if (node != null)
                            {
                                mountLocation = node.InnerText;
                                status = true;
                            }
                        }
                    }
                }
                catch
                {
                    EventLog.WriteEntry("ICYService", "Invalid config file.");
                }
            }
            else
            {
                EventLog.WriteEntry("ICYService", "Missing config file.");
            }

            return status;
        }

        Dictionary<string, string> getArgumentList(string[] arguments)
        {
            Dictionary<string, string> argumentList = new Dictionary<string, string>();
            for (int i = 0; i < arguments.Length; i++)
            {
                string[] nameValue = arguments[i].Split(":".ToCharArray());
                if (nameValue.Length == 2)
                {
                    argumentList.Add(nameValue[0], nameValue[1]);
                }
            }

            return argumentList;
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            if (icyServer != null)
            {
                icyServer.StopServer();
                icyServer = null;
            }
        }
    }
}
