using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery
{
    public class KeyNotFoundException : System.Exception
    {
        public KeyNotFoundException(string message)
            : base(message)
        {
        }

        public KeyNotFoundException() 
            : base()
        {
        }
    }
}
