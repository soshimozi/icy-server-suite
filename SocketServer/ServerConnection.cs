using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using ObjectEventQueue;

namespace SocketServer
{
    public class ServerConnection<TProtocol> : DataSocket
        where TProtocol : IProtocol, new()
    {
        protected const int BUFFERSIZE = 1024;

        protected Int64 _lastSendTime;
        protected Int64 _lastReceiveTime;
        protected Int64 _creationTime;

        protected TProtocol _protocol;
        protected Stack<IProtocolHandler> _handlers = new Stack<IProtocolHandler>();
        protected StringBuilder _sendbuffer = new StringBuilder();

        protected int _datarate;
        protected int _lastdatarate;

        protected bool _closed;
        protected bool _checksendtime;

        byte [] _buffer = new byte[BUFFERSIZE];

        public ServerConnection()
            : base()
        {
            Initialize();
        }

        public ServerConnection(Socket s)
            : base(s)
        {
            Initialize();
        }

        public override void Close()
        {
            _closed = false;
        }

        public void CloseSocket()
        {
            base.Close();

            // remove all handlers
            ClearHandlers();
        }

        public bool Closed
        {
            get
            {
                return _closed;
            }
        }

        public TProtocol Protocol
        {
            get
            {
                return _protocol;
            }
        }

        public void SwitchHandler(IProtocolHandler handler)
        {
            if (CurrentHandler != null)
            {
                CurrentHandler.Leave();
                _handlers.Pop();
            }

            _handlers.Push(handler);
            handler.Enter();
        }

        public void AddHandler(IProtocolHandler handler)
        {
            if (CurrentHandler != null)
            {
                CurrentHandler.Leave();
            }

            _handlers.Push(handler);
            handler.Enter();
        }

        public void RemoveCurrentHandler()
        {
            _handlers.Peek().Leave();
            _handlers.Pop();

            if (CurrentHandler != null)
            {
                CurrentHandler.Enter();
            }
        }

        public IProtocolHandler CurrentHandler
        {
            get
            {
                if (_handlers.Count > 0)
                {
                    return _handlers.Peek();
                }
                else
                {
                    return null;
                }
            }
        }

        public void ClearHandlers()
        {
            if (CurrentHandler != null)
            {
                CurrentHandler.Leave();
            }

            _handlers.Clear();
        }

        protected void Initialize()
        {
            _protocol = new TProtocol();

            _datarate = 0;
            _lastdatarate = 0;
            _lastReceiveTime = 0;
            _lastSendTime = 0;
            _checksendtime = false;
            _creationTime = DateTime.Now.Ticks;
            _closed = false;

            ObjectEventManager.SubscribeEvent<SocketMessage>(
                new AsynchObjectEvent<SocketMessage>(),
                OnSocketMessage, null);
        }

        public Int64 LastSendTime
        {
            get
            {
                return _lastSendTime;
            }
        }

        public void BufferData(byte[] buffer)
        {
            _sendbuffer.Append(ASCIIEncoding.ASCII.GetString(buffer));
        }

        public void SendBuffer()
        {
            if (_sendbuffer.Length > 0)
            {
                Send(ASCIIEncoding.ASCII.GetBytes(_sendbuffer.ToString()));
                _lastSendTime = DateTime.Now.Ticks;
            }
        }

        private void OnSocketMessage(string eventName, SocketMessage message)
        {
            switch (message.MessageCode)
            {
                case ServerEvent.IncommingData:
                    OnReceive(message.MessageData as byte[]);
                    break;
            }
        }

        public void OnReceive(byte [] data)
        {
            //data = _protocol.Translate(data);

            // echo server
            Send(data);
            SendBuffer();
        }
    }
}
