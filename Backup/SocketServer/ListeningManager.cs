using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using ObjectEventQueue;

namespace SocketServer
{
    public class ListeningManager
    {
        protected const int MAX_BACKLOG = 8;

        protected List<ListeningSocket> _sockets = new List<ListeningSocket>();

        public ListeningManager()
        {
            AsynchObjectEvent<SocketMessage> serverEvent = new AsynchObjectEvent<SocketMessage>();
            ObjectEventManager.SubscribeEvent<SocketMessage>(serverEvent, new ObjectEventDelegate<SocketMessage>(MessageHandler), null);
        }

        ~ListeningManager()
        {
            foreach (ListeningSocket socket in _sockets)
            {
                socket.Close();
            }
        }

        protected void MessageHandler(string eventName, SocketMessage message)
        {
        }

        public void AddPort(int port)
        {
            // create a new socket
            ListeningSocket socket = new ListeningSocket();

            // listen on the requested port
            // add the socket to the socket vector
            _sockets.Add(socket);

            socket.Listen(port);

            // make the socket non-blocking, so that it won't block if a
            // connection exploit is used when accepting (see Chapter 4)
            socket.Blocking = false;

            socket.AcceptConnections();
            //socket.Socket.BeginAccept(new AsyncCallback(acceptAsynchHandler), socket);

            // add the socket descriptor to the set
            //_set.AddSocket(socket);
        }


        //public void Listen()
        //{
        //    // define a data socket that will receive connections from the listening
        //    // sockets
        //    DataSocket datasock = new DataSocket();

        //    // detect if any sockets have action on them
        //    if (_set.Poll() > 0)
        //    {
        //        // loop through every listening socket
        //        foreach (ListeningSocket socket in _sockets)
        //        {
        //            // check to see if the current socket has a connection waiting
        //            if (_set.HasActivity(socket))
        //            {
        //                try
        //                {
        //                    // accept the connection
        //                    datasock = socket.Accept();

        //                    // run the action function on the new data socket
        //                    _manager.NewConnection(datasock);
        //                }

        //                // catch any exceptions, and rethrow it if it isn't
        //                // EWOULDBLOCK. This is done because of a connection
        //                // exploit that is possible, by causing a socket to
        //                // detect a connection, but then not be able to retrieve
        //                // the connection once it gets to the accept call.
        //                // So, if the connection would block, this just ignores
        //                // it, but if any other error occurs, it is rethrown.
        //                catch (SocketLibException e)
        //                {

        //                    if (e.ErrorCode != ErrorCode.EOperationWouldBlock)
        //                    {
        //                        throw e;
        //                    }
        //                }
        //            }   // end activity check
        //        }   // end socket loop
        //    }   // end check for number of active sockets
        //}
    }
}
