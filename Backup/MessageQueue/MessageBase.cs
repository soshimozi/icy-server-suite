using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectEventQueue
{
    public class MessageBase
    {
        //public string EventName
        //{
        //    get;
        //    set;
        //}

        public Type PayloadType
        {
            get;
            set;
        }

        public object Payload
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }

        public DateTime SendTime
        {
            get;
            set;
        }

        public MessageBase()
        { 
        }
    }
}
