using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using log4net;
using ObjectEventQueue;

namespace SocketServer
{
    public class ConnectionManager<TProtocol>
        where TProtocol : IProtocol, new()
    {
        static ILog logger = LogManager.GetLogger(typeof(ConnectionManager<TProtocol>));

        private Mutex _connectMapMutex = new Mutex();
        private Dictionary<Int64, ServerConnection<TProtocol>> _connectionMap = new Dictionary<long, ServerConnection<TProtocol>>();

        public ConnectionManager()
        {
            AsynchObjectEvent<SocketMessage> serverEvent = new AsynchObjectEvent<SocketMessage>();
            ObjectEventManager.SubscribeEvent<SocketMessage>(serverEvent, new ObjectEventDelegate<SocketMessage>(MessageHandler), null);
        }

        protected ServerConnection<TProtocol> FindConnection(long identifer)
        {
            if (_connectMapMutex.WaitOne())
            {
                try
                {
                    if (_connectionMap.ContainsKey(identifer))
                    {
                        return _connectionMap[identifer];
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    _connectMapMutex.ReleaseMutex();
                }
            }

            return null;
        }

        public void Send(long connectionId, byte [] data)
        {
            try
            {
                // find the identifer in the map
                DataSocket socket = FindConnection(connectionId);
                if (socket != null)
                {
                    socket.Send(data);
                }
                else
                {
                    logger.Error("Invalid identifier specified in SendData command.");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }

        private void removeConnectionFromMap(long id)
        {
            if (_connectMapMutex.WaitOne())
            {
                try
                {
                    if (_connectionMap.ContainsKey(id))
                    {
                        _connectionMap.Remove(id);
                    }
                }
                finally
                {
                    _connectMapMutex.ReleaseMutex();
                }
            }
        }

        private void HandleDisconnect(long identifier)
        {
            removeConnectionFromMap(identifier);
        }

        private void addToConnectionMap(DataSocket socket)
        {

            if (_connectMapMutex.WaitOne())
            {
                try
                {
                    // an old one must be around, sorry guy, gotta bootcha
                    if (_connectionMap.ContainsKey((long)socket.PlatformSocketHandle))
                    {
                        _connectionMap.Remove((long)socket.PlatformSocketHandle);
                    }

                    _connectionMap.Add((long)socket.PlatformSocketHandle, new ServerConnection<TProtocol>(socket.PlatformSocket));
                }
                finally
                {
                    _connectMapMutex.ReleaseMutex();
                }
            }
        }

        private void MessageHandler(string eventName, SocketMessage message)
        {
            DataSocket socket;
            switch (message.MessageCode)
            {
                case ServerEvent.SocketConnect:
                    socket = message.MessageData as DataSocket;
                    addToConnectionMap(socket);

                    // start receieving
                    socket.StartReceive();

                    ObjectEventManager.PublishEvent<SocketMessage>( new AsynchObjectEvent<SocketMessage>(),
                        new SocketMessage()
                        {
                            MessageId = message.MessageId
                            ,
                            MessageData = (message.MessageData as DataSocket).RemoteAddress.ToString()
                            ,
                            MessageCode = ServerEvent.ClientConnect
                        }
                    );
                            
                            
                    break;
                //case ServerEvent.OutgoingData:
                //    SendData(message.MessageId, (byte[])message.MessageData, true);
                //    break;

                case ServerEvent.SocketDisconnect:
                    HandleDisconnect(message.MessageId);

                    ObjectEventManager.PublishEvent<SocketMessage>(
                        new AsynchObjectEvent<SocketMessage>(),
                        new SocketMessage()
                        {
                            MessageId = message.MessageId
                            ,
                            MessageData = (message.MessageData as DataSocket).RemoteAddress.ToString()
                            ,
                            MessageCode = ServerEvent.ClientConnect
                        }
                    );

                    break;
            }
        }
    }
}
