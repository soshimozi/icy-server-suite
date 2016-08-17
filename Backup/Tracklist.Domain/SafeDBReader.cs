using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Tracklist.Domain
{
    internal class SafeDbReader<T>
    {
        static internal T GetValue(SqlDataReader reader, string columnName)
        {
            try
            {
                if (reader.HasRows && reader[columnName] != DBNull.Value)
                {
                    return (T)reader[columnName];
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        static internal T GetValue(SqlDataReader reader, int index)
        {
            try
            {
                if (reader.HasRows && reader[index] != DBNull.Value)
                {
                    return (T)reader[index];
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }

}
