namespace ChainSaw.Client.Console
{
    static class Resources
    {
        public const string UniversityOfTehran = @"
 _   _       _                    _ _                  __   _____    _                     
| | | |     (_)                  (_) |                / _| |_   _|  | |                    
| | | |_ __  ___   _____ _ __ ___ _| |_ _   _    ___ | |_    | | ___| |__  _ __ __ _ _ __  
| | | | '_ \| \ \ / / _ \ '__/ __| | __| | | |  / _ \|  _|   | |/ _ \ '_ \| '__/ _` | '_ \ 
| |_| | | | | |\ V /  __/ |  \__ \ | |_| |_| | | (_) | |     | |  __/ | | | | | (_| | | | |
 \___/|_| |_|_| \_/ \___|_|  |___/_|\__|\__, |  \___/|_|     \_/\___|_| |_|_|  \__,_|_| |_|
                                         __/ |                                             
                                        |___/                                              ";

        public const string ChainsawChat = @"
 ██████╗██╗  ██╗ █████╗ ██╗███╗   ██╗███████╗ █████╗ ██╗    ██╗     ██████╗██╗  ██╗ █████╗ ████████╗
██╔════╝██║  ██║██╔══██╗██║████╗  ██║██╔════╝██╔══██╗██║    ██║    ██╔════╝██║  ██║██╔══██╗╚══██╔══╝
██║     ███████║███████║██║██╔██╗ ██║███████╗███████║██║ █╗ ██║    ██║     ███████║███████║   ██║   
██║     ██╔══██║██╔══██║██║██║╚██╗██║╚════██║██╔══██║██║███╗██║    ██║     ██╔══██║██╔══██║   ██║   
╚██████╗██║  ██║██║  ██║██║██║ ╚████║███████║██║  ██║╚███╔███╔╝    ╚██████╗██║  ██║██║  ██║   ██║   
 ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝ ╚══╝╚══╝      ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   
                                                                                                    ";
        public const string AppDescription = @"
A blockchain based chat system.
Written by: Emad Hosseini, Fateme Souri and Faeze Moradi";
        public const string Separator = "_________________________________________________________________";
        public const string Initializing = "Initializing application...";
        public const string Done = "Done.";
        public const string AwaitingCommands = @"
Application initialized.
Start typing commands. Try 'Help' to get started.";
        public const string Help = @"
Command        Parameters                    Description
_________________________________________________________________________________________________
Help                                         Display this help

Connect        ServerAddress                Connect to server located at server address

LogIn          -u Username -p Password       Log in to selected server with provided credentials

List                                         Get a list of online users

Chat           UserId                       Start chat with available user

Exit                                         Exit from chat and application
________________________________________________________________________________________________";
    }
}
