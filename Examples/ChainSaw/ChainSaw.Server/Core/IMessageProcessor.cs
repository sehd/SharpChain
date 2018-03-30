using ChainSaw.Models;

namespace ChainSaw.Server.Core
{
    public interface IMessageProcessor
    {
        void Start(IMqttServerManager server);
        void ProcessMessage(Message message);
    }
}
