using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChainSaw.Models;

namespace ChainSaw.Client.Console.Core
{
    [ContainAs(typeof(IChatClient), IsSingleton = true)]
    public class ChatClient : IChatClient
    {
        private IEncryptionHelper encryptionHelper;

        public ChatClient()
        {
            encryptionHelper = IocContainer.Resolve<IEncryptionHelper>();
        }

        public string ConnectedServerAddress { get; private set; }
        public string LoggedInAs { get; private set; }
        public string ChattingWith { get; private set; }

        public event EventHandler<GenericEventArgs<ConnectionRequest>> ConnectionRequested;
        public event EventHandler<GenericEventArgs<string>> MessageReceived;

        public async Task<bool> AcceptChatRequest(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task EndChat()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserInfo>> GetUsersList()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RequestChat(string userId)
        {
            ChattingWith = "Tester";
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                MessageReceived?.Invoke(this, new GenericEventArgs<string>("exit chat"));
            });
            return true;
        }

        public async Task SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetServerAddress(string address)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
            });
            return true;
        }
    }
}
