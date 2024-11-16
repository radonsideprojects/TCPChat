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
            Console.WriteLine(Properties.Resources.logo + Environment.NewLine);
            
            Handling handler = new Handling();

            handler.Initiate();

            Thread.Sleep(-1);
        }   
    }
}