using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace Client.Classes
{
    public class Connection
    {
        private TcpClient client;
        private CancellationTokenSource cts;
        private CancellationToken ct;

        public Connection()
        {
            client = new TcpClient();
            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        public void Receive()
        {
            Task.Run(() =>
            {
                client.Connect(Settings.Connection.Ip, Settings.Connection.Port);

                byte[] buffer = new byte[Settings.Connection.BufferSize];
                NetworkStream stream = client.GetStream();

                while (!ct.IsCancellationRequested)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    Console.WriteLine(Encoding.UTF8.GetString(Encryption.Decrypt(buffer.Take(bytesRead).ToArray())));
                }
            });
        }

        public void Dispose()
        {
            client.Close();
            client.Dispose();
        }

        public void Break()
        {
            Dispose();
            cts.Cancel();
        }
    }
}