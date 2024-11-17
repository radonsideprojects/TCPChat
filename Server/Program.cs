using System;
using System.Text;
using Server.Classes;
using System.Threading;
using System.Windows;

namespace Server
{
    internal class Program
    {
        private static Connection connection;
        public static void Initiate()
        {
            connection = new Connection();
            connection.onConnected += onClientConnection;
            connection.onReceived += onDataReceived;
            connection.Listen();
        }

        private static void onDataReceived(object sender, ReceivedArgs e)
        {
            Logging.Info("Received data from " + e.sender.Client.RemoteEndPoint);
            connection.broadcast(e.data);
        }

        private static void onClientConnection(object sender, ConnectedArgs e)
        {
            Logging.Success("Client connected: " + e.client.Client.RemoteEndPoint);
        }

        [STAThread]
        public static void Main()
        {
            
            Initiate();
            Thread.Sleep(-1);
        }   
    }
}