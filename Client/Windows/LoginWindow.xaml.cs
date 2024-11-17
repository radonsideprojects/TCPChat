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
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (userBox.Text == "")
            {
                MessageBox.Show("Your username cannot be blank!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ChatWindow chatWindow = new ChatWindow(userBox.Text);
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
