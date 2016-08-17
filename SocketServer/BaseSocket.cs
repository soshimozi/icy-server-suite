using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    public class BaseSocket
    {
        private IPEndPoint _localInfo;
        private bool _bIsBlocking;

        protected Socket _socket;

        protected BaseSocket()
        {
            _socket = null;
        }

        protected BaseSocket(Socket s)
        {
            _socket = s;

            if (s != null)
            {
                _localInfo = (IPEndPoint)s.LocalEndPoint;

                // the socket is blocking by default
                _bIsBlocking = s.Blocking;
            }
        }

        public Socket PlatformSocket
        {
            get
            {
                return _socket;
            }
        }

        public IntPtr PlatformSocketHandle
        {
            get
            {
                return _socket.Handle;
            }
        }

        /// <summary>
        /// Gets the local port of the socket
        /// </summary>
        public int LocalPort
        {
            get
            {
                return _localInfo.Port;
            }
        }

        public IPAddress LocalAddress
        {
            get
            {
                return _localInfo.Address;
            }
        }

        public bool Blocking
        {
            get
            {
                return _socket.Blocking;
            }

            set
            {
                _socket.Blocking = value;
            }
        }

        public virtual void Close()
        {
            _socket.Close();
            _socket = null;
        }
    }
}
