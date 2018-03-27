using ChainSaw.Client.Console.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSaw.Client.Console.UserInterface
{
    [ContainAs(typeof(IChatView))]
    public class ChatView : IChatView
    {
        private IChatClient chatClient;

        public ChatView()
        {
            chatClient = IocContainer.Resolve<IChatClient>();
        }

        public void EnterChatMode()
        {
            chatClient.MessageReceived += ChatClient_MessageReceived;

        }

        public void ExitChatMode()
        {
            chatClient.MessageReceived -= ChatClient_MessageReceived;

        }

        private void ChatClient_MessageReceived(object sender, GenericEventArgs<string> e)
        {
            throw new NotImplementedException();
        }
    }
}
