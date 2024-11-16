using System;
using System.Text;
using System.Threading;
using System.Windows;

using Client.Classes;

namespace Client
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new Windows.LoginWindow());
        }
    }
}