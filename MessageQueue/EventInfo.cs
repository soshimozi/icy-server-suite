using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectEventQueue
{
    internal class EventInfo
    {
        private Delegate _eventDelegate;
        private object _eventKey;

        public Delegate EventDelegate
        {
            get { return _eventDelegate; }
            set { _eventDelegate = value; }
        }

        public object EventKey
        {
            get { return _eventKey; }
            set { _eventKey = value; }
        }
    }
}
