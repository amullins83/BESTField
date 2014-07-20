using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Should.Fluent;

using BEST2014;
using System.Net;
using System.Net.Sockets;

namespace TestBestCommunicator
{
    [TestFixture]
    public class TestField
    {
        private Field f;
        private IPAddress local = IPAddress.Parse("127.0.0.1");
        private MockUdpClient client = new MockUdpClient();
        private string expectedSendString = "reset;";

        [SetUp]
        public void BeforeEach()
        {
            f = new Field(1, local, client);
        }

        [TearDown]
        public void AfterEach()
        {

        }

        [Test]
        public void SendTest()
        {            
            f.Reset();

            client.SendBytes.Should()
                .Equal(Encoding.UTF8.GetBytes(expectedSendString));
        }
    }
}
