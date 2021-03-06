﻿using System;
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
            32250,
            32251,
            32252,
            32253,
        };

        internal static int Port(int id)
        {
            return portMap[id - 1];
        }
    }

    public class Field : IField
    {
        public Field(int id, IPAddress address, IUdpClient client)
        {
            Id = id;
            Address = address;
            Messages = new List<string>();

            Port = FieldMap.Port(Id);
            this.client = client;

            if (Address != null)
            {
                client.Connect(Address, Port);


                endPoint = new IPEndPoint(Address, Port);
            }
        }

        public int Id { get; private set; }

        public int Port { get; private set; }

        public IPAddress Address { get; private set; }

        public List<string> Messages { get; private set; }

        public string LastMessage
        {
            get
            {
                if (Messages.Any())
                {
                    return Messages.Last();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private static string QueryString = "QRY";
        private static byte[] QueryCommand = Encoding.UTF8.GetBytes(QueryString.ToCharArray());
        private static int QueryCommandLength = QueryCommand.Length;

        public FieldState Query()
        {
            client.Send(QueryCommand, QueryCommandLength);
            buffer = client.Receive(ref endPoint);
            return new FieldState(processQueryBuffer(buffer));
        }

        public async Task<FieldState> QueryAsync()
        {
            await client.SendAsync(QueryCommand, QueryCommandLength);
            UdpReceiveResult result = await client.ReceiveAsync();
            return new FieldState(processQueryBuffer(result.Buffer));
        }

        private string processQueryBuffer(byte[] buffer)
        {
            if (buffer == null)
            {
                // The field was disconnected
                return string.Empty;
            }

            string read = Encoding.UTF8.GetString(buffer);
            Messages.Add(read);
            return read;
        }

        private static string ResetString = "RST";
        private static byte[] ResetCommand = Encoding.UTF8.GetBytes(ResetString.ToCharArray());
        private static int ResetCommandLength = ResetCommand.Length;

        public void Reset()
        {
            client.Send(ResetCommand, ResetCommandLength);
            byte[] buffer = client.Receive(ref endPoint);
            processResetResponse(buffer);
        }

        public async Task ResetAsync()
        {
            await client.SendAsync(ResetCommand, ResetCommandLength);
            UdpReceiveResult result = await client.ReceiveAsync();
            processResetResponse(result.Buffer);
        }

        private void processResetResponse(byte[] buffer)
        {
            string response = processQueryBuffer(buffer);
            if (response != "RST")
            {
                System.Diagnostics.Debug.WriteLine(
                    "Did not get RST acknowledge: {0}", response);
            }
        }

        private IUdpClient client;
        private IPEndPoint endPoint;
        private byte[] buffer;
    }
}
