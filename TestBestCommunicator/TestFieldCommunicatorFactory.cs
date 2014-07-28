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
using System.Xml.Linq;
using System.IO;

namespace TestBestCommunicator
{
    [TestFixture]
    public class TestFieldCommunicatorFactory
    {
        private IFieldCommunicator fc;
        private IFieldCommunicatorFactory factory;

        [SetUp]
        public void BeforeEach()
        {
            factory = new FieldCommunicatorFactory();
        }

        [Test]
        public void TestCreate()
        {
            fc = factory.Create();
            fc.Should().Be.OfType<FieldCommunicator>();
        }

        [Test]
        public void TestCreateWithIPAddresses()
        {
            IPAddress[] addresses = new IPAddress[4];

            for(int i = 1; i <= 4; i++)
            {
                addresses[i - 1] =
                    IPAddress.Parse("192.168.1." + i.ToString());
            }

            fc = factory.CreateWithIPAddresses(
                addresses);

            fc.Should().Be.OfType<FieldCommunicator>();

            fc.Count.Should().Equal(4);

            for (int i = 3; i >= 0; i--)
            {
                var field = fc.PopField();
                field.Address.Should().Equal(addresses[i]);
            }
        }

        [Test]
        public void TestCreateWithStrings()
        {
            string[] addresses = new string[4];

            for(int i = 1; i <= 4; i++)
            {
                addresses[i - 1] =
                    "192.168.1." + i.ToString();
            }

            fc = factory.CreateWithIPAddresses(
                addresses);

            fc.Should().Be.OfType<FieldCommunicator>();

            fc.Count.Should().Equal(4);

            for (int i = 3; i >= 0; i--)
            {
                var field = fc.PopField();
                field.Address.ToString().Should().Equal(addresses[i]);
            }
        }
    }
}
