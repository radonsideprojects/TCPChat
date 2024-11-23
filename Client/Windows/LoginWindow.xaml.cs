using Client.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userBox.Text) || string.IsNullOrWhiteSpace(ipBox.Text))
            {
                MessageBox.Show("Your username and the server IP cannot be blank!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Settings.Connection.Ip = ipBox.Text;
                ChatWindow chatWindow = new ChatWindow(userBox.Text, ipBox.Text);
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
