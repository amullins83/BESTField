using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Should.Fluent;

using BEST2014;
using System.Net;
using System.Net.Sockets;

using System.IO;

namespace TestBestCommunicator
{
    public class TestField
    {
        private Field f;
        private IPAddress local = IPAddress.Loopback;
        private MockUdpClient client = new MockUdpClient();
        private string expectedSendString = "RST";
        private string expectedQueryString = "QRY";


        [Fact]
        public void SendTest()
        {
            f = new Field(1, local, client);
            client.ReceiveBytes = Encoding.UTF8.GetBytes("RST");

            f.Reset();

            client.SendBytes.Should()
                .Equal(Encoding.UTF8.GetBytes(expectedSendString));
        }

        [Fact]
        public void ReceiveTest()
        {
            f = new Field(1, local, client);
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
