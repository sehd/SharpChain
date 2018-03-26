using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSaw.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string PasswordHash { get; set; }
        public bool IsConnected { get; set; }
        public bool IsAvailable { get; set; }
    }
}
