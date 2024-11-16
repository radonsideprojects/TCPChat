using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

namespace Client
{
    internal class Program
    {   
        private static TcpClient client;
        public static void Main()
        {
            Application.ApplicationExit += clientClosing;
            client = new TcpClient();
            client.Connect("109.87.212.225", 1488);

            var stream = client.GetStream();
            byte[] buffer = new byte[] { };

            while (true) {
                Console.Write(": ");
                string a = Console.ReadLine();
                buffer = Encoding.UTF8.GetBytes(a);
                stream.Write(buffer, 0, buffer.Length);
            };
        }

        private static void clientClosing(object sender, System.EventArgs e)
        {
            client.Close();
        }
    }
}