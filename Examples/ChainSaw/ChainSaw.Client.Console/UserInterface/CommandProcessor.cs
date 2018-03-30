using ChainSaw.Client.Console.Core;
using ChainSaw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Con = System.Console;

namespace ChainSaw.Client.Console.UserInterface
{
    [ContainAs(typeof(ICommandProcessor))]
    public class CommandProcessor : ICommandProcessor
    {
        private CancellationTokenSource readCancellationSource;
        private IChatClient chatClient;
        private IChatView chatView;

        public CommandProcessor()
        {
            chatView = IocContainer.Resolve<IChatView>();
            chatClient = IocContainer.Resolve<IChatClient>();
            chatClient.ConnectionRequested += ChatClient_ConnectionRequested;
        }

        private void ChatClient_ConnectionRequested(object sender, GenericEventArgs<ConnectionRequest> e)
        {
            readCancellationSource.Cancel(true);
            var color = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Cyan;
            Con.SetCursorPosition(0, Con.CursorTop);
            Con.WriteLine(Resources.GetConnectionRequestMessage(e.Content.FromUserId));
            Con.ForegroundColor = color;
        }

        public void Run()
        {
            Con.WriteLine(Resources.AwaitingCommands);
            var command = "";
            do
            {
                Con.Write("\n> ");
                readCancellationSource = new CancellationTokenSource();
                command = Extensions.CancellableReadLine(readCancellationSource.Token);
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
                        Con.WriteLine(Resources.CommandError);
                        continue;
                    }
                    switch (command)
                    {
                        case "connect":
                            Connect(args);
                            break;
                        case "login":
                            Login(args);
                            break;
                        case "list":
                            List();
                            break;
                        case "chat":
                            Chat(args);
                            break;
                        case "accept":
                            Accept(args);
                            break;
                        case "help":
                            Con.WriteLine(Resources.Help);
                            break;
                        case "exit":
                            break;
                        default:
                            Con.WriteLine(Resources.UnknownCommand);
                            break;
                    }
                }
            } while (command != "exit");
        }

        private void Accept(Dictionary<string, string> args)
        {
            if (args.Count != 1 || !args.ContainsKey(""))
                Con.WriteLine(Resources.InvalidParameters);
            else
            {
                var res = chatClient.AcceptChatRequest(args[""]);
                if (res.WaitHandled() && res.Result)
                {
                    chatView.EnterChatMode();
                }
                else
                {
                    Con.WriteLine(Resources.AcceptFailed);
                }
            }
        }

        private void Chat(Dictionary<string, string> args)
        {
            if (args.Count != 1 || !args.ContainsKey(""))
                Con.WriteLine(Resources.InvalidParameters);
            else
            {
                var res = chatClient.RequestChat(args[""]);
                if (res.WaitHandled(TimeSpan.FromMinutes(1)))
                {
                    if (res.Result)
                    {
                        chatView.EnterChatMode();
                    }
                    else
                    {
                        Con.WriteLine(Resources.ChatRequestRejected(args[""]));
                    }
                }
                else
                {
                    Con.WriteLine(Resources.CreateChatError);
                }
            }
        }

        private void List()
        {
            var res = chatClient.GetUsersList();
            if (res.WaitHandled())
            {
                var users = res.Result.Select(obj => $"{obj.UserId}\t\t{(obj.IsConnected ? Resources.StatusOnline : Resources.StatusOffline)}\t\t{(obj.IsAvailable ? Resources.UserAvailable : Resources.UserUnavailable)}");
                Con.WriteLine(string.Join("\n", users));
            }
            else
            {
                Con.WriteLine(Resources.GetListUnsuccessful);
            }
        }

        private void Login(Dictionary<string, string> args)
        {
            if (args.Count != 2 || !args.ContainsKey("u") || !args.ContainsKey("p"))
                Con.WriteLine(Resources.InvalidParameters);
            else
            {
                var res = chatClient.Login(args["u"], args["p"]);
                if (res.WaitHandled() && res.Result)
                    Con.WriteLine(Resources.LoginSuccess);
                else
                    Con.WriteLine(Resources.LoginFail);
            }
        }

        private void Connect(Dictionary<string, string> args)
        {
            if (args.Count != 1 || !args.ContainsKey(""))
                Con.WriteLine(Resources.InvalidParameters);
            else
            {
                var res = chatClient.SetServerAddress(args[""]);
                if (res.WaitHandled() && res.Result)
                    Con.WriteLine(Resources.ConnectionSuccess);
                else
                    Con.WriteLine(Resources.ConnectionFail);
            }
        }
    }
}
