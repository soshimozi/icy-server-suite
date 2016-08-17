using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectEventQueue
{
    public class ObjectEventBase
    {
        protected string _eventName;

        public ObjectEventBase(string eventName)
        {
            _eventName = eventName;
        }

        public string EventName
        {
            get { return _eventName; }
        }

        internal virtual void TriggerEvent(Delegate d)
        { }
    }
}
