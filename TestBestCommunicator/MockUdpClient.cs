using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BEST2014;

using System.Net;
using System.Net.Sockets;

namespace TestBestCommunicator
{
    public class MockUdpClient : IUdpClient
    {
        public IPEndPoint Endpoint { get; set; }
        public byte[] ReceiveBytes { get; set; }
        public byte[] SendBytes { get; private set; }
        public int SendLength { get; private set; }
        public IPAddress Address { get; private set; }
        public int Port { get; private set; }

        public byte[] Receive(ref IPEndPoint endpoint)
        {
            Endpoint = endpoint;
            return ReceiveBytes;
        }

        public async Task<UdpReceiveResult> ReceiveAsync()
        {
            return await Task.Run(new Func<UdpReceiveResult>(
                makeResult));
        }

        private UdpReceiveResult makeResult()
        {
            return new UdpReceiveResult(ReceiveBytes, Endpoint);
        }

        public void Send(byte[] message, int length)
        {
            SendBytes = message;
            SendLength = length;
        }

        private delegate void Sender();

        public async Task SendAsync(byte[] message, int length)
        {
            Sender doSend = delegate
            {
                Send(message, length);
            };

            await Task.Run(new Action(doSend));
        }

        public void Connect(IPAddress address, int port)
        {
            Address = address;
            Port = port;
        }
    }
}
