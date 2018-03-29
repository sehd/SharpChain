using ChainSaw.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSaw.Server.Core
{
    public interface IMessageProcessor
    {
        void Start(IMqttServerManager server);
        void ProcessMessage(Message message);
    }
}
