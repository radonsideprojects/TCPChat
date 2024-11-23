using System;

namespace Server.Classes
{
    public class Logging
    {
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[!] " + message);
            Console.ResetColor();
        }

        public static void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[!] " + message);
            Console.ResetColor();
        }

        public static void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[*] " + message);
            Console.ResetColor();
        }

        public static void Info(string message)
        {
            Console.WriteLine("[*] " + message);
        }
    }
}
