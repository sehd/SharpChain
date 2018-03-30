using ChainSaw.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChainSaw.Client.Console.Core
{
    public interface IChatClient
    {
        event EventHandler<GenericEventArgs<ConnectionRequest>> ConnectionRequested;
        event EventHandler<GenericEventArgs<string>> MessageReceived;

        Task<bool> SetServerAddress(string address);
        Task<bool> Login(string username, string password);
        Task<List<UserInfo>> GetUsersList();
        Task<bool> RequestChat(string userId);
        Task<bool> AcceptChatRequest(string userId);
        Task SendMessage(Message message);
        Task EndChat();

        string ConnectedServerAddress { get; }
        string LoggedInAs { get; }
        string ChattingWith { get; }
    }
}
