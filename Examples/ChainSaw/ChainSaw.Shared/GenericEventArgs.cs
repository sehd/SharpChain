using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSaw
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
