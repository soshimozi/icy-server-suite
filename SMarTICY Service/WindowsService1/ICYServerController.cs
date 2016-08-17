using System;
using System.Collections.Generic;
using System.Text;

namespace ICYService
{
    public class ICYServerController
    {
        SMarTICY.ICYServer icyServer; // = new SMarTICY.ICYServer(false);
        public ICYServerController()
        {
            string configPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config\\server.xml";
            EventLog.WriteEntry("ICYServerController", configPath);
            if (File.Exists(configPath))
            {
                FileStream stream = new FileStream(configPath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream);

                XmlDocument document = new XmlDocument();
                string configXml = reader.ReadToEnd();

                int port = -1;
                string mountLocation = string.Empty;
                try
                {
                    document.LoadXml(configXml);

                    XmlNode node = document.SelectSingleNode("/configuration/port");
                    if (node != null)
                    {
                        string portText = node.InnerText;

                        if (int.TryParse(portText, out port))
                        {
                            node = document.SelectSingleNode("/configuration/mount_location");
                            if (node != null)
                            {
                                mountLocation = node.InnerText;
                            }
                        }
                    }
                }
                catch
                {
                }

                icyServer = new SMarTICY.ICYServer(mountLocation);
            }
        }

        public void StartServer(int listenPort)
        {
            if (!icyServer.IsRunning)
            {
                icyServer.StartServer(listenPort);
            }
        }

        public void StopServer()
        {
            if (icyServer.IsRunning)
            {
                icyServer.StopServer();
            }
        }
    }
}
