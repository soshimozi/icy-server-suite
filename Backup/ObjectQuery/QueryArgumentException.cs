using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery
{
    public class QueryArgumentException : System.Exception
    {
        string parameterName;
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }

        public QueryArgumentException() { }
        public QueryArgumentException(string parameterName)
        {
            this.parameterName = parameterName;
        }

        public QueryArgumentException(string message, string parameterName) : base(message)
        {
            this.parameterName = parameterName;
        }

    }
}
