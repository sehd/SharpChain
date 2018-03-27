using System;
using Con = System.Console;

namespace ChainSaw.Client.Console
{
    [ContainAs(typeof(ILogger))]
    public class Logger : ILogger
    {
        public void Debug(Type type, string typeName, string methodName, string group, string message)
        {
            var oldColor = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Gray;
            Con.WriteLine($"{DateTime.Now}\tDEBUG\t{message}\t{group}\t{typeName}-{methodName}");
            Con.ForegroundColor = oldColor;
        }

        public void Error(Type type, string typeName, string methodName, string group, string message, Exception ex)
        {
            var oldColor = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Red;
            Con.WriteLine("ERROR");
            Con.WriteLine($"{DateTime.Now}\t{message}\t{group}\t{typeName}-{methodName}");
            Con.WriteLine(ex.Message);
            if (ex.InnerException != null)
                Con.WriteLine(ex.InnerException.Message);
            Con.WriteLine("Call Stack");
            Con.WriteLine(ex.StackTrace);
            Con.ForegroundColor = oldColor;
        }

        public void Info(Type type, string typeName, string methodName, string group, string message)
        {
            var oldColor = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Gray;
            Con.WriteLine($"{DateTime.Now}\tINFO\t{message}\t{group}\t{typeName}-{methodName}");
            Con.ForegroundColor = oldColor;
        }

        public void Warn(Type type, string typeName, string methodName, string group, string message)
        {
            var oldColor = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Yellow;
            Con.WriteLine($"{DateTime.Now}\tWARN\t{message}\t{group}\t{typeName}-{methodName}");
            Con.ForegroundColor = oldColor;
        }
    }
}
