using System.Collections.Generic;
using Server.Classes.Serializable;

namespace Server.Classes
{
    internal class Handler
    {
        private Connection connection;

        private List<User> users;
        public void Initialize()
        {
            users = new List<User>();
            connection = new Connection();
            connection.onConnected += onClientConnected;
            connection.onDisconnected += onClientDisconnected;
            connection.onReceived += onDataReceived;
            connection.Listen();
        }

        private void onClientDisconnected(object sender, ConnectedArgs e)
        {
            Logging.Warning("Client disconnected: " + e.client.Client.RemoteEndPoint);

            User userToRemove = users.Find(u => u.Client == e.client);

            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                Logging.Success($"Removed user {userToRemove.Username ?? "Unknown"} from the list.");

                Message serverMessage = new Message
                {
                    Type = "userLeft",
                    Username = userToRemove.Username
                };

                connection.broadcast(Serialization.XmlSerializeToByte(serverMessage));
            }
        }

        private void onClientConnected(object sender, ConnectedArgs e)
        {
            Logging.Success("Client connected: " + e.client.Client.RemoteEndPoint);
        }

        private void onDataReceived(object sender, ReceivedArgs e)
        {
            Message message = new Message();
            Message serverMessage = new Message();
            User user = new User();
            try
            {
                message = Serialization.XmlDeserializeFromBytes<Message>(e.data);
                switch (message.Type)
                {
                    case "chatMessage":
                        Logging.Info("Received a chat message from: " + e.sender.Client.RemoteEndPoint + $"({message.Username})");
                        connection.broadcast(e.data);
                        break;
                    case "userJoined":
                        user.Client = e.sender;
                        user.Username = message.Username;

                        users.Add(user);

                        Logging.Success("Added client " + $"{e.sender.Client.RemoteEndPoint} ({message.Username}) to the user list.");

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