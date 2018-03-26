using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChainSaw.Client.Console
{
    [ContainAs(typeof(IChatClient))]
    public class ChatClient : IChatClient
    {
        private IEncryptionHelper encryptionHelper;

        public ChatClient()
        {
            encryptionHelper = IocContainer.Resolve<IEncryptionHelper>();
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                ConnectionRequested?.Invoke(this, new EventArgs());
            });
        }

        public event EventHandler ConnectionRequested;
        public event EventHandler MessageReceived;

        public void Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool SetServerAddress(string address)
        {
            throw new NotImplementedException();
        }
    }
}
