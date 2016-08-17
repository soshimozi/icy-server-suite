using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectQuery
{
    public interface IQuery<T>
    {
        List<T> SearchAll(List<T> data);
        T FindOne(List<T> data);
        List<T> SearchAll(IEnumerable<T> data);
        T FindOne(IEnumerable<T> data);
        void SetParameterValue(string parameterName, object value);
        object GetParameterValue(string parameterName);
        void SetIndexedParameterValue(string parameterName, object[] values);
        object[] GetIndexedParameterValue(string parameterName);
        void SetIndexedParameterValue(string parameterName, int index, object value);
        object GetParameterValue(string parameterName, int index);
    }
}