using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace BEST2014
{
    internal static class FieldMap
    {
        private static int[] portMap = new int[] {
            3000,
            3001,
            3002,
            3003,
            3004,
            3005,
            3006,
            3007,
            3008
        };

        internal static int Port(int id)
        {
            return portMap[id];
        }
    }

    public class Field : IField
    {
        public Field(int id, IPAddress address)
        {
            Id = id;
            Address = address;
            Messages = new List<string>();

            Port = FieldMap.Port(Id);

            client = new UdpClient(Address.ToString(), Port);
            endPoint = new IPEndPoint(Address, Port);
        }

        public int Id { get; private set; }

        public int Port { get; private set; }

        public IPAddress Address { get; private set; }

        public List<string> Messages { get; private set; }

        public string LastMessage
        {
            get
            {
                return Messages.Last();
            }
        }

        public string Read()
        {
            buffer = client.Receive(ref endPoint);
            string read = Encoding.UTF8.GetString(buffer);
            Messages.Add(read);
            return read;
        }

        public async Task<string> ReadAsync()
        {
            UdpReceiveResult result = await client.ReceiveAsync();
            string read = Encoding.UTF8.GetString(result.Buffer);
            Messages.Add(read);
            return read;
        }

        private static string ResetString = "reset;";
        private static byte[] ResetCommand = Encoding.UTF8.GetBytes(ResetString.ToCharArray());
        private static int ResetCommandLength = ResetCommand.Length;

        public void Reset()
        {
            client.Send(ResetCommand, ResetCommandLength);
        }

        public async Task ResetAsync()
        {
            await client.SendAsync(ResetCommand, ResetCommandLength);
        }

        private UdpClient client;
        private IPEndPoint endPoint;
        private byte[] buffer;
    }
}
