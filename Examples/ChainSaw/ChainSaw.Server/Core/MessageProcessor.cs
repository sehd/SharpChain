using System;
using System.Collections.Generic;
using System.Text;
using ChainSaw.Models;

namespace ChainSaw.Server.Core
{
    [ContainAs(typeof(IMessageProcessor))]
    public class MessageProcessor : IMessageProcessor
    {
        public void ProcessMessage(Message message)
        {
            switch (message.Command)
            {
                case "Text":
                    break;
                case "AcceptChat":
                    break;
                case "EndChat":
                    break;
                case "RequestChat":
                    break;
                case "UserList":
                    break;
                default:
                    break;
            }
        }
    }
}
