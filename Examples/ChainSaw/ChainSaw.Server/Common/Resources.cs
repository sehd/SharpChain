using System;

namespace ChainSaw.Server
{
    static class Resources
    {
        public const string AppDescription = @"
ChainSaw Chat Server
A blockchain based chat system.
Written by: Emad Hosseini, Fateme Souri and Faeze Moradi";


        public const string AwaitingCommands = @"
Application initialized.
Start typing commands. Try 'Help' to get started.";



        public const string Help = @"
Command        Parameters                    Description
_________________________________________________________________________________________________
Help                                         Display this help

Start                                        Start MQTT server

User-List                                    List available users

Add-User      -u Username -p Password        Create a new user in server

Exit                                         Exit from chat and application
________________________________________________________________________________________________";




        public const string UnknownCommand = "Unknown command";
        public const string CommandError = "Error parsing command. Checkout 'Help' for more info";
        public const string InvalidParameters = "Invalid parameters";

        //public static string EnterChatMessage(string chattingWith) => $"You are now chatting with {chattingWith}.\nStart typing messages and press enter to send.\nType in 'Exit Chat' to exit chat and return to command console.";
    }
}
