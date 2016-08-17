using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public interface IProtocolHandler
    {
        void Leave();
        void Enter();
    }
}
