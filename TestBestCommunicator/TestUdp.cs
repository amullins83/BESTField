namespace TestBestCommunicator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using Should.Fluent;

    /// <summary>
    /// Test suite for experimenting on the <see cref="UdpClient"/> class
    /// </summary>
    [TestFixture]
    public class TestUdp
    {
        /// <summary>
        /// The expected receive data
        /// </summary>
        private byte[] expectedBytes;

        /// <summary>
        /// The test port
        /// </summary>
        private int port = 30000;

        /// <summary>
        /// The send client
        /// </summary>
        private UdpClient sendClient;

        /// <summary>
        /// The receive client
        /// </summary>
        private UdpClient receiveClient;

        /// <summary>
        /// The last received endpoint
        /// </summary>
        private IPEndPoint anyAddressEndPoint;
        
        /// <summary>
        /// The loopback endpoint
        /// </summary>
        private IPEndPoint loopBackEndPoint;
        
        /// <summary>
        /// A value indicating whether the test is complete
        /// </summary>
        private bool done = false;

        /// <summary>
        /// Create <see cref="UpdClient"/>'s and the <see cref="BackgroundWorker"/>
        /// </summary>
        [TestFixtureSetUp]
        public void RunWorker()
        {
            this.sendClient = new UdpClient();
            this.sendClient.ExclusiveAddressUse = false;
            this.sendClient.MulticastLoopback = true;

            this.receiveClient = new UdpClient();
            this.receiveClient.ExclusiveAddressUse = false;
            
            this.anyAddressEndPoint = new IPEndPoint(IPAddress.None, this.port);
            this.loopBackEndPoint = new IPEndPoint(IPAddress.Loopback, this.port + 1);
            this.sendClient.Connect(this.anyAddressEndPoint);
        }
    }
}
