using System;
using System.Collections.Generic;
using System.Text;

namespace ICYServer_Stress_Test_Harness
{
    class RequestArguments
    {
        private long _bytesSent;
        private long _bytesReceived;
        private TimeSpan _requestTime;

        public TimeSpan RequestTime
        {
            get { return _requestTime; }
            set { _requestTime = value; }
        }

        public long BytesReceived
        {
            get { return _bytesReceived; }
            set { _bytesReceived = value; }
        }

        public long BytesSent
        {
            get { return _bytesSent; }
            set { _bytesSent = value; }
        }
    }
}
