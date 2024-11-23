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
            app.DispatcherUnhandledException += GlobalExceptionHandleEvent;
            app.Run(new Windows.LoginWindow());
        }

        private static void GlobalExceptionHandleEvent(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}