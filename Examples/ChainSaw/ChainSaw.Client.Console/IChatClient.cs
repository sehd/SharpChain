using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSaw.Client.Console
{
    public interface IChatClient
    {
        event EventHandler ConnectionRequested;
        event EventHandler MessageReceived;

        bool SetServerAddress(string address);
        void Login(string username, string password);
    }
}
