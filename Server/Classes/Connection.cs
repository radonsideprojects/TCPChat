using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Server.Classes;

namespace Server.Classes
{
    class ConnectedArgs : EventArgs
    {
        public TcpClient client { get; set; }

        public ConnectedArgs(TcpClient _client) {
            client = _client;
        }
    }

    class ReceivedArgs : EventArgs {
        public byte[] data { get; set; }
        public TcpClient sender { get; set; }

        public ReceivedArgs(byte[] _data, TcpClient _sender)
        {
            data = _data;
            sender = _sender;
        }
    }

    internal class Connection
    {
        private TcpListener listener;

        private List<TcpClient> clients;

        private CancellationTokenSource cts;
        private CancellationToken ct;

        public Connection()
        {
            listener = new TcpListener(IPAddress.Any, Settings.Connection.Port);

            clients = new List<TcpClient>();

            cts = new CancellationTokenSource();
            ct = cts.Token;
        }

        public event EventHandler<ReceivedArgs> onReceived;
        public event EventHandler<ConnectedArgs> onConnected;

        public List<TcpClient> getClients()
        {
            return clients;
        }

        public void Listen()
        {
            Task.Run(async () =>
            {
                listener.Start();
                Logging.Info("Listening for connections on port: " + Settings.Connection.Port);

                while (!cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        var client = await listener.AcceptTcpClientAsync();
                        lock (clients) clients.Add(client);

                        onConnected?.Invoke(this, new ConnectedArgs(client));

                        receiveFromClient(client);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Logging.Error("Failed to accept a client\n" + ex.ToString());
                    }
                }
            });
        }

        private void receiveFromClient(TcpClient client)
        {
            Task.Run(async () =>
            {
                try
                {
                    byte[] buffer = new byte[Settings.Connection.BufferSize];
                    NetworkStream stream = client.GetStream();

                    while (!cts.Token.IsCancellationRequested)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cts.Token);

                        if (bytesRead == 0)
                        {
                            lock (clients) clients.Remove(client);
                            Logging.Warning("Client disconnected: " + client.Client.RemoteEndPoint);
                            break;
                        }

                        byte[] receivedData = buffer.Take(bytesRead).ToArray();
                        onReceived?.Invoke(this, new ReceivedArgs(Encryption.Decrypt(receivedData), client));
                    }
                }
                catch (Exception)
                {
                    lock (clients) clients.Remove(client);
                    Logging.Warning("Client disconnected: " + client.Client.RemoteEndPoint);
                }
            });
        }

        public void sendToClient(TcpClient client, byte[] data)
        {
            var stream = client.GetStream();
            var _data = Encryption.Encrypt(data);
            stream.Write(_data, 0, _data.Length);
        }

        public void Dispose()
        {
            listener.Stop();
            clients.Clear();
        }

        public void Break()
        {
            cts.Cancel();
            Dispose();
        }
    }
}
