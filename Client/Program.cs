using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using Client.Classes;
using System.Runtime.CompilerServices;

namespace Client
{
    internal class Program
    {
        private static Connection connection;
        public static void Main()
        {
            Application.ApplicationExit += clientClosing;

            connection = new Connection();
            connection.Receive();

            Thread.Sleep(-1);
        }

        private static void clientClosing(object sender, System.EventArgs e)
        {
            connection.Break();
        }
    }
}