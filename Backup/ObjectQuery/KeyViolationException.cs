using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery
{
    public class KeyViolationException : System.Exception
    {
        public KeyViolationException(string message)
            : base(message)
        {
        }

        public KeyViolationException()
            : base()
        {
        }
    }
}
