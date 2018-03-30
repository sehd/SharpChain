using ChainSaw.Models;
using ChainSaw.Server.Data;
using ChainSaw.Server.Data.Model;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainSaw.Server.Core
{
    [ContainAs(typeof(IMqttServerManager))]
    public class MqttServerManager : IMqttServerManager
    {
        private readonly IMqttServer server;
        private List<ConnectionInfo> connections;

        public event EventHandler<GenericEventArgs<Message>> MessageReceived;

        public MqttServerManager()
        {
            connections = new List<ConnectionInfo>();
            server = new MqttFactory().CreateMqttServer();
            server.ApplicationMessageReceived += OnMessageReceived;
            server.ClientConnected += ClientConnected;
            server.ClientDisconnected += ClientDisconnected;
        }

        private void ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            LogHelper.Debug(this, "Client Connected");
            connections.RemoveAll(obj => obj.UserName == e.Client.ClientId);
        }

        private void ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            LogHelper.Debug(this, "Client Connected");
            connections.Add(new ConnectionInfo()
            {
                UserName = e.Client.ClientId
            });
        }

        private void OnMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.ClientId))
            {
                var data = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                var message = JsonConvert.DeserializeObject<Message>(data);
                message.SenderId = e.ClientId;
                MessageReceived?.Invoke(this, new GenericEventArgs<Message>(message));
            }
        }

        public async Task Start()
        {
            var options = new MqttServerOptionsBuilder()
                .WithConnectionValidator(context =>
                {
                    using (var db = new ChainSawDbContext())
                    {
                        var user = db.Users.FirstOrDefault(obj => obj.Username == context.Username && obj.PasswordHash == context.Password.HashPassword());
                        if (user != default(User))
                            context.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                        else
                            context.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    }
                })
                .Build();
            await server.StartAsync(options);
            LogHelper.Info(this, "MQTT server running. Press x to exit");
            char c = ' ';
            while (c != 'x')
            {
                c = Console.ReadKey().KeyChar;
            }
            await server.StopAsync();
        }

        public List<UserInfo> GetOnlineUsersList()
        {
            return connections.Select(obj => new UserInfo()
            {
                UserId = obj.UserName,
                IsAvailable = string.IsNullOrEmpty(obj.InChatWith),
                IsConnected = true,
            }).ToList();
        }

        public async Task SendMessage(Message message, string recipientId)
        {
            if (connections.Exists(obj => obj.UserName == recipientId))
            {
                var appMessage = new MqttApplicationMessageBuilder().WithTopic(recipientId).
                    WithPayload(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message))).Build();
                await server.PublishAsync(appMessage);
            }
            else
                LogHelper.Warn(this, "Sending message to inactive client");
        }

        public void StartChat(params string[] users)
        {
            foreach (var user in users)
            {
                connections.First(obj => obj.UserName == user).InChatWith = 
                    users.FirstOrDefault(obj => obj != user);
            }
        }

        public string GetInChatWith(string userId)
        {
            return connections.FirstOrDefault(obj => obj.UserName == userId)?.InChatWith;
        }

        public void EndChat(string userId)
        {
            var user = connections.FirstOrDefault(obj => obj.UserName == userId);
            if (user != null)
                user.InChatWith = null;
        }
    }
}
