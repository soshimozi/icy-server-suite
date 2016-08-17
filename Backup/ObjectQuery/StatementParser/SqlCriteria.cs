using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery.StatementParser
{
    public class SqlCriteria
    {
        string typeName;
        string placeholderName;
        //string whereClause;
        WhereClause [] whereClauses;

        public WhereClause[] WhereClauses
        {
            get { return whereClauses; }
            set { whereClauses = value; }
        }

        public string PlaceholderName
        {
            get { return placeholderName; }
            set { placeholderName = value; }
        }

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }
    }
}
