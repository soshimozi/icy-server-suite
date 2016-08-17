using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using ObjectEventQueue;

namespace SocketServer
{
    public class ListeningSocket : BaseSocket
    {
        private const int MaxBacklog = 8;

        private bool _listening;

        public bool Listening
        {
            get { return _listening; }
            set { _listening = value; }
        }

        public ListeningSocket()
            : base(null)
        {
            _listening = false;
        }

        /// <summary>
        /// Tell the socket to listen on a certain port.
        /// </summary>
        /// <param name="port">The port to listen on.</param>
        public void Listen(int port)
        {
            // first try to obtain a socket descriptor from the OS, if
            // there isn't already one.
            if (_socket == null)
            {
                try
                {
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                catch (SocketException e)
                {
                    throw new SocketLibException(e.SocketErrorCode);
                }
                catch
                {
                    throw new SocketLibException(ErrorCode.ESeriousError);
                }
            }

            var listenEndPoint = new IPEndPoint(IPAddress.Any, port);
            try
            {
                _socket.Bind(listenEndPoint);

                // now listen on the socket. There is a very high chance that this will
                // be successful if it got to this point, but always check for errors
                // anyway. Set the queue to 8; a reasonable number.
                _socket.Listen(MaxBacklog);
                _listening = true;
            }
            catch (SocketException e)
            {
                throw new SocketLibException(e.SocketErrorCode);
            }
            catch
            {
                throw new SocketLibException(ErrorCode.ESeriousError);
            }
        }

        /// <summary>
        /// This is a blocking function that will accept an 
        /// incomming connection and return a data socket with info
        /// about the new connection.
        /// </summary>
        /// <returns></returns>
        public void AcceptConnections()
        {
            _socket.BeginAccept(acceptAsynchHandler, null);
        }

        private void acceptAsynchHandler(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                try
                {
                    var dataSocket = new DataSocket(_socket.EndAccept(result));

                    // fire off event for new connection
                    //AsynchObjectEvent<SocketMessage> serverEvent = new AsynchObjectEvent<SocketMessage>();

                    var message =
                        new SocketMessage()
                        {
                            MessageCode = ServerEvent.SocketConnect,
                            MessageData = dataSocket,
                            MessageId = (long)_socket.Handle
                        };

                    // let our listeners know
                    AsynchEventManager.Instance.Publish(message, 0, DateTime.Now);
                }
                catch (Exception ex)
                {
                    //logger.Error(ex);
                }
            }

            // start accepting connections anew
            _socket.BeginAccept(acceptAsynchHandler, result.AsyncState);
        }

        public override void Close()
        {
            base.Close();

            _listening = false;
        }
    }
}
