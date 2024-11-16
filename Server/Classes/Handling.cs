using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes
{
    public class Handling
    {
        private static Connection connection;
        public void Initiate()
        {
            connection = new Connection();
            connection.onReceived += onMessage;
            connection.onConnected += onClientConnected;
            connection.onDisconnected += onClientDisconnected;
            connection.Listen();
        }

        private void onMessage(object sender, ReceivedArgs e)
        {
            Message message;
            try
            {
                message = Serialization.XmlDeserializeFromBytes<Message>(e.data);
                Logging.Info("Received data from: " + e.sender.Client.RemoteEndPoint + $" ({message.Username})");
                connection.broadcast(e.data);
            }
            catch
            {
                Logging.Error("Invalid data sent by client " + e.sender.Client.RemoteEndPoint);
            }
        }

        private void onClientConnected(object sender, ConnectedArgs e)
        {
            Logging.Success("Client connected: " + e.client.Client.RemoteEndPoint);
        }

        private void onClientDisconnected(object sender, ConnectedArgs e)
        {
            Logging.Info("Client disconnected: " + e.client.Client.RemoteEndPoint);
        }
    }
}
