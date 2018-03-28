using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChainSaw.Server.Core
{
    public interface IMqttServerManager
    {
        Task Start();
    }
}
