using System;
using System.Collections.Generic;
using Con=System.Console;

namespace ChainSaw.Client.Console
{
    [ContainAs(typeof(ICommandProcessor))]
    public class CommandProcessor : ICommandProcessor
    {
        private IChatClient chatClient;
        private IEncryptionHelper encryptionHelper;

        public CommandProcessor()
        {
            chatClient = IocContainer.Resolve<IChatClient>();
            encryptionHelper = IocContainer.Resolve<IEncryptionHelper>();
        }

        public void Run()
        {
            Con.WriteLine(Resources.AwaitingCommands);
            var command = "";
            do
            {
                Con.Write("> ");
                command = Con.ReadLine();
                if (!string.IsNullOrEmpty(command))
                {
                    var splitCommand = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    command = splitCommand[0].ToLower();
                    switch (command)
                    {
                        case "connect":
                            break;
                        case "login":
                            break;
                        case "list":
                            break;
                        case "chat":
                            break;
                        case "help":
                            Con.WriteLine(Resources.Help);
                            break;
                        default:
                            Con.WriteLine("Unknown command");
                            break;
                    }
                }
            } while (command != "exit");
        }
    }
}
