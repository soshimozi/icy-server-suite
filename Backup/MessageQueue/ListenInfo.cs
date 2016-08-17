using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectEventQueue
{
    class ListenInfo
    {
        public Guid ClientId
        {
            get;
            set;
        }

        public ObjectEventDelegate EventDelegate
        {
            get;
            set;
        }
    }

    class EventDescriptor
    {
        public Type ListenType
        {
            get;
            set;
        }

        public ObjectEventDelegate EventDelegate
        {
            get;
            set;
        }

    }
}
