using ChainSaw.Models;
using ChainSaw.Server.Data;
using ChainSaw.Server.Data.Model;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainSaw.Server.Core
{
    [ContainAs(typeof(IMqttServerManager))]
    public class MqttServerManager : IMqttServerManager
    {
        IMqttServer server;
        private IMessageProcessor messageProcessor;

        public MqttServerManager()
        {
            messageProcessor = IocContainer.Resolve<IMessageProcessor>();
            server = new MqttFactory().CreateMqttServer();
            server.ApplicationMessageReceived += MessageReceived;
            server.ClientConnected += ClientConnected;
            server.ClientDisconnected += ClientDisconnected;
        }

        private void ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            LogHelper.Debug(this, "Client Connected");
        }

        private void ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            LogHelper.Debug(this, "Client Connected");
        }

        private void MessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            var data = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            var message = JsonConvert.DeserializeObject<Message>(data);
            messageProcessor.ProcessMessage(message);
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
    }
}
