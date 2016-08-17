using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    public class DataSocket : BaseSocket
    {
        public delegate void SocketDataReady(long descriptor, byte[] data);

        private const int ReceiveBufferSize = 1024 * 16;

        protected bool _connected;
        protected IPEndPoint _remoteInfo;

        protected long _bytesSent = 0;
        protected long _bytesReceived = 0;

        protected byte[] _recieveBuffer;

        public event SocketDataReady OnSocketDataReady;

        public DataSocket()
            : base()
        {
            _connected = false;
        }

        public DataSocket(Socket s)
            : base(s)
        {
            if (s != null)
            {
                _remoteInfo = (IPEndPoint)s.RemoteEndPoint;
                _connected = true;
            }
        }

        public IPAddress RemoteAddress
        {
            get
            {
                return _remoteInfo.Address;
            }
        }

        public int RemotePort
        {
            get
            {
                return _remoteInfo.Port;
            }
        }

        public bool Connected
        {
            get
            {
                return _connected;
            }
        }

        protected void FireOnSocketDataReady(long descriptor, byte[] data)
        {
            if (OnSocketDataReady != null)
            {
                OnSocketDataReady(descriptor, data);
            }
        }

        /// <summary>
        /// Connects this socket to another socket. This will fail
        /// if the socket is already connected, or the server
        /// rejects the connection.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public void Connect(IPAddress address, int port)
        {
            // if the socket is already connected...
            if (_connected)
            {
                throw new SocketLibException(ErrorCode.EAlreadyConnected);
            }

            // first try to obtain a socket descriptor from the OS, if
            // there isn't already one.
            if (_socket == null)
            {
                try
                {
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    _remoteInfo = new IPEndPoint(address, port);
                    _socket.Connect(_remoteInfo);
                    _connected = true;
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
        }

        /// <summary>
        /// Attempts to send data, and returns the number of
        /// of bytes sent
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public void Send(byte [] data)
        {
            ServerDataPacket packet = new ServerDataPacket(data, false);

            try
            {
                _socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(asynchronousSendCallback), packet);
            }
            catch
            {
                // HandleError
            }
        }

        private void asynchronousSendCallback(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                ServerDataPacket packet = (ServerDataPacket)result.AsyncState;
                try
                {
                    if (_socket.Connected)
                    {
                        int size = _socket.EndSend(result);

                        _bytesSent += size;
                        if (size != packet.Data.Length)
                        {
                            _socket.Close();
                            //FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
                        }
                        else
                        {
                            if (packet.NotifyComplete)
                            {
                                //FireOnSendComplete((long)dataSocket.Socket.Handle);
                            }
                        }
                    }
                }
                catch (ObjectDisposedException oex)
                {
                    //FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
                }
                catch (Exception ex)
                {
                    //HandleError(ex);
                    //FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
                }
            }
        }

        //public int Send(string data)
        //{
        //    int bytecount = 0;

        //    // make sure the socket is connected first.
        //    if (!_connected)
        //    {
        //        throw new SocketLibException(ErrorCode.ENotConnected);
        //    }

        //    try
        //    {
        //        bytecount = _socket.Send(ASCIIEncoding.ASCII.GetBytes(data));
        //    }
        //    catch (SocketException e)
        //    {
        //        ErrorCode error = SocketLibException.TranslateError(e.SocketErrorCode);
        //        if (error != ErrorCode.EOperationWouldBlock)
        //        {
        //            throw new SocketLibException(error);
        //        }
        //        else
        //        {
        //            // if the socket is nonblocking, we don't want to send a terminal
        //            // error, so just set the number of bytes sent to 0, assuming
        //            // that the client will be able to handle that.
        //            bytecount = 0;
        //        }
        //    }
        //    catch
        //    {
        //        throw new SocketLibException(ErrorCode.ESeriousError);
        //    }

        //    return bytecount;
        //}

        public void StartReceive()
        {
            _recieveBuffer = new byte[ReceiveBufferSize];
            _socket.BeginReceive(_recieveBuffer, 0, ReceiveBufferSize, SocketFlags.None, new AsyncCallback(receiveAsynchHandler), null);
        }

        private void receiveAsynchHandler(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                try
                {
                    int size = _socket.EndReceive(result);

                    _bytesReceived += size;
                    if (size > 0)
                    {
                        // let listeners know
                        FireOnSocketDataReady((int)_socket.Handle, (byte [])result.AsyncState);

                        // start the cycle anew
                        StartReceive();
                    }
                    else
                    {
                        // socket is closed or dead
                        //FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
                    }
                }
                catch
                {
                    //HandleError(ex);
                    //FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
                }
            }
        }

        public override void Close()
        {
            try
            {
                if (_connected)
                {
                    _socket.Shutdown(SocketShutdown.Both);
                }

                base.Close();
                _connected = false;
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
    }
}
