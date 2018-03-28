using ChainSaw.Server.Data;
using ChainSaw.Server.Data.Model;
using ChainSaw.Server.UserInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ChainSaw.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Resources.AppDescription);
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
