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
        private int count = 0;
        public ChatWindow(string _username)
        {
            InitializeComponent();
            Username = _username;
            connection = new Connection();
            connection.onReceived += onMessageReceived;
            chatBox.AppendText("Welcome to the chat!\n\n");
            connection.Receive();
        }

        private void onMessageReceived(object sender, ReceivedArgs e)
        {
            Dispatcher.Invoke(() => {
                DateTime timestamp = DateTime.Now;
                Message message = Serialization.XmlDeserializeFromBytes<Message>(e.data);

                if (message.Username == "system")
                {
                    if (count > 0)
                    {
                        chatBox.AppendText("\n" + message.Content + "\n\n");
                    }
                    count++;
                }
                else
                {
                    chatBox.AppendText($"{timestamp.Hour}:{timestamp.Minute} " + $"[ {message.Username} ]"  + ": " + message.Content + "\n");
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
                message.Content = inputBox.Text;
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