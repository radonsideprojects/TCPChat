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
            Task.Run(async() =>
            {
                client.Connect(Settings.Connection.Ip, Settings.Connection.Port);

                byte[] buffer = new byte[Settings.Connection.BufferSize];
                NetworkStream stream = client.GetStream();

                while (!ct.IsCancellationRequested)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    Console.WriteLine("Received from server: " + Encoding.UTF8.GetString(Encryption.Decrypt(buffer.Take(bytesRead).ToArray())));
                }
            });
        }

        public void sendToServer(byte[] data)
        {
            byte[] encryptedData = Encryption.Encrypt(data);
            var stream = client.GetStream();
            stream.Write(encryptedData, 0, encryptedData.Length);
        }

        public void Break()
        {
            client.Close();
            cts.Cancel();
        }
    }
}