using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChainSaw.Models;

namespace ChainSaw.Client.Console
{
    [ContainAs(typeof(IChatClient))]
    public class ChatClient : IChatClient
    {
        private IEncryptionHelper encryptionHelper;

        public ChatClient()
        {
            encryptionHelper = IocContainer.Resolve<IEncryptionHelper>();
        }

        public event EventHandler<GenericEventArgs<ConnectionRequest>> ConnectionRequested;
        public event EventHandler<GenericEventArgs<string>> MessageReceived;

        public async Task<List<UserInfo>> GetUsersList()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string username, string password)
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
