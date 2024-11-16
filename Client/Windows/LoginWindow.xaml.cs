﻿using Client.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            if (Connection.isServerUp())
            {
                InitializeComponent();
            } else
            {
                MessageBox.Show("The server is unreachable.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow chatWindow = new ChatWindow(userBox.Text);
            chatWindow.Show();
            this.Close();
        }

        private void userBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            userBox.Text = userBox.Text.Replace(" ", "");
        }
    }
}
