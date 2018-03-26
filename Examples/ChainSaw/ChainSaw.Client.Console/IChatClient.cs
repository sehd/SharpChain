using ChainSaw.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChainSaw.Client.Console
{
    public interface IChatClient
    {
        event EventHandler<GenericEventArgs<ConnectionRequest>> ConnectionRequested;
        event EventHandler<GenericEventArgs<string>> MessageReceived;

        Task<bool> SetServerAddress(string address);
        Task<bool> Login(string username, string password);
        Task<List<UserInfo>> GetUsersList();
        Task<bool> RequestChat(string v);
    }
}
