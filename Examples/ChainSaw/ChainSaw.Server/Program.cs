using ChainSaw.Server.Data;
using ChainSaw.Server.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ChainSaw.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ChainSawDbContext())
            {
                db.Database.Migrate();
            }
            comm:
            var command = Console.ReadLine();
            if (command.ToLower() == "start")
            {

            }
            else if (command.ToLower().StartsWith("user-add"))
            {
                var addUserArgs = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                addUserArgs.RemoveAt(0);
                if (addUserArgs.Count != 2)
                {
                    Console.WriteLine("Bad arguments");
                    goto comm;
                }
                else
                {
                    using (var db = new ChainSawDbContext())
                    {
                        db.Add(new User()
                        {
                            Username = addUserArgs[0],
                            PasswordHash = addUserArgs[1].HashPassword()
                        });
                        db.SaveChanges();
                    }
                    goto comm;
                }
            }
            else if (command.ToLower() == "user-list")
            {
                using (var db = new ChainSawDbContext())
                {
                    var users = db.Users.Select(obj => $"{obj.Id}\t\t{obj.Username}");
                    foreach (var user in users)
                    {
                        Console.WriteLine(user);
                    }
                }
                goto comm;
            }
        }
    }
}
