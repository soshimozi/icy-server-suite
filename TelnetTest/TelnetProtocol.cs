using System;
using System.Collections.Generic;
using System.Text;
using SocketServer;

namespace TelnetTest
{
    public class TelnetProtocol : IProtocol
    {
        #region IProtocol Members

        public byte[] Translate(byte[] data)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
