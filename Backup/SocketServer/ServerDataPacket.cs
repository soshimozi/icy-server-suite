using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class ServerDataPacket
    {
        private byte[] _data;
        private bool _notifyComplete;

        public ServerDataPacket(byte[] data)
        {
            _data = data;
        }

        public ServerDataPacket(bool notifyComplete)
        {
            _notifyComplete = notifyComplete;
        }

        public ServerDataPacket(byte[] data, bool notifyComplete)
        {
            _data = data;
            _notifyComplete = notifyComplete;
        }

        public bool NotifyComplete
        {
            get { return _notifyComplete; }
            set { _notifyComplete = value; }
        }

        public byte[] Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
