using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Server.Classes
{
    public class Serialization
    {
        // https://stackoverflow.com/a/20770973
        public static byte[] XmlSerializeToByte<T>(T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream))
                {
                    serializer.Serialize(xmlWriter, value);

                    return memoryStream.ToArray();
                }
            }
        }

        public static T XmlDeserializeFromBytes<T>(byte[] bytes)
                                         where T : class
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new InvalidOperationException();
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                using (XmlReader xmlReader = XmlReader.Create(memoryStream))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }

        // Me when I'm lazy
        public static byte[] SerializeListToBytes<T>(List<T> list)
        {
            return XmlSerializeToByte(list);
        }

        public static List<T> DeserializeListFromBytes<T>(byte[] bytes)
        {
            return XmlDeserializeFromBytes<List<T>>(bytes);
        }
    }
}
