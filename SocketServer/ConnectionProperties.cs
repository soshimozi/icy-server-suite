using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class ConnectionProperties
    {
        string _remoteAddress;
        Int64 _descriptorId;

        public ConnectionProperties(Int64 descriptorId, string remoteAddress)
        {
            _remoteAddress = remoteAddress;
            _descriptorId = descriptorId;
        }

        public Int64 DescriptorId
        {
            get { return _descriptorId; }
            set { _descriptorId = value; }
        }

        public string RemoteAddress
        {
            get { return _remoteAddress; }
            set { _remoteAddress = value; }
        }

    }
}
