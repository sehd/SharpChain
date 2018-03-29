using ChainSaw.Models;
using ChainSaw.Server.Core;
using ChainSaw.Server.Data;
using ChainSaw.Server.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChainSaw.Server.UserInterface
{
    [ContainAs(typeof(ICommandProcessor))]
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IMqttServerManager mqttServer;

        public CommandProcessor()
        {
            mqttServer = IocContainer.Resolve<IMqttServerManager>();
            IocContainer.Resolve<IMessageProcessor>().Start(mqttServer);
        }

        public void Run()
        {
            Console.WriteLine(Resources.AwaitingCommands);
            var command = "";
            do
            {
                Console.Write("\n> ");
                command = Console.ReadLine();
                var args = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(command))
                {
                    try
                    {
                        var splitCommand = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        command = splitCommand[0].ToLower();
                        if (splitCommand.Length > 1)
                        {
                            for (int i = 1; i < splitCommand.Length; i++)
                            {
                                if (splitCommand[i].StartsWith("-"))
                                {
                                    args.Add(splitCommand[i].TrimStart('-'), splitCommand[i + 1]);
                                    i++;
                                }
                                else
                                    args.Add("", splitCommand[i]);
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine(Resources.CommandError);
                        continue;
                    }
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
                            Console.WriteLine(Resources.Help);
                            break;
                        default:
                            Console.WriteLine(Resources.UnknownCommand);
                            break;
                    }
                }
            } while (command != "exit");
        }

        private void SendMessage(Dictionary<string, string> args)
        {
            mqttServer.SendMessage(new Message()
            {
                Command = "Text",
                Parameters = args["m"]
            },args["to"]);
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
