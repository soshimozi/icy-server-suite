using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class ServerFloodException : System.Exception
    {
        System.Net.EndPoint ep;

        public ServerFloodException(System.Net.EndPoint ep)
        {
            this.ep = ep;
        }

        protected ServerFloodException() { }

        public string HostName
        {
            get { return ep.ToString(); }
        }

        public System.Net.EndPoint ClientRemoteAddress
        {
            get { return ep; }
        }
    }
}
