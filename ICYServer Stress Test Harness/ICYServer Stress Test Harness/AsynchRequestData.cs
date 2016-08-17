using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ICYServer_Stress_Test_Harness
{
    class AsynchRequestData
    {
        private Semaphore _resourceBlocker;
        private DateTime _requestStartTime;
        private int _threadId;

        public int ThreadId
        {
            get { return _threadId; }
            set { _threadId = value; }
        }

        public DateTime RequestStartTime
        {
            get { return _requestStartTime; }
            set { _requestStartTime = value; }
        }

        public Semaphore ResourceBlocker
        {
            get { return _resourceBlocker; }
            set { _resourceBlocker = value; }
        }
    }
}
