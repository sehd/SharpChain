using ChainSaw.Client.Console.Core;
using System;
using System.Collections.Generic;
using Con = System.Console;
using System.Threading;
using ChainSaw.Models;

namespace ChainSaw.Client.Console.UserInterface
{
    [ContainAs(typeof(IChatView))]
    public class ChatView : IChatView
    {
        private bool sessionEndFlag;
        private CancellationTokenSource readCancellationSource;
        private IChatClient chatClient;

        public ChatView()
        {
            chatClient = IocContainer.Resolve<IChatClient>();
        }

        public void EnterChatMode()
        {
            sessionEndFlag = false;
            chatClient.MessageReceived += ChatClient_MessageReceived;
            Con.WriteLine(Resources.EnterChatMessage(chatClient.ChattingWith));
            var message = "";
            do
            {
                readCancellationSource = new CancellationTokenSource();
                message = Extensions.CancellableReadLine(readCancellationSource.Token);
                if (!string.IsNullOrEmpty(message))
                {
                    chatClient.SendMessage(new Message()
                    {
                        Command = "Text",
                        Parameters = message
                    }).Wait();
                }
            } while (message.ToLower() != "exit chat" && !sessionEndFlag);
            ExitChatMode();
        }

        public void ExitChatMode()
        {
            if (!sessionEndFlag)
            {
                sessionEndFlag = true;
                if (readCancellationSource != null)
                    readCancellationSource.Cancel();
                chatClient.MessageReceived -= ChatClient_MessageReceived;
                chatClient.EndChat().WaitHandled();
            }
        }

        private void ChatClient_MessageReceived(object sender, GenericEventArgs<string> e)
        {
            if (readCancellationSource != null)
                readCancellationSource.Cancel();
            var color = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Yellow;
            Con.SetCursorPosition(0, Con.CursorTop);
            Con.WriteLine(chatClient.ChattingWith + ":");
            Con.WriteLine(e.Content);
            Con.WriteLine();
            Con.ForegroundColor = color;
            if (e.Content.ToLower() == "exit chat")
            {
                Con.WriteLine(Resources.ChatSessionEnded);
                ExitChatMode();
            }
        }
    }
}
