using System;

namespace ChainSaw
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ContainAsAttribute : Attribute
    {
        readonly Type interfaceType;

        public ContainAsAttribute(Type interfaceType)
        {
            this.interfaceType = interfaceType;
        }

        public Type InterfaceType
        {
            get { return interfaceType; }
        }

        public string Name { get; set; }

        public bool IsSingleton { get; set; }
    }
}
