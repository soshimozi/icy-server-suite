using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public interface IProtocol
    {
        byte[] Translate(byte[] data);
    }
}
