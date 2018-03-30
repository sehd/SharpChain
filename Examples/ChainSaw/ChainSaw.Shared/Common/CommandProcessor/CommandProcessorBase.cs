using ChainSaw.ConsoleHelper;
using System;
using System.Collections.Generic;
using System.Threading;
using Con = System.Console;

namespace ChainSaw.CommandProcessor
{
    public abstract class CommandProcessorBase : ICommandProcessor
    {
        protected CancellationTokenSource readCancellationSource;
        private IConsoleHelper consoleHelper;

        public CommandProcessorBase()
        {
            consoleHelper = IocContainer.Resolve<IConsoleHelper>();
        }

        public void Run()
        {
            Console.WriteLine(Resources.AwaitingCommands);
            var command = "";
            do
            {
                Console.Write("\n> ");
                readCancellationSource = new CancellationTokenSource();
                command = consoleHelper.CancellableReadLine(readCancellationSource.Token);
                if (!string.IsNullOrEmpty(command))
                {
                    ProcessSingleCommand(command);
                }
            } while (command != "exit");
        }

        private void ProcessSingleCommand(string command)
        {
            var args = new Dictionary<string, string>();
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
                return;
            }
            ExecuteCommand(command, args);
        }

        public abstract void ExecuteCommand(string command, Dictionary<string, string> args);
    }
}
