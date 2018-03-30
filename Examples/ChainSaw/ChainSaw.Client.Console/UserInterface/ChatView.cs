using ChainSaw.Client.Console.Core;
using System;
using System.Collections.Generic;
using Con = System.Console;
using System.Threading;
using ChainSaw.Models;
using SigmaSharp.SharpChain;
using Newtonsoft.Json;

namespace ChainSaw.Client.Console.UserInterface
{
    [ContainAs(typeof(IChatView))]
    public class ChatView : IChatView
    {
        private bool sessionEndFlag;
        private CancellationTokenSource readCancellationSource;
        private IChatClient chatClient;
        private Chain<string, byte[]> chain;

        public ChatView()
        {
            chatClient = IocContainer.Resolve<IChatClient>();
        }

        public void EnterChatMode()
        {
            sessionEndFlag = false;
            chain = ChainFactory.Create<string>();
            chatClient.MessageReceived += ChatClient_MessageReceived;
            Con.WriteLine(Resources.EnterChatMessage(chatClient.ChattingWith));
            var message = "";
            do
            {
                readCancellationSource = new CancellationTokenSource();
                message = Extensions.CancellableReadLine(readCancellationSource.Token);
                if (!string.IsNullOrEmpty(message))
                {
                    if (message.ToLower() == "show chain")
                        ShowChain();
                    else
                        SendMessage(message);
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

        private void ShowChain()
        {
            var messages = chain.ViewChain();
            var color = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.DarkBlue;
            Con.WriteLine("Messages already in the chain are:");
            foreach (var message in messages)
            {
                Con.WriteLine(message);
            }
            Con.WriteLine();
            Con.ForegroundColor = color;
        }

        private void SendMessage(string message)
        {
            var hash = chain.CreateBlock(message);
            var blockMessage = new BlockMessage()
            {
                Message = message,
                Hash = hash
            };
            chatClient.SendMessage(new Message()
            {
                Command = "Text",
                Parameters = JsonConvert.SerializeObject(blockMessage)
            }).Wait();
            chain.AddBlock(message, hash);
        }

        private void ChatClient_MessageReceived(object sender, GenericEventArgs<string> e)
        {
            var blockMessage = JsonConvert.DeserializeObject<BlockMessage>(e.Content);
            if (chain.AddBlock(blockMessage.Message, blockMessage.Hash))
            {
                ShowMessage(blockMessage.Message);
                if (blockMessage.Message.ToLower() == "exit chat")
                {
                    Con.WriteLine(Resources.ChatSessionEnded);
                    ExitChatMode();
                }
            }
        }

        private void ShowMessage(string message)
        {
            if (readCancellationSource != null)
                readCancellationSource.Cancel();
            var color = Con.ForegroundColor;
            Con.ForegroundColor = ConsoleColor.Yellow;
            Con.SetCursorPosition(0, Con.CursorTop);
            Con.WriteLine(chatClient.ChattingWith + ":");
            Con.WriteLine(message);
            Con.WriteLine();
            Con.WriteLine();
            Con.ForegroundColor = color;
            
        }
    }
}
