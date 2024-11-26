using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes.Serializable
{
    public class Server
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string EncryptionKey { get; set; }
    }
}
