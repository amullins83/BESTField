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

using System.IO;

namespace TestBestCommunicator
{
    [TestFixture]
    public class TestField
    {
        private Field f;
        private IPAddress local = IPAddress.Parse("127.0.0.1");
        private MockUdpClient client = new MockUdpClient();
        private string expectedSendString = "RST";
        private string expectedQueryString = "QRY";

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
            client.ReceiveBytes = Encoding.UTF8.GetBytes("RST");

            f.Reset();

            client.SendBytes.Should()
                .Equal(Encoding.UTF8.GetBytes(expectedSendString));
        }

        [Test]
        public void ReceiveTest()
        {
            var receiveString = File.ReadAllText("Resources/FieldStateValid.txt");
            client.ReceiveBytes = Encoding.UTF8.GetBytes(receiveString);
            FieldState expectedState = new FieldState(receiveString);

            var fieldState = f.Query();

            client.SendBytes.Should()
                .Equal(Encoding.UTF8.GetBytes(expectedQueryString));

            fieldState.ToString().Should().Equal(expectedState.ToString());
        }
    }
}
