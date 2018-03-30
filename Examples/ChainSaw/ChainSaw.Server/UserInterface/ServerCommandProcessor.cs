using ChainSaw.CommandProcessor;
using ChainSaw.Models;
using ChainSaw.Server.Core;
using ChainSaw.Server.Data;
using ChainSaw.Server.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChainSaw.Server.UserInterface
{
    [ContainAs(typeof(ICommandProcessor))]
    public class ServerCommandProcessor : CommandProcessorBase
    {
        private readonly IMqttServerManager mqttServer;

        public ServerCommandProcessor()
        {
            mqttServer = IocContainer.Resolve<IMqttServerManager>();
            IocContainer.Resolve<IMessageProcessor>().Start(mqttServer);
        }

        public override void ExecuteCommand(string command, Dictionary<string, string> args)
        {
            switch (command)
            {
                case "start":
                    mqttServer.Start().Wait();
                    break;
                case "send":
                    SendMessage(args);
                    break;
                case "user-list":
                    ListUsers();
                    break;
                case "user-add":
                    AddUser(args);
                    break;
                case "help":
                    Console.WriteLine(Resources.ServerHelp);
                    break;
                case "exit":
                    break;
                default:
                    Console.WriteLine(Resources.UnknownCommand);
                    break;
            }
        }

        private void SendMessage(Dictionary<string, string> args)
        {
            mqttServer.SendMessage(new Message()
            {
                Command = "Text",
                Parameters = args["m"]
            }, args["to"]);
        }

        private void ListUsers()
        {
            using (var db = new ChainSawDbContext())
            {
                var users = db.Users.Select(obj => $"{obj.Id}\t\t{obj.Username}");
                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
        }

        private void AddUser(Dictionary<string, string> args)
        {
            if (args.Count != 2 || !args.ContainsKey("u") || !args.ContainsKey("p"))
                Console.WriteLine(Resources.InvalidParameters);
            else
            {
                using (var db = new ChainSawDbContext())
                {
                    db.Add(new User()
                    {
                        Username = args["u"],
                        PasswordHash = args["p"].HashPassword()
                    });
                    db.SaveChanges();
                }
            }
        }
    }
}
