using System;

namespace ChainSaw
{
    public interface ILogger
    {
        void Error(Type type, string typeName, string methodName, string group, string message, Exception ex);
        void Info(Type type, string typeName, string methodName, string group, string message);
        void Warn(Type type, string typeName, string methodName, string group, string message);
        void Debug(Type type, string typeName, string methodName, string group, string message);
    }
}
