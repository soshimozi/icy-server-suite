using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;
using ExtensionFunctions;

namespace ObjectEventQueue
{
    public delegate void ObjectEventDelegate(object messagePayload);

    public class AsynchEventManager
    {
        delegate void MessageHandlerDelegate(MessageBase message, ListenInfo[] listeners);

        private static AsynchEventManager _instance = null;

        private Dictionary<string, List<ListenInfo>> _listenerMap = new Dictionary<string, List<ListenInfo>>();
        //List<ListenInfo> _listeners = new List<ListenInfo>();

        private Stack<MessageBase> _messageQueue = new Stack<MessageBase>();

        AutoResetEvent _stopEvent = new AutoResetEvent(false);

        // A semaphore for the worker thread pool
        //
        private static Semaphore _pool;

        // 
        private Mutex _listenerMapMutex = new Mutex();
        private Mutex _messageQueueMutex = new Mutex();

        private AsynchEventManager()
        {
            int maxWorkers = 5;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxWorkerThreads"]))
            {
                int.TryParse(ConfigurationManager.AppSettings["MaxWorkerThreads"], out maxWorkers);
            }

            _pool = new Semaphore(maxWorkers, maxWorkers);

            StartPump();
        }

        public static AsynchEventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AsynchEventManager();
                }

                return _instance;
            }
        }

        public void UnSubscribe(Guid clientId, Type messagePayloadType)
        {
            if (_listenerMapMutex.WaitOne())
            {
                try
                {
                    string messageKey = messagePayloadType.ToString();
                    if (_listenerMap.ContainsKey(messageKey))
                    {
                        var query = from li in _listenerMap[messageKey]
                                    where li.ClientId.Equals(clientId)
                                    select li;

                        ListenInfo entry = query.FirstOrDefault();
                        if (entry != null)
                        {
                            // remove it from the list
                            _listenerMap[messageKey].Remove(entry);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    _listenerMapMutex.ReleaseMutex();
                }
            }
        }

        public Guid Subscribe<T>(ObjectEventDelegate eventToCall)
        {
            string messageKey = typeof(T).ToString();

            Guid clientId = Guid.NewGuid();
            ListenInfo listenerInfo = new ListenInfo()
            {
                EventDelegate = eventToCall,
                ClientId = clientId
            };

            if (_listenerMapMutex.WaitOne())
            {
                try
                {
                    if (!_listenerMap.ContainsKey(messageKey))
                    {
                        _listenerMap[messageKey] = new List<ListenInfo>();
                    }

                    _listenerMap[messageKey].Add(listenerInfo);
                    return clientId;
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    _listenerMapMutex.ReleaseMutex();
                }

            }

            return Guid.Empty;
        }

        public void Publish<T>(T messagePayload, int priority, DateTime sendTime)
        {
            MessageBase eventToSend = new MessageBase()
            {
                Priority = priority,
                SendTime = sendTime,
                Payload = messagePayload,
                PayloadType = typeof(T)
            };

            QueueEvent(eventToSend, priority);
        }

        private void MessagePumpThread()
        {
            _started = true;
            while (!_stopEvent.WaitOne(1))
            {
                // try to get an open pool slot
                if (_pool.WaitOne(0))
                {

                    MessageBase message = DequeueEvent();
                    ListenInfo[] listeners;

                    if (message != null && (listeners = GetListeners(message.PayloadType.ToString())) != null)
                    {
                        MessageHandlerDelegate handlerDelegate = new MessageHandlerDelegate(
                            (mb, list) =>
                            {
                                try
                                {
                                    MessageEventHandler(mb, list);
                                }
                                catch (Exception ex)
                                {
                                }
                            });

                        handlerDelegate.BeginInvoke(message, listeners,
                            (ar) =>
                            {
                                try
                                {
                                    handlerDelegate.EndInvoke(ar);
                                }
                                catch (Exception ex)
                                { }
                                finally
                                {
                                    _pool.Release();
                                }
                            }, null);
                    }
                    else
                    {
                        _pool.Release();
                    }
                }
            }
            _started = false;
        }

        private void QueueEvent(MessageBase eventToSend, int priority)
        {
            if (_messageQueueMutex.WaitOne())
            {
                try
                {
                    _messageQueue.Push(eventToSend);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    _messageQueueMutex.ReleaseMutex();
                }
            }
        }

        private MessageBase DequeueEvent()
        {
            MessageBase msg = null;

            if (_messageQueueMutex.WaitOne())
            {
                try
                {
                    if (_messageQueue.Count > 0)
                    {
                        msg = _messageQueue.Pop();
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    _messageQueueMutex.ReleaseMutex();
                }
            }

            return msg;
        }

        private ListenInfo [] GetListeners(string eventType)
        {
            ListenInfo [] listeners = null;
            if (_listenerMapMutex.WaitOne())
            {
                try
                {
                    if (_listenerMap.ContainsKey(eventType))
                    {
                        listeners = _listenerMap[eventType].ToArray();
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    _listenerMapMutex.ReleaseMutex();
                }
            }
            return listeners;
        }

        private void MessageEventHandler(MessageBase msg, ListenInfo[] listeners)
        {
            foreach (ListenInfo eventInfo in listeners)
            {
                ObjectEventDelegate callback = eventInfo.EventDelegate;
                if (callback != null)
                {
                    callback.Invoke(msg.Payload);
                }
            }
        }

        private bool _started = false;
        public void StartPump()
        {
            if (!_started)
            {
                Thread thread = new Thread(new ThreadStart(MessagePumpThread));
                thread.Start();
            }
        }

        public void StopPump()
        {
            _stopEvent.Set();
        }

    }
}
