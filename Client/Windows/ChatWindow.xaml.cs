using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Client.Classes;
using System;

namespace Client.Windows
{
    public partial class ChatWindow : Window
    {
        private Connection connection;
        private string Username;
        private string IP;
        public ChatWindow(string _username, string _ip)
        {
            InitializeComponent();
            Username = _username;
            IP = _ip;

            this.Title = $"TCPChat ({_username}) | Server: {_ip}";

            connection = new Connection();
            connection.onReceived += onMessageReceived;

            chatBox.AppendText("Welcome to the chat!" + "\n");
            connection.Receive();

            Message message = new Message();
            message.Username = _username;
            message.Type = "userJoined";

            connection.sendToServer(Serialization.XmlSerializeToByte(message));
        }

        private void onMessageReceived(object sender, ReceivedArgs e)
        {
            Dispatcher.Invoke(() => {
                DateTime timestamp = DateTime.Now;
                Message message = Serialization.XmlDeserializeFromBytes<Message>(e.data);

                switch (message.Type)
                {
                    case "chatMessage":
                        chatBox.AppendText($"{timestamp.Hour}:{timestamp.Minute} " + $"[ {message.Username} ]" + ": " + Encoding.UTF8.GetString(message.Data) + "\n");
                        break;
                    case "userJoined":
                        chatBox.AppendText("A user has joined: " + message.Username + "\n");
                        break;
                }
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connection.Break();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Message message;
            if (e.Key == Key.Enter)
            {
                message = new Message();
                message.Data = Encoding.UTF8.GetBytes(inputBox.Text);
                message.Type = "chatMessage";
                message.Username = Username;
                connection.sendToServer(Serialization.XmlSerializeToByte<Message>(message));
                inputBox.Clear();
            }
        }

        private void chatBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            chatBox.ScrollToEnd();
        }
    }
}