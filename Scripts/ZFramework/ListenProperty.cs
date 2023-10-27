using System;

namespace ZFramework
{
    public class ListenProperty<T> where T : IEquatable<T>
    {
        private event Action<T> ValueChanged;
        private T value;

        public ListenProperty(T value)
        {
            this.value = value;
        }
        public void Subscribe(Action<T> callback)
        {
            ValueChanged += callback;
        }
        public void UnsubscribeAll()
        {
            ValueChanged = null;
        }
        public T Value
        {
            get { return value; }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    ValueChanged?.Invoke(value);
                }
            }
        }
    }
}