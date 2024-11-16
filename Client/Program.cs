using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Client.Classes;

namespace Client
{
    internal class Program
    {
        private static Connection connection;
        public static void Main()
        {
            Application.ApplicationExit += clientClosing;

            connection = new Connection();
            connection.onReceived += onReceivedFromServer;
            connection.Receive();
            connection.sendToServer(Encoding.UTF8.GetBytes("Test message :P"));

            Thread.Sleep(-1);
        }

        private static void onReceivedFromServer(object sender, ReceivedArgs e)
        {
            Console.WriteLine("Received from server: " + Encoding.UTF8.GetString(e.data));
        }

        private static void clientClosing(object sender, System.EventArgs e)
        {
            connection.Break();
        }
    }
}