using System.Xml.Serialization;

namespace Client.Settings
{
    public class Connection
    {
        public static int Port { get; set; }
        public static string Ip { get; set; }

        [XmlIgnore]
        public static int BufferSize = 65536;
    }
    public class Encryption
    {
        [XmlIgnore]
        public static byte[] Salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        public static string Key { get; set; }
    }
}