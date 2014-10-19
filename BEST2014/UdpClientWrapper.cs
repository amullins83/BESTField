namespace BEST2014
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Wrapper around the real System.Net.Sockets.UdpClient class to implement BEST2014.IUdpClient
    /// </summary>
    public class UdpClientWrapper : IUdpClient, IDisposable
    {
        /// <summary>
        /// Actual UdpClient object
        /// </summary>
        private UdpClient client = new UdpClient();

        /// <summary>
        /// Value indicating whether the UdpClient has already been disposed
        /// </summary>
        private bool isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="UdpClientWrapper"/> class
        /// </summary>
        public UdpClientWrapper()
        {
            this.client.EnableBroadcast = true;
            this.client.ExclusiveAddressUse = false;
            this.client.MulticastLoopback = true;
        }

        /// <summary>
        /// Synchronously listen for UDP data
        /// </summary>
        /// <returns>A byte array with the received data</returns>
        public byte[] Receive()
        {
            IPEndPoint endpoint = null;
            return this.Receive(ref endpoint);
        }

        /// <summary>
        /// Synchronously listen for UDP data
        /// </summary>
        /// <param name="endpoint">[Output] Takes the value of the endpoint from which data was received</param>
        /// <returns>A byte array with the received data</returns>
        public byte[] Receive(ref IPEndPoint endpoint)
        {
            byte[] rec = null;
            try
            {
                rec = this.client.Receive(ref endpoint);
            }
            catch (SocketException)
            {
                // Suppress common errors
            }

            return rec;
        }

        /// <summary>
        /// Asynchronously listen for UDP data
        /// </summary>
        /// <returns>An await-able promise object, which yields a UdpReceiveResult</returns>
        public async Task<UdpReceiveResult> ReceiveAsync()
        {
            return await this.client.ReceiveAsync();
        }

        /// <summary>
        /// Synchronously send UDP data
        /// </summary>
        /// <param name="message">A byte array to send</param>
        /// <param name="length">The length of the byte array sent</param>
        public void Send(byte[] message, int length)
        {
            this.client.Send(message, length);
        }

        /// <summary>
        /// Asynchronously send UDP data
        /// </summary>
        /// <param name="message">A byte array to send</param>
        /// <param name="length">The length of the byte array sent</param>
        /// <returns></returns>
        public async Task SendAsync(byte[] message, int length)
        {
            await this.client.SendAsync(message, length);
        }

        /// <summary>
        /// Establish a connection to a particular device and port
        /// </summary>
        /// <param name="address">The IP address of the connecting device</param>
        /// <param name="port">The port for the connection</param>
        public void Connect(IPAddress address, int port)
        {
            this.client.Connect(address, port);
        }

        /// <summary>
        /// Close the UdpClient handle
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                Dispose(true);
                isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        /// <summary>
        /// Close the UdpClient handle if it has not already been done
        /// </summary>
        /// <param name="isDisposing">A value indicating whether the handle needs to be closed now</param>
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing && !isDisposed)
            {
                this.client.Close();
            }
        }
    }
}
