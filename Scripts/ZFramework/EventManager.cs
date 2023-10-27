using System;
using System.Collections.Generic;
using UniRx;

namespace ZFramework
{
    public class EventManager:IDisposable
    {
        public static readonly EventManager Default = new EventManager();
        private Dictionary<Type, List<Action<object>>> notifiers = new Dictionary<Type, List<Action<object>>>();
        private bool isDisposed;

        public void Publish<T>(T message)
        {
            if(isDisposed) return;
            Type messageType = typeof(T);
            lock (notifiers)
            {
                if (notifiers.TryGetValue(messageType, out List<Action<object>> callbacks))
                {
                    foreach (var callback in callbacks)
                    {
                        callback(message);
                    }
                }
            }
        }
        public void Receive<T>(Action<T> callback)
        {
            Type messageType = typeof(T);
            if (isDisposed) throw new ObjectDisposedException("EventManager");
            lock (notifiers)
            {
                if (!notifiers.ContainsKey(messageType))
                {
                    notifiers[messageType] = new List<Action<object>>();
                }
                notifiers[messageType].Add(obj => callback((T)obj));
            }
        }
        public void Dispose()
        {
            lock (notifiers)
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    notifiers.Clear();
                }
            }
        }
    }
}