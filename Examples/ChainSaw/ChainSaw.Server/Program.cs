using ChainSaw.CommandProcessor;
using ChainSaw.Server.Data;
using ChainSaw.Server.UserInterface;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChainSaw.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Resources.ServerDescription);
            Console.ForegroundColor = ConsoleColor.White;

            using (var db = new ChainSawDbContext())
            {
                db.Database.Migrate();
            }
            IocContainer.Initialize(typeof(Program).Assembly);
            IocContainer.Resolve<ICommandProcessor>().Run();
        }
    }
}
