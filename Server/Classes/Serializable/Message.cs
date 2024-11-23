using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes.Serializable
{
    public class Message
    {
        public byte[] Data { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }
    }
}