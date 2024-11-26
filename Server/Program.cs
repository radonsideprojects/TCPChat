using System;
using System.IO;
using Server.Classes;
using System.Threading;
using Server.Classes.Serializable;

namespace Server
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            if (!File.Exists("Settings.xml"))
            {
                SettingsC settings = new SettingsC();

                settings.Port = 1488;
                settings.Key = "ExampleKey";

                File.WriteAllText("Settings.xml", Serialization.XmlSerializeToString(settings));

                Logging.Warning("No default settings file found, creating a new one.");
            }
            else
            {
                SettingsC settings = Serialization.XmlDeserializeFromString<SettingsC>(File.ReadAllText("Settings.xml"));

                Settings.Connection.Port = settings.Port;
                Settings.Encryption.Key = settings.Key;
            }

            Handler handler = new Handler();
            handler.Initialize();

            Thread.Sleep(-1);
        }

    }
}
