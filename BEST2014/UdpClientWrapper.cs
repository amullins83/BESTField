using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace BEST2014
{
    public class UdpClientWrapper : IUdpClient, IDisposable
    {
        private UdpClient client = new UdpClient();

        public byte[] Receive(ref IPEndPoint endpoint)
        {
            return client.Receive(ref endpoint);
        }

        public async Task<UdpReceiveResult> ReceiveAsync()
        {
            return await client.ReceiveAsync();
        }

        public void Send(byte[] message, int length)
        {
            client.Send(message, length);
        }

        public async Task SendAsync(byte[] message, int length)
        {
            await client.SendAsync(message, length);
        }

        public void Connect(IPAddress address, int port)
        {
            client.Connect(address, port);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool shouldDispose)
        {
            client.Close();
        }
    }
}
