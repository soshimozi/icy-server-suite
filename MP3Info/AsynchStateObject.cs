using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MP3Info
{
    public class AsynchStateObject
    {
        FileStream stream;
        object state;
        public const int HeaderSize = 128;

        public object State
        {
            get { return state; }
            set { state = value; }
        }

        public FileStream Stream
        {
            get { return stream; }
            set { stream = value; }
        }

        byte[] headerBuffer = new byte[HeaderSize];

        public byte[] HeaderBuffer
        {
            get { return headerBuffer; }
        }
    }
}
