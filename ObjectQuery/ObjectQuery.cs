using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using ObjectQuery.StatementParser;

namespace ObjectQuery
{
    public class ObjectQuery<T> : IQuery<T>
    {
        Dictionary<string, QueryParameter> parameters = new Dictionary<string, QueryParameter>();

        private ObjectQuery() { }

        public ObjectQuery(SqlCriteria criteria, object[] values)
        {
            if (criteria.WhereClauses.Length != values.Length)
            {
                throw new ArgumentException("Argument list size mismatch", "fields");
            }
            else
            {
                for (int i = 0; i < criteria.WhereClauses.Length; i++)
                {
                    QueryParameter parameter = new QueryParameter();
                    parameter.Name = criteria.WhereClauses[i].FieldName.Replace(string.Format("{0}.", criteria.PlaceholderName), "");
                    parameter.QueryOperation = criteria.WhereClauses[i].FieldOperation;
                    
                    // if this is a set type, then values should and better be an
                    // array of objects and it's size had better same size as declaration
                    // or the search will throw exceptions
                    if (criteria.WhereClauses[i].FieldOperation == WhereClause.Operation.Set)
                    {
                        if (values[i] is object[] && ((object[])values[i]).Length == criteria.WhereClauses[i].SetSize)
                        {
                            parameter.SetValues = (object[])values[i];
                        }
                        else
                        {
                            throw new ArgumentException("Argument type incorrect", "values");
                        }
                    }
                    else
                    {
                        parameter.Value = values[i];
                    }

                    parameters.Add(parameter.Name, parameter);
                }
            }
        }

        private bool searchPredicate(T dataItem)
        {
            int matchCount = 0;
            foreach(QueryParameter parameter in parameters.Values)
            {
                if (checkFieldMatch(dataItem, parameter))
                    matchCount++;
            }

            return (matchCount == parameters.Count);
        }

        private bool checkFieldMatch(object dataItem, QueryParameter parameter)
        {
            bool match = false;
            Type type = typeof(T);

            string propertyName = string.Format("get_{0}", parameter.Name);
            MethodInfo info = type.GetMethod(propertyName);
            if (info == null)
            {
                throw new QueryArgumentException("Invalid query parameter.", parameter.Name);
            }
            else if (parameter.QueryOperation == WhereClause.Operation.Set)
            {
                foreach(object setValue in parameter.SetValues)
                {
                    if (setValue.Equals(info.Invoke(dataItem, new object[] { })))
                    {
                        // small optimization, since this is OR, we leave early (non-greedy)
                        match = true;
                        break;
                    }
                }

            }
            else
            {
                match = parameter.Value.Equals(info.Invoke(dataItem, new object[] { }));
            }

            return match;
        }


        #region IQuery<T> Members

        public List<T> SearchAll(IEnumerable<T> data)
        {
            // copy to list of type
            List<T> dataList = new List<T>(data);
            return SearchAll(dataList);
        }

        public T FindOne(IEnumerable<T> data)
        {
            List<T> dataList = new List<T>(data);
            return FindOne(dataList);
        }

        public List<T> SearchAll(List<T> data)
        {
            return data.FindAll(new Predicate<T>(searchPredicate));
        }

        public T FindOne(List<T> data)
        {
            return data.Find(new Predicate<T>(searchPredicate));
        }

        public void SetParameterValue(string parameterName, object value)
        {
            if (!parameters.ContainsKey(parameterName))
            {
                throw new QueryArgumentException("Invalid query parameter.", parameterName);
            }
            else
            {
                parameters[parameterName].Value = value;
            }

        }

        public object GetParameterValue(string parameterName)
        {
            if (!parameters.ContainsKey(parameterName))
            {
                throw new QueryArgumentException("Invalid query parameter.", parameterName);
            }
            else
            {
                return parameters[parameterName].Value;
            }
        }

        public void SetIndexedParameterValue(string parameterName, object[] values)
        {
            if (!parameters.ContainsKey(parameterName))
            {
                throw new QueryArgumentException("Invalid query parameter.", parameterName);
            }
            else if (parameters[parameterName].SetValues.Length != values.Length)
            {
                throw new QueryArgumentException("Invalid query parameter.", parameterName);
            }
            else
            {
                parameters[parameterName].SetValues = values;
            }
        }

        public object[] GetIndexedParameterValue(string parameterName)
        {
            if (!parameters.ContainsKey(parameterName))
            {
                throw new QueryArgumentException("Invalid query parameter.", parameterName);
            }
            else
            {
                return parameters[parameterName].SetValues;
            }
        }

        public void SetIndexedParameterValue(string parameterName, int index, object value)
        {
            if (!parameters.ContainsKey(parameterName))
            {
                throw new QueryArgumentException("Invalid query parameter.", parameterName);
            }
            else if (index < 0 || index >= parameters[parameterName].SetValues.Length)
            {
                throw new QueryArgumentException("Invalid query parameter index.", parameterName);
            }
            else
            {
                parameters[parameterName].SetValues[index] = value;
            }

        }

        public object GetParameterValue(string parameterName, int index)
        {
            if (!parameters.ContainsKey(parameterName))
            {
                throw new QueryArgumentException("Invalid query parameter.", parameterName);
            }
            else if (index < 0 || index >= parameters[parameterName].SetValues.Length)
            {
                throw new QueryArgumentException("Invalid query parameter index.", parameterName);
            }
            else
            {
                return parameters[parameterName].SetValues[index];
            }
        }

        #endregion

    }
}
