using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SMarTICY
{
    class Request
    {
        private string action; // i.e. GET, POST
        private Dictionary<string, List<string>> headerValues = new Dictionary<string, List<string>>();
        private string path;
        string[] arguments;
        string protocol = string.Empty;

        public string Protocol
        {
            get { return protocol; }
            set { protocol = value; }
        }


        public Request()
        {
        }

        public string[] Arguments
        {
            get { return arguments; }
            set { arguments = value; }
        }

        public string GetServerPath(string serverBasePath)
        {
            StringBuilder filePathBuilder = new StringBuilder();
            filePathBuilder.AppendFormat("{0}{1}", serverBasePath, path.Replace("/", "\\"));

            if (string.IsNullOrEmpty(System.IO.Path.GetExtension(filePathBuilder.ToString())))
            {
                filePathBuilder.Append(".mp3");
            }
            return filePathBuilder.ToString();
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }

        public Dictionary<string, List<string>> HeaderValues
        {
            get { return headerValues; }
        }

        public string Action
        {
            get { return action; }
            set { action = value; }
        }
    }
}
