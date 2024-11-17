using System;
using System.Text;
using Server.Classes;
using System.Threading;
using System.Windows;

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