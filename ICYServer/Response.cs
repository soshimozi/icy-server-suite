using System;
using System.Collections.Generic;
using System.Text;

namespace SMarTICY
{
    public class Response
    {
        string streamName;
        string streamUrl;

        public string StreamUrl
        {
            get { return streamUrl; }
            set { streamUrl = value; }
        }

        public string StreamName
        {
            get { return streamName; }
            set { streamName = value; }
        }
    }
}
