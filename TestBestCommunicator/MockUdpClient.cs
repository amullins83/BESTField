namespace TestBestCommunicator
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using BEST2014;

    /// <summary>
    /// Dummy UDP client class to test fields
    /// </summary>
    public class MockUdpClient : IUdpClient
    {
        /// <summary>
        /// Gets or sets the endpoint to be delivered on the <c>endpoint</c>
        /// parameter of the <see cref="Receive"/> method
        /// </summary>
        public IPEndPoint Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the bytes to be returned by calls to <see cref="Receive"/>
        /// </summary>
        public byte[] ReceiveBytes { get; set; }

        /// <summary>
        /// Gets the last byte array passed to the <c>message</c> parameter of <see cref="Send"/>
        /// </summary>
        public byte[] SendBytes { get; private set; }

        /// <summary>
        /// Gets the last byte-array length passed to the <c>length</c> parameter of <see cref="Send"/>
        /// </summary>
        public int SendLength { get; private set; }

        /// <summary>
        /// Gets the IP address of the most recent connection
        /// </summary>
        public IPAddress Address { get; private set; }

        /// <summary>
        /// Gets the port number of the most recent connection
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// Mock for the Receive method
        /// </summary>
        /// <returns>The value of the <see cref="ReceiveBytes"/> property</returns>
        public byte[] Receive()
        {
            return this.ReceiveBytes;
        }

        /// <summary>
        /// Mock for the Receive method
        /// </summary>
        /// <param name="endpoint">[Output] This will hold the value of the <see cref="Endpoint"/> property after the call</param>
        /// <returns>The value of the <see cref="ReceiveBytes"/> property</returns>
        public byte[] Receive(ref IPEndPoint endpoint)
        {
            endpoint = this.Endpoint;
            return this.ReceiveBytes;
        }

        /// <summary>
        /// Mock for the asynchronous receive method
        /// </summary>
        /// <returns>An await-able promise object that yields a <see cref="UdpReceiveResult"/></returns>
        public async Task<UdpReceiveResult> ReceiveAsync()
        {
            return await Task.Run(new Func<UdpReceiveResult>(
                this.MakeResult));
        }

        /// <summary>
        /// Synchronously mock the Send method
        /// </summary>
        /// <param name="message">The new value for the <see cref="SendBytes"/> property</param>
        /// <param name="length">The new value for the <see cref="SendLength"/> property</param>
        public void Send(byte[] message, int length)
        {
            this.SendBytes = message;
            this.SendLength = length;
        }

        /// <summary>
        /// Asynchronously mock the Send method
        /// </summary>
        /// <param name="message">The new value for the <see cref="SendBytes"/> property</param>
        /// <param name="length">The new value for the <see cref="SendLength"/> property</param>
        /// <returns>An await-able promise object</returns>
        public async Task SendAsync(byte[] message, int length)
        {
            Action doSend = delegate
            {
                this.Send(message, length);
            };

            await Task.Run(doSend);
        }

        /// <summary>
        /// Set the <see cref="Address"/> and <see cref="Port"/> properties
        /// </summary>
        /// <param name="address">The new value for the <see cref="Address"/> property</param>
        /// <param name="port">The new value for the <see cref="Port"/> property</param>
        public void Connect(IPAddress address, int port)
        {
            this.Address = address;
            this.Port = port;
        }

        /// <summary>
        /// Produce a <see cref="UdpReceiveResult"/> with the <see cref="ReceiveBytes"/> and <see cref="Endpoint"/> properties
        /// </summary>
        /// <returns>A new instance of the <see cref="UdpReceiveResult"/> class</returns>
        private UdpReceiveResult MakeResult()
        {
            return new UdpReceiveResult(this.ReceiveBytes, this.Endpoint);
        }
    }
}
