using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectEventQueue
{
    public class AsynchObjectEvent<T> : ObjectEventBase
    {
        private T _value;

        public AsynchObjectEvent(string eventName)
            : base(eventName)
        {
        }

        public AsynchObjectEvent()
            : base(string.Empty)
        { 
        }

        public T Value
        {
            get { return _value; }
            internal set { _value = value; }
        }

        internal AsynchObjectEvent<T> CopyValue(T data)
        {
            AsynchObjectEvent<T> copy = new AsynchObjectEvent<T>(base.EventName);
            copy.Value = data;
            return copy;
        }

        internal override void TriggerEvent(Delegate d)
        {
            ObjectEventDelegate<T> callback = d as ObjectEventDelegate<T>;
            if (callback != null)
            {
                callback.BeginInvoke(base.EventName, 
                    this._value, 
                    (ar) => 
                    {
                        try
                        {
                            ObjectEventDelegate<T> cb = ar.AsyncState as ObjectEventDelegate<T>;
                            cb.EndInvoke(ar);
                        }
                        catch(Exception ex)
                        {
                        }
                    },
                    callback);
            }
        }

        private void AsynchEventCallback(IAsyncResult result)
        {
            if (result.IsCompleted)
            {
                ObjectEventDelegate<T> callback = result.AsyncState as ObjectEventDelegate<T>;
                if (callback != null)
                {
                    callback.EndInvoke(result);
                }
            }
        }
    }
}
