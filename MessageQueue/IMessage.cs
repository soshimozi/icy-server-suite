using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectMessageQueue
{
    public interface IMessage
    {
        Int64 MessageId
        {
            get;
            set;
        }

        int SequenceNum
        {
            get;
            set;
        }

        int MessageCode
        {
            get;
            set;
        }

        object MessageData
        {
            get;
            set;
        }
    }
}
