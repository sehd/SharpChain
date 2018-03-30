using ChainSaw.Models;
using Newtonsoft.Json;

namespace ChainSaw.Server.Core
{
    [ContainAs(typeof(IMessageProcessor))]
    public class MessageProcessor : IMessageProcessor
    {
        private IMqttServerManager mqttServer;

        public void Start(IMqttServerManager server)
        {
            mqttServer = server;
            mqttServer.MessageReceived += (s, e) =>
                ProcessMessage(e.Content);
        }

        public void ProcessMessage(Message message)
        {
            switch (message.Command)
            {
                case "Text":
                    SendTextMessage(message.SenderId, message.Parameters);
                    break;
                case "AcceptChat":
                    AcceptChat(message.SenderId, message.Parameters);
                    break;
                case "EndChat":
                    EndChat(message.SenderId);
                    break;
                case "RequestChat":
                    RequestChat(JsonConvert.DeserializeObject<ConnectionRequest>(message.Parameters));
                    break;
                case "UserList":
                    SendUserListsTo(message.SenderId);
                    break;
                default:
                    break;
            }
        }

        private void EndChat(string senderId)
        {
            var icw = mqttServer.GetInChatWith(senderId);
            mqttServer.SendMessage(new Message()
            {
                Command = "EndChat"
            }, icw);
            mqttServer.EndChat(senderId);
            mqttServer.EndChat(icw);
        }

        private void SendTextMessage(string from, string text)
        {
            var icw = mqttServer.GetInChatWith(from);
            if (!string.IsNullOrEmpty(icw))
                mqttServer.SendMessage(new Message()
                {
                    Command = "Text",
                    Parameters = text
                }, icw);
        }

        private void AcceptChat(string accepter, string requester)
        {
            mqttServer.SendMessage(new Message()
            {
                Command = "RequestChat",
                Parameters = true.ToString(),
                SenderId = accepter
            }, requester);
            mqttServer.SendMessage(new Message()
            {
                Command = "AcceptChat",
                Parameters = true.ToString(),
                SenderId = requester
            }, accepter);
            mqttServer.StartChat(accepter, requester);
        }

        private void RequestChat(ConnectionRequest connectionRequest)
        {
            mqttServer.SendMessage(new Message()
            {
                Command = "ChatRequested",
                Parameters = JsonConvert.SerializeObject(connectionRequest),
                SenderId = connectionRequest.FromUserId
            },
            connectionRequest.ToUserId);
        }

        private void SendUserListsTo(string recipientId)
        {
            var res = mqttServer.GetOnlineUsersList();
            mqttServer.SendMessage(new Message()
            {
                Command = "UserList",
                Parameters = JsonConvert.SerializeObject(res)
            }, recipientId);
        }
    }
}
