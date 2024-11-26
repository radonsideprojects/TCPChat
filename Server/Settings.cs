using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Settings
{
    public class Connection
    {
        public static int Port { get; set; }
        public static int BufferSize = 65536;
    }
    public class Encryption
    {
        public static byte[] Salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        public static string Key { get; set; }
    }
}