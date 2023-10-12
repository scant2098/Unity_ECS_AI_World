using System;
using System.Collections.Generic;

namespace JH_ECS
{
    public class ObjectPool<T> where T : new()
    {
        private Queue<T> objects;
        private Action<T> ResetObjectCallback;

        public ObjectPool(Action<T> resetObjectCallback)
        {
            objects = new Queue<T>();
            ResetObjectCallback = resetObjectCallback;
        }

        public T Get()
        {
            if (objects.Count > 0)
            {
                T obj = objects.Dequeue();
                ResetObjectCallback(obj);
                return obj;
            }
            else
            {
                return new T();
            }
        }

        public void Return(T obj)
        {
            objects.Enqueue(obj);
        }
    }
}