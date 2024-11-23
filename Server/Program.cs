using System;
using Server.Classes;
using System.Threading;

namespace Server
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Handler handler = new Handler();
            handler.Initialize();

            Thread.Sleep(-1);
        }

    }
}
