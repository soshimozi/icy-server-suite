using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectEventQueue
{
    internal class PriorityMessage<T> : MessageBase
    {

        public PriorityMessage(string eventName)
        {
            EventName = eventName;
        }

        public PriorityMessage()
        { 
        }

        public new T Message
        {
            get;
            set;
        }

        public DateTime SendTime
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }
    }
}
