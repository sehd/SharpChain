using ChainSaw.Server.Data;
using ChainSaw.Server.Data.Model;
using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Linq;

namespace ChainSaw.Server
{
    class MqttServerManager
    {
        IMqttServer server;

        public MqttServerManager()
        {
            server = new MqttFactory().CreateMqttServer();
            server.ApplicationMessageReceived += MessageReceived;
            server.ClientConnected += ClientConnected;
            server.ClientDisconnected += ClientDisconnected;
        }

        private void ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void MessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            var options = new MqttServerOptionsBuilder()
                .WithConnectionValidator(context =>
                {
                    using (var db = new ChainSawDbContext())
                    {
                        var user=db.Users.FirstOrDefault(obj => obj.Username == context.Username && obj.PasswordHash == context.Password.HashPassword());
                        if (user != default(User))
                            context.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                        else
                            context.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    }
                })
                .Build();
            server.StartAsync(options);
        }
    }
}
