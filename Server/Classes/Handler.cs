using Server.Classes.Serializable;

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
            try
            {
                message = Serialization.XmlDeserializeFromBytes<Message>(e.data);
                Logging.Info("Received proper data from: " + e.sender.Client.RemoteEndPoint + " broadcasting...");
                connection.broadcast(e.data);
            }
            catch
            {
                Logging.Error("Improper data sent by client: " + e.sender.Client.RemoteEndPoint);
            }
        }
    }
}