using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Info
{
    public class DecodeEventArgs
    {
        object state;
        ID3v1Header header;

        public ID3v1Header Header
        {
            get { return header; }
            set { header = value; }
        }

        public DecodeEventArgs() { }
        public DecodeEventArgs(ID3v1Header header, object state)
        {
            this.header = header;
            this.state = state;
        }

        public object UserState
        {
            get { return state; }
            set { state = value; }
        }

        
    }
}
