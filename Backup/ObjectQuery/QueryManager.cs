using System;
using System.Collections.Generic;
using System.Text;
using ObjectQuery.StatementParser;

namespace ObjectQuery
{
    public class QueryManager<T>
    {
        public QueryManager()
        {
        }

        static public IQuery<T> GetQuery(string sql, params object[] values)
        {
            ObjectQuery<T> query = new ObjectQuery<T>(parseQuery(sql), values);
            return query;
        }

        static private SqlCriteria parseQuery(string query)
        {
            SqlStatementParser parser = new ObjectQuery.StatementParser.SqlStatementParser();
            return parser.ParseStatement(query);
        }
    }
}
