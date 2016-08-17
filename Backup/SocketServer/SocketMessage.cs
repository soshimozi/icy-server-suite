using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class SocketMessage // : IMessage
    {
        private static int next_sequence = 0;

        #region IMessage Members
        private int sequence = getNextSequence();

        public SocketMessage()
        { }

        static private int getNextSequence()
        {
            return next_sequence++;
        }

        public long MessageId { get; set; }

        public int SequenceNum { get { return sequence; } }

        public ServerEvent MessageCode { get; set; }

        public object MessageData { get; set; }

        #endregion
    }
}
