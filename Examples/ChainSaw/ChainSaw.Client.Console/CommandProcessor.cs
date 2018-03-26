using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Con = System.Console;

namespace ChainSaw.Client.Console
{
    [ContainAs(typeof(ICommandProcessor))]
    public class CommandProcessor : ICommandProcessor
    {
        private CancellationTokenSource readCancellationSource;
        private IChatClient chatClient;

        public CommandProcessor()
        {
            chatClient = IocContainer.Resolve<IChatClient>();
            chatClient.ConnectionRequested += ChatClient_ConnectionRequested;
            chatClient.MessageReceived += ChatClient_MessageReceived;
        }

        private void ChatClient_MessageReceived(object sender, EventArgs e)
        {
            readCancellationSource.Cancel(true);
            var color = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Cyan;
            Con.SetCursorPosition(0, Con.CursorTop);
            Con.WriteLine("Connection requested");
            Con.ForegroundColor = color;
        }

        private void ChatClient_ConnectionRequested(object sender, EventArgs e)
        {
            readCancellationSource.Cancel(true);
            var color = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Cyan;
            Con.SetCursorPosition(0, Con.CursorTop);
            Con.WriteLine("Connection requested");
            Con.ForegroundColor = color;
        }

        public void Run()
        {
            Con.WriteLine(Resources.AwaitingCommands);
            var command = "";
            do
            {
                Con.Write("> ");
                readCancellationSource = new CancellationTokenSource();
                command = CancellableRead(readCancellationSource.Token);
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
                                {
                                    args.Add("", splitCommand[i]);
                                }
                            }
                        }
                    }
                    catch
                    {
                        Con.WriteLine("Error parsing command. Checkout 'Help' for more info");
                        continue;
                    }
                    switch (command)
                    {
                        case "connect":
                            if (args.Count != 1 || !args.ContainsKey(""))
                                Con.WriteLine("Invalid parameters");
                            else
                                chatClient.SetServerAddress(args[""]);
                            break;
                        case "login":
                            if (args.Count != 2 || !args.ContainsKey("u") || !args.ContainsKey("p"))
                                Con.WriteLine("Invalid parameters");
                            else
                                chatClient.Login(args["u"], args["p"]);
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

        private string CancellableRead(CancellationToken cancellationToken)
        {
            var res = Task.Run(() =>
            {
                return Con.ReadLine();
            });
            try
            {
                res.Wait(cancellationToken);
                return res.Result;
            }
            catch
            {
                return "";
            }
        }
    }
}
