using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BEST2014
{
    public interface IUdpClient
    {
        byte[] Receive(ref IPEndPoint endpoint);
        Task<UdpReceiveResult> ReceiveAsync();
        void Send(byte[] message, int length);
        Task SendAsync(byte[] message, int length);
        void Connect(IPAddress address, int port);
    }
}
