using System;
using System.Collections.Generic;
using System.Text;
//using SocketServer;

namespace ObjectMessageQueue
{
    public class Listener
    {
        private ObjectMessageManager.MessageReceivedEventHandler _handler = null;
        bool _queueMessage;

        public bool QueueMessage
        {
            get { return _queueMessage; }
            set { _queueMessage = value; }
        }

        public ObjectMessageManager.MessageReceivedEventHandler MessageEvent
        {
            get { return _handler; }
            set { _handler = value; }
        }

    }
}
