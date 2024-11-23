using Server.Classes.Serializable;
using System.Text;

namespace Server.Classes
{
    internal class Handler
    {
        private Connection connection;
        public void Initialize()
        {
            connection = new Connection();
            connection.onConnected += onClientConnected;
            connection.onDisconnected += onClientDisconnected;
            connection.onReceived += onDataReceived;
            connection.Listen();
        }

        private void onClientDisconnected(object sender, ConnectedArgs e)
        {
            Logging.Warning("Client disconnected: " + e.client.Client.RemoteEndPoint);
        }

        private void onClientConnected(object sender, ConnectedArgs e)
        {
            Logging.Success("Client connected: " + e.client.Client.RemoteEndPoint);
        }

        private void onDataReceived(object sender, ReceivedArgs e)
        {
            Message message = new Message();
            Message serverMessage = new Message();
            try
            {
                message = Serialization.XmlDeserializeFromBytes<Message>(e.data);
                switch (message.Type)
                {
                    case "chatMessage":
                        Logging.Info("Received a chat message from: " + e.sender.Client.RemoteEndPoint + $" {message.Username}");
                        connection.broadcast(e.data);
                        break;
                    case "userJoined":
                        serverMessage.Type = "userJoined";
                        serverMessage.Username = message.Username;

                        connection.broadcast(Serialization.XmlSerializeToByte(serverMessage));
                        break;
                }
            }
            catch
            {
                Logging.Error("Improper data sent by client: " + e.sender.Client.RemoteEndPoint);
            }
        }
    }
}