using Client.Classes;
using Client.Classes.Serializable;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ObservableCollection<Server> servers;
        public LoginWindow()
        {
            servers = new ObservableCollection<Server>();
            if (File.Exists("Servers.xml"))
                servers = Serialization.XmlDeserializeFromString<ObservableCollection<Server>>(File.ReadAllText("Servers.xml"));
            else
            {
                Server defaultServer = new Server();

                defaultServer.IP = "127.0.0.1";
                defaultServer.EncryptionKey = "ExampleKey";
                defaultServer.Port = 1488;
                defaultServer.Name = "My Server";

                servers.Add(defaultServer);

                File.WriteAllText("Servers.xml", Serialization.XmlSerializeToString<ObservableCollection<Server>>(servers));
            }

            InitializeComponent();

            serverBox.ItemsSource = servers;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userBox.Text))
            {
                MessageBox.Show("Your username cannot be blank!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Server server = (Server)serverBox.SelectedItem;

                Settings.Connection.Ip = server.IP;
                Settings.Connection.Port = server.Port;
                Settings.Encryption.Key = server.EncryptionKey;

                ChatWindow chatWindow = new ChatWindow(userBox.Text, server.Name);
                chatWindow.Show();
                
                this.Close();
            }
            
        }

        private void userBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userBox.Text = userBox.Text.Replace(" ", "");
            userBox.ScrollToEnd();
            if (userBox.Text == "system")
                userBox.Text = "";
        }
    }
}
