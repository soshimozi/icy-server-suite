using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public enum ServerEvent
    {
        SocketConnect,
        SocketDisconnect,
        IncommingData,
        OutgoingData,
        TransmitComplete,
        ClientConnect,
        ClientDisconnect
    }

}
