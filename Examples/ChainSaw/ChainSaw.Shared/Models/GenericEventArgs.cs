using System;

namespace ChainSaw.Models
{
    public class GenericEventArgs<T> : EventArgs
    {
        public T Content { get; private set; }

        public GenericEventArgs(T value) : base()
        {
            Content = value;
        }
    }
}
