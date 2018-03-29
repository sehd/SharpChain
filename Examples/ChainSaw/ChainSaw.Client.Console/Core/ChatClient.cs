using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChainSaw.Models;
using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Newtonsoft.Json;

namespace ChainSaw.Client.Console.Core
{
    [ContainAs(typeof(IChatClient), IsSingleton = true)]
    public class ChatClient : IChatClient
    {
        private readonly IEncryptionHelper encryptionHelper;
        private readonly IMqttClient mqttClient;
        private Message reply;

        public string ConnectedServerAddress { get; private set; }
        public string LoggedInAs { get; private set; }
        public string ChattingWith { get; private set; }

        public event EventHandler<GenericEventArgs<ConnectionRequest>> ConnectionRequested;
        public event EventHandler<GenericEventArgs<string>> MessageReceived;

        public ChatClient()
        {
            encryptionHelper = IocContainer.Resolve<IEncryptionHelper>();
            mqttClient = new MQTTnet.MqttFactory().CreateMqttClient();
            mqttClient.Connected += MqttClient_Connected;
            mqttClient.Disconnected += MqttClient_Disconnected;
            mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
        }

        public async Task<bool> AcceptChatRequest(string userId)
        {
            await SendMessage(new Message()
            {
                Command = "AcceptChat",
                Parameters = userId
            });
            var res = await Task.Run(() =>
            {
                while (reply?.Command != "AcceptChat")
                    Thread.Sleep(50);
                return reply;
            });
            if (bool.TryParse(res.Parameters, out bool reqResult))
            {
                if (reqResult)
                    ChattingWith = userId;
                return reqResult;
            }

            return false;
        }

        public async Task EndChat()
        {
            await SendMessage(new Message()
            {
                Command = "EndChat",
            });
        }

        public async Task<List<UserInfo>> GetUsersList()
        {
            await SendMessage(new Message()
            {
                Command = "UserList",
            });
            var res = await Task.Run(() =>
            {
                while (reply?.Command != "UserList")
                    Thread.Sleep(50);
                return reply;
            });
            return JsonConvert.DeserializeObject<List<UserInfo>>(res.Parameters);
        }

        public async Task<bool> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(ConnectedServerAddress))
            {
                LogHelper.Warn(this, "Please connect to server first");
                LoggedInAs = "";
                return false;
            }
            try
            {
                var options = new MqttClientOptionsBuilder().WithTcpServer(ConnectedServerAddress).
                    WithCredentials(username, password).WithClientId(username).Build();
                await mqttClient.ConnectAsync(options);
                LoggedInAs = username;
                await mqttClient.SubscribeAsync(LoggedInAs, MqttQualityOfServiceLevel.ExactlyOnce);
                return true;
            }
            catch (MqttConnectingFailedException ex)
            {
                if (ex.ReturnCode == MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword)
                    LogHelper.Warn(this, "Invalid username or password");
                else
                    LogHelper.Error(this, ex);
                LoggedInAs = "";
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.Error(this, ex);
                LoggedInAs = "";
                return false;
            }
        }

        public async Task<bool> RequestChat(string userId)
        {
            await SendMessage(new Message()
            {
                Command = "RequestChat",
                Parameters = JsonConvert.SerializeObject(new ConnectionRequest()
                {
                    FromUserId = LoggedInAs,
                    ToUserId = userId
                })
            });
            var res = await Task.Run(() =>
            {
                while (reply?.Command != "RequestChat")
                    Thread.Sleep(50);
                return reply;
            });
            if (bool.TryParse(res.Parameters, out bool reqResult))
            {
                if (reqResult)
                    ChattingWith = userId;
                return reqResult;
            }

            return false;
        }

        public async Task SendMessage(Message message)
        {
            if (string.IsNullOrEmpty(LoggedInAs))
            {
                LogHelper.Warn(this, "You are not signed in yet.");
                return;
            }
            var payload = JsonConvert.SerializeObject(message);
            await mqttClient.PublishAsync(new MqttApplicationMessageBuilder().
                WithPayload(Encoding.UTF8.GetBytes(payload)).WithTopic("Server").Build());
        }

        public async Task<bool> SetServerAddress(string address)
        {
            try
            {
                var options = new MqttClientOptionsBuilder().WithTcpServer(address).WithCredentials("aasoifa ;oihfa;osihfla", "aasdlfkbaw ia;woefh").Build();
                await mqttClient.ConnectAsync(options);
                ConnectedServerAddress = "";
                return false;
            }
            catch (MqttConnectingFailedException ex)
            {
                if (ex.ReturnCode == MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword)
                {
                    ConnectedServerAddress = address;
                    return true;
                }
                else
                {
                    ConnectedServerAddress = "";
                    return false;
                }
            }
            catch (Exception)
            {
                ConnectedServerAddress = "";
                return false;
            }
        }

        private void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            if (e.ApplicationMessage.Topic == LoggedInAs)
            {
                var data = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                var message = JsonConvert.DeserializeObject<Message>(data);
                if (message.Command == "Text")
                    MessageReceived?.Invoke(this, new GenericEventArgs<string>(message.Parameters));
                else if (message.Command == "ChatRequested")
                {
                    var req = JsonConvert.DeserializeObject<ConnectionRequest>(message.Parameters);
                    ConnectionRequested?.Invoke(this, new GenericEventArgs<ConnectionRequest>(req));
                }
                else
                    reply = message;
            }
        }

        private void MqttClient_Disconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            //LogHelper.Warn(this, "Disconnected from server");
        }

        private void MqttClient_Connected(object sender, MqttClientConnectedEventArgs e)
        {
            //LogHelper.Info(this, "Connection Successful");
        }
    }
}
