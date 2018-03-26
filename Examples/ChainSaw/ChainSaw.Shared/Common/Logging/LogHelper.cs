using System;
using System.Runtime.CompilerServices;

namespace ChainSaw
{
    public static class LogHelper
    {
        private static ILogger logger;
        private static ILogger Logger
        {
            get
            {
                if (logger == null)
                    GetLogger();
                return logger;
            }
        }

        private static void GetLogger()
        {
            logger = IocContainer.Resolve<ILogger>();
        }

        public static void Error(object type, Exception ex, string group = "", [CallerMemberName] string methodName = "")
        {
            group = string.IsNullOrEmpty(group) ? ex.GetType().Name : group;
            Logger.Error(type.GetType(), GetTypeName(type), methodName, group, ex.Message, ex);
        }

        public static void Error(object type, string message, Exception ex, string group = "", [CallerMemberName] string methodName = "")
        {
            group = string.IsNullOrEmpty(group) ? ex.GetType().Name : group;
            Logger.Error(type.GetType(), GetTypeName(type), methodName, group, message, ex);
        }

        public static void Info(object type, string message, string group = "", [CallerMemberName] string methodName = "")
        {
            Logger.Info(type.GetType(), GetTypeName(type), methodName, group, message);
        }

        public static void Warn(object type, string message, string group = "", [CallerMemberName] string methodName = "")
        {
            Logger.Warn(type.GetType(), GetTypeName(type), methodName, group, message);
        }

        public static void Debug(object type, string message, string group = "", [CallerMemberName] string methodName = "")
        {
            Logger.Debug(type.GetType(), GetTypeName(type), methodName, group, message);
        }

        private static string GetAssemblyName(object type)
        {
            if (type is Type)
            {
                return (type as Type).Assembly.ManifestModule.Name;
            }
            return type == null ? "" : type.GetType().Assembly.ManifestModule.Name;
        }

        private static string GetTypeName(object type)
        {
            if (type is Type)
            {
                return (type as Type).FullName;
            }
            return type == null ? "" : type.GetType().FullName;
        }
    }
}
