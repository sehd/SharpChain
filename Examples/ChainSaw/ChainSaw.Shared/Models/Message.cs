using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSaw.Models
{
    public class Message
    {
        public string SenderId { get; set; }
        public string Command { get; set; }
        public string Parameters { get; set; }
    }
}
