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




        public const string AwaitingCommands = @"
Application initialized.
Start typing commands. Try 'Help' to get started.";



        public const string Help = @"
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




        public const string CommandError = "Error parsing command. Checkout 'Help' for more info";
        public const string Separator = "_________________________________________________________________";
        public const string Initializing = "Initializing application...";
        public const string Done = "Done.";
        public const string LoginSuccess = "Login successful";
        public const string LoginFail = "Login failed. Probably invalid username and password.";
        public const string ConnectionSuccess = "Connection successful";
        public const string ConnectionFail = "Server not found or connection failed";
        public const string StatusOnline = "Online";
        public const string StatusOffline = "Offline";
        public const string UserAvailable = "Available";
        public const string UserUnavailable = "Unavailable";
        public const string InvalidParameters = "Invalid parameters";
        public const string UnknownCommand = "Unknown command";
        public const string GetListUnsuccessful = "Getting list of users failed. Please check connection and try again";

        public static string GetConnectionRequestMessage(string from) => $"Connection requested from {from}";
    }
}
