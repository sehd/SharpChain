namespace ChainSaw
{
    public static class Resources
    {
        public static string UniversityOfTehran => @"
 _   _       _                    _ _                  __   _____    _                     
| | | |     (_)                  (_) |                / _| |_   _|  | |                    
| | | |_ __  ___   _____ _ __ ___ _| |_ _   _    ___ | |_    | | ___| |__  _ __ __ _ _ __  
| | | | '_ \| \ \ / / _ \ '__/ __| | __| | | |  / _ \|  _|   | |/ _ \ '_ \| '__/ _` | '_ \ 
| |_| | | | | |\ V /  __/ |  \__ \ | |_| |_| | | (_) | |     | |  __/ | | | | | (_| | | | |
 \___/|_| |_|_| \_/ \___|_|  |___/_|\__|\__, |  \___/|_|     \_/\___|_| |_|_|  \__,_|_| |_|
                                         __/ |                                             
                                        |___/                                              ";
        public static string ChainsawChat => @"
 ██████╗██╗  ██╗ █████╗ ██╗███╗   ██╗███████╗ █████╗ ██╗    ██╗     ██████╗██╗  ██╗ █████╗ ████████╗
██╔════╝██║  ██║██╔══██╗██║████╗  ██║██╔════╝██╔══██╗██║    ██║    ██╔════╝██║  ██║██╔══██╗╚══██╔══╝
██║     ███████║███████║██║██╔██╗ ██║███████╗███████║██║ █╗ ██║    ██║     ███████║███████║   ██║   
██║     ██╔══██║██╔══██║██║██║╚██╗██║╚════██║██╔══██║██║███╗██║    ██║     ██╔══██║██╔══██║   ██║   
╚██████╗██║  ██║██║  ██║██║██║ ╚████║███████║██║  ██║╚███╔███╔╝    ╚██████╗██║  ██║██║  ██║   ██║   
 ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝ ╚══╝╚══╝      ╚═════╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝   
                                                                                                    ";
        public static string AppDescription => @"
A blockchain based chat system.
Written by: Emad Hosseini, Fateme Souri and Faeze Moradi";
        public static string ServerDescription => @"
ChainSaw Chat Server
A blockchain based chat system.
Written by: Emad Hosseini, Fateme Souri and Faeze Moradi";
        public static string AwaitingCommands => @"
Application initialized.
Start typing commands. Try 'Help' to get started.";
        public static string AppHelp => @"
Command        Parameters                    Description
_________________________________________________________________________________________________
Help                                         Display this help

Connect        ServerAddress                 Connect to server located at server address

LogIn          -u Username -p Password       Log in to selected server with provided credentials

List                                         Get a list of online users

Chat           UserId                        Start chat with available user

Accept         UserId                        Accept connection requested from UserId

Exit                                         Exit from chat and application
________________________________________________________________________________________________";
        public static string ServerHelp => @"
Command        Parameters                    Description
_________________________________________________________________________________________________
Help                                         Display this help

Start                                        Start MQTT server

User-List                                    List available users

User-Add      -u Username -p Password        Create a new user in server

Exit                                         Exit from chat and application
________________________________________________________________________________________________";
        public static string CommandError => "Error parsing command. Checkout 'Help' for more info";
        public static string Separator => "_________________________________________________________________";
        public static string Initializing => "Initializing application...";
        public static string LoginSuccess => "Login successful";
        public static string LoginFail => "Login failed. Probably invalid username and password.";
        public static string ConnectionSuccess => "Connection successful";
        public static string ConnectionFail => "Server not found or connection failed";
        public static string StatusOnline => "Online";
        public static string StatusOffline => "Offline";
        public static string UserAvailable => "Available";
        public static string UserUnavailable => "Unavailable";
        public static string InvalidParameters => "Invalid parameters";
        public static string UnknownCommand => "Unknown command";
        public static string GetListUnsuccessful => "Getting list of users failed. Please check connection and try again";
        public static string CreateChatError => "Starting chat failed. Please try again.";
        public static string AcceptFailed => "Chat request could not be accepted. Either request is not present or other user unavailable.";
        public static string ChatSessionEnded => "Chat session ended. You are back at command console.";
        public static string GetConnectionRequestMessage(string from) => $"Connection requested from {from}";
        public static string ChatRequestRejected(string userId) => $"Your chat request rejected by {userId}";
        public static string EnterChatMessage(string chattingWith) => $"You are now chatting with {chattingWith}.\nStart typing messages and press enter to send.\nType in 'Exit Chat' to exit chat and return to command console.\nType in 'Show Chain' to see the messages already in your blockchain.";
    }
}
