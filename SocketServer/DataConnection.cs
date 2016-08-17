using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using log4net;

namespace SocketServer
{
    public class DataConnection
    {
        static ILog logger = LogManager.GetLogger(typeof(DataConnection));

        //byte[] recieveBuffer;

        private DataSocket dataSocket = null;
        private Int64 id;
        private bool errorFlag = false;

        private const int MAX_FLOOD = 8000;

        public delegate void DisconnectedDelegate(Int64 identifier, Int64 bytesSent, Int64 bytesRecieved, TimeSpan connectedTime);
        public delegate void DataRecievedDelegate(Int64 identifier, byte[] data);
        public delegate void DataSendCompleteDelegate(Int64 identifier);

        public event DisconnectedDelegate OnDisconnected = null;
        public event DataRecievedDelegate OnDataRecieved = null;
        public event DataSendCompleteDelegate OnSendComplete = null;

        protected DataConnection() { }
        public DataConnection(Socket s, Int64 identifier)
        {
            this.dataSocket = new DataSocket(s);
            this.id = identifier;
        }

        //public Int64 BytesRecieved
        //{
        //    get { return _bytesRecieved; }
        //}

        //public Int64 BytesSent
        //{
        //    get { return _bytesSent; }
        //}

        // read as much data as possible
        public void Receive()
        {
            //recieveBuffer = new byte[RecieveBufferSize];
            dataSocket.Receive();

            //dataSocket.Socket.BeginReceive(recieveBuffer, 0, RecieveBufferSize, SocketFlags.None, new AsyncCallback(receiveAsynchHandler), null);
        }

        //private void receiveAsynchHandler(IAsyncResult result)
        //{
        //    if (result.IsCompleted)
        //    {
        //        try
        //        {
        //            int size = dataSocket.Socket.EndReceive(result);

        //            _bytesRecieved += size;
        //            if (size > 0)
        //            {
        //                // copy only bytes transported
        //                byte[] transportBuffer = new byte[size];
        //                Array.Copy(recieveBuffer, transportBuffer, size);

        //                // let listeners know
        //                FireOnDataRecieved((long)dataSocket.Socket.Handle, transportBuffer);

        //                // start the cycle anew
        //                recieveBuffer = new byte[RecieveBufferSize];
        //                dataSocket.Socket.BeginReceive(recieveBuffer, 0, RecieveBufferSize, SocketFlags.None, new AsyncCallback(receiveAsynchHandler), result.AsyncState);
        //            }
        //            else
        //            {
        //                // socket is closed or dead
        //                FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
        //            }
        //        }
        //        catch (ObjectDisposedException oex)
        //        {
        //            FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
        //        }
        //        catch (Exception ex)
        //        {
        //            HandleError(ex);
        //            FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
        //        }
        //    }
        //}

        private void FireOnDisconnected(long identifier, long bytesSent, long bytesRecieved, TimeSpan connectedTime)
        {
            if (OnDisconnected != null)
            {
                OnDisconnected.BeginInvoke(identifier, bytesSent, bytesRecieved, connectedTime, DisconnectAsynchCallback, null);
            }
        }

        private void DisconnectAsynchCallback(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                OnDisconnected.EndInvoke(result);
            }
        }

        private void FireOnDataRecieved(long identifier, byte[] data)
        {
            if (OnDataRecieved != null)
            {
                OnDataRecieved.BeginInvoke(identifier, data, DataRecievedAsynchCallback, null);
            }
        }

        private void DataRecievedAsynchCallback(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                OnDataRecieved.EndInvoke(result);
            }
        }

        private void FireOnSendComplete(long identifier)
        {
            if (OnSendComplete != null)
            {
                OnSendComplete.BeginInvoke(identifier, SendAsynchCallback, null);
            }
        }

        private void SendAsynchCallback(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                OnSendComplete.EndInvoke(result);
            }
        }

        public void CloseSocket()
        {
            dataSocket.Close();
        }

        public Int64 Id
        {
            get { return id; }
        }

        //public bool SendImmediate(byte[] data)
        //{
        //    if (errorFlag)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            dataSocket.Send(data);
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            HandleError(ex);
        //            return false;
        //        }
        //    }
        //}

        public bool SendAscynchronous(byte [] data, bool notify)
        {
            if (errorFlag)
            {
                return false;
            }
            else
            {
                try
                {
                    dataSocket.Send(data);
                    //dataSocket.Socket.BeginSend(packet.Data, 0, packet.Data.Length, SocketFlags.None, new AsyncCallback(asynchronousSendCallback), packet);
                    return true;
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                    return false;
                }
            }
        }

//dataSocket.Socket.BeginSend(packet.Data, 0, packet.Data.Length, SocketFlags.None, new AsyncCallback(asynchronousSendCallback), packet);
//        private void asynchronousSendCallback(IAsyncResult result)
//        {
//            if (result.IsCompleted && !errorFlag)
//            {
//                ServerDataPacket packet = (ServerDataPacket)result.AsyncState;
//                try
//                {
//                    if (dataSocket.Socket.Connected)
//                    {
//                        int size = dataSocket.Socket.EndSend(result);

//                        _bytesSent += size;
//                        if (size != packet.Data.Length)
//                        {
//                            errorFlag = true;
//                            logger.Error("Did not send expected number of bytes.  Closing socket");

//                            dataSocket.Close();
//                            FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
//                        }
//                        else
//                        {
//                            if (packet.NotifyComplete)
//                            {
//                                FireOnSendComplete((long)dataSocket.Socket.Handle);
//                            }
//                        }
//                    }
//                }
//                catch (ObjectDisposedException oex)
//                {
//                    FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
//                }
//                catch (Exception ex)
//                {
//                    HandleError(ex);
//                    FireOnDisconnected((long)dataSocket.Socket.Handle, _bytesSent, _bytesRecieved, DateTime.Now - _connectionTime);
//                }
//            }
//        }

        private void HandleError(Exception ex)
        {
            errorFlag = true;
            logger.Error(ex.Message);
        }
    }
}
