using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes
{
    public class User
    {
        public TcpClient Client { get; set; }
        public string Username { get; set; }
    }
}
