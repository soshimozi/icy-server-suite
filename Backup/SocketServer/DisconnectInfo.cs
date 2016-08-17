using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class DisconnectInfo
    {
        Int64 _socketId;
        string _remoteAddress;
        Int64 _bytesReceived;
        Int64 _bytesSent;
        TimeSpan _connectedTime;

        public DisconnectInfo(Int64 socketId, Int64 bytesSent, Int64 bytesReceived, string remoteAddress, TimeSpan connectedTime)
        {
            _socketId = socketId;
            _bytesReceived = bytesReceived;
            _bytesSent = bytesSent;
            _remoteAddress = remoteAddress;
            _connectedTime = connectedTime;
        }

        public TimeSpan ConnectedTime
        {
            get { return _connectedTime; }
            set { _connectedTime = value; }
        }

        public Int64 BytesSent
        {
            get { return _bytesSent; }
            set { _bytesSent = value; }
        }

        public Int64 BytesReceived
        {
            get { return _bytesReceived; }
            set { _bytesReceived = value; }
        }

        public string RemoteAddress
        {
            get { return _remoteAddress; }
            set { _remoteAddress = value; }
        }

        public Int64 SocketId
        {
            get { return _socketId; }
            set { _socketId = value; }
        }
    }
}
