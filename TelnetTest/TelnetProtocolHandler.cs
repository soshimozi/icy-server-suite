using System;
using System.Collections.Generic;
using System.Text;
using SocketServer;

namespace TelnetTest
{
    class TelnetProtocolHandler : IProtocolHandler
    {
        #region IProtocolHandler Members

        public void Leave()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Enter()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
