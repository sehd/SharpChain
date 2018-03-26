using System;

namespace ChainSaw
{
    [System.AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class ForwardToAttribute : Attribute
    {
        private Type forwardType;

        public ForwardToAttribute(Type forwardType)
        {
            this.forwardType = forwardType;
        }

        public Type ForwardType
        {
            get { return forwardType; }
        }
    }
}
