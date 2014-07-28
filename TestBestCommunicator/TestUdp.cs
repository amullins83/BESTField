using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

using NUnit.Framework;
using Should.Fluent;

using System.ComponentModel;

namespace TestBestCommunicator
{
    [TestFixture]
    public class TestUdp
    {
        private BackgroundWorker receiver = new BackgroundWorker();
        private byte[] expectedBytes;
        private byte[] actualBytes;
        private int port = 30000;
        private UdpClient client;
        private IPEndPoint anyAddressEndPoint;
        private IPEndPoint loopBackEndPoint;
        private bool done = false;

        [TestFixtureSetUp]
        public void RunWorker()
        {
            client = new UdpClient(port);
            anyAddressEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            loopBackEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            receiver.WorkerSupportsCancellation = true;
            receiver.DoWork += receiver_DoWork;
        }

        [SetUp]
        public void ResetDone()
        {
            done = false;
            receiver.RunWorkerAsync();
        }

        void receiver_DoWork(object sender, DoWorkEventArgs e)
        {
            while(!receiver.CancellationPending)
            {
                actualBytes = client.Receive(ref anyAddressEndPoint);
                expectedBytes.SequenceEqual(actualBytes).Should().Be.True();
                done = true;
            }

            e.Cancel = true;
        }

        [Test]
        public void TestSanity()
        {
            string testString = "Waka Waka";
            expectedBytes = Encoding.UTF8.GetBytes(testString);

            client.Send(expectedBytes, expectedBytes.Length,
                loopBackEndPoint);

            while(!done)
            {
                System.Threading.Thread.Sleep(100);
            }

            receiver.CancelAsync();
        }
    }
}
