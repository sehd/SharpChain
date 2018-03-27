using ChainSaw.Client.Console.UserInterface;
using System;
using Con = System.Console;

namespace ChainSaw.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();
        }

        private static void Initialize()
        {
            Con.SetWindowSize(120, 30);
            Con.ForegroundColor = ConsoleColor.Cyan;
            Con.WriteLine(Resources.UniversityOfTehran);
            Con.ForegroundColor = ConsoleColor.Yellow;
            Con.WriteLine(Resources.ChainsawChat);
            Con.WriteLine(Resources.AppDescription);
            Con.WriteLine(Resources.Separator);
            Con.ForegroundColor = ConsoleColor.White;
            Con.WriteLine(Resources.Initializing);
            IocContainer.Initialize(typeof(Program).Assembly);
            IocContainer.Resolve<ICommandProcessor>().Run();
        }
    }
}
