using System;
using System.Collections.Generic;
using System.Text;
using ObjectQuery.StatementParser;

namespace ObjectQuery
{
    public class QueryParameter
    {
        string name;
        object value;
        WhereClause.Operation operation;
        object[] setValues;

        public object[] SetValues
        {
            get { return setValues; }
            set { setValues = value; }
        }

        public WhereClause.Operation QueryOperation
        {
            get { return operation; }
            set { operation = value; }
        }

        public object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
