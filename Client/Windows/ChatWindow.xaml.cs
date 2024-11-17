﻿using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Client.Classes;

namespace Client.Windows
{
    public partial class ChatWindow : Window
    {
        private Connection connection;
        private string Username;
        public ChatWindow(string _username)
        {
            InitializeComponent();
            Username = _username;
            connection = new Connection();
            connection.onReceived += onMessageReceived;
            connection.Receive();
        }

        private void onMessageReceived(object sender, ReceivedArgs e)
        {
            Dispatcher.Invoke(() => {
                Message message = Serialization.XmlDeserializeFromBytes<Message>(e.data);
                chatBox.AppendText(message.Username + ": " + message.Content + "\n");
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