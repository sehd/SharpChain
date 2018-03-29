using ChainSaw.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChainSaw.Server.Core
{
    public interface IMqttServerManager
    {
        event EventHandler<GenericEventArgs<Message>> MessageReceived;

        Task Start();
        List<UserInfo> GetOnlineUsersList();
        Task SendMessage(Message message, string recipientId);
        void StartChat(params string[] users);
        string GetInChatWith(string userId);
        void EndChat(string userId);
    }
}
