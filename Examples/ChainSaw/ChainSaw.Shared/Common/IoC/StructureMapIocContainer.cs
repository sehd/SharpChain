using StructureMap;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ChainSaw
{
    public static class IocContainer
    {
        private static Container container;

        #region Resolve

        public static T Resolve<T>()
        {
            return container.TryGetInstance<T>();
        }

        public static T Resolve<T>(string name)
        {
            return container.TryGetInstance<T>(name);
        }

        #endregion

        public static void RegisterType(Type @interface, Type concrete)
        {
            container.Configure(o => o.For(@interface).Singleton().Use(concrete));
        }

        #region Initialize

        public static void Initialize(Assembly rootAssembly)
        {
            try
            {
                container = new Container();
                var assemblies = GetAssemblies(rootAssembly);
                foreach (var assembly in assemblies)
                {
                    if (assembly.FullName.StartsWith("Rayanmehr"))
                        RegisterTypesInAssembly(assembly);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(IocContainer), ex);
                throw;
            }
        }

        private static IEnumerable<Assembly> GetAssemblies(Assembly rootAssembly)
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(rootAssembly);

            do
            {
                var asm = stack.Pop();

                yield return asm;

                foreach (var reference in asm.GetReferencedAssemblies())
                    if (reference.FullName.StartsWith(
                        rootAssembly.FullName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0]) &&
                        !list.Contains(reference.FullName))
                    {
                        stack.Push(Assembly.Load(reference));
                        list.Add(reference.FullName);
                    }

            }
            while (stack.Count > 0);

        }

        private static void RegisterTypesInAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes();

            var concreteRegisters = types.Where(obj =>
                Attribute.IsDefined(obj, typeof(ContainAsAttribute)));
            foreach (var type in concreteRegisters)
            {
                RegisterSingleType(type);
            }

            var forwardRegisters = types.Where(obj =>
                Attribute.IsDefined(obj, typeof(ForwardToAttribute)));
            foreach (var type in forwardRegisters)
            {
                RegisterSingleForwardType(type);
            }
        }

        #region Register ContainAs attribute

        private static void RegisterSingleType(Type type)
        {
            var concretes = Attribute.GetCustomAttributes(type, typeof(ContainAsAttribute));
            foreach (ContainAsAttribute concrete in concretes)
            {
                if (!concrete.InterfaceType.IsAssignableFrom(type) ||
                    !type.IsClass)
                    throw new Exception("Invalid configuration for type: " + type.Name);

                container.Configure(p => p.For(concrete.InterfaceType).
                    CreateExpression(concrete.IsSingleton,
                    type, concrete.Name));
            }
        }

        private static ConfiguredInstance CreateExpression(this GenericFamilyExpression obj,
            bool IsSingleton, Type concrete, string Name)
        {
            if (IsSingleton)
                obj = obj.Singleton();

            var res = obj.Use(concrete);
            if (!string.IsNullOrEmpty(Name))
                res.Named(Name);
            return res;
        }

        #endregion

        #region Register ForwardTo attribute

        private static void RegisterSingleForwardType(Type type)
        {
            var forwards = Attribute.GetCustomAttributes(type, typeof(ForwardToAttribute));
            foreach (ForwardToAttribute forward in forwards)
            {
                container.Configure(p => p.For(type).
                    Use(ctx => ctx.GetInstance(forward.ForwardType)));
            }
        }

        #endregion

        #endregion
    }
}
