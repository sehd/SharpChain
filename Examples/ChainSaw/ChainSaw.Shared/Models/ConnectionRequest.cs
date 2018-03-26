using System;
using System.Collections.Generic;
using System.Text;

namespace ChainSaw.Models
{
    public class ConnectionRequest
    {
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string SenderPublicKey { get; set; }
        public Guid Topic { get; set; }
    }
}
