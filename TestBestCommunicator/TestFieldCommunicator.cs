using System.Linq;

using NUnit.Framework;
using Should.Fluent;

using BEST2014;
using System.Net;
using System.Xml.Linq;
using System.IO;

namespace TestBestCommunicator
{
    [TestFixture]
    public class TestFieldCommunicator
    {
        private FieldCommunicator fc;
        private MockField[] fields = new MockField[4];
        private static string xmlPath = "Resources/FieldStateValid.xml";

        private XElement fieldStateElement;
        private static XElement baseFieldStateElement =
            XElement.Parse(File.ReadAllText(xmlPath));

        [SetUp]
        public void BeforeEach()
        {
            fieldStateElement = baseFieldStateElement;
                
            fc = new FieldCommunicator();

            for(int i = 0; i < 4; i++)
            {
                fields[i] = new MockField();
                fields[i].Id = i + 1;
                fields[i].Address = IPAddress.Parse("127.0.0." + i);
                fc.AddField(fields[i]);
            }
        }

        [Test]
        public void CountTest()
        {
            fc.Count.Should().Equal(4);
        }

        [Test]
        public void ReadTest()
        {
            int i = 1;
            foreach (var field in fields)
            {
                fieldStateElement.Elements().Where(e => e.Name.LocalName == "TIEBREAK")
                    .Elements().Where(e => e.Name.LocalName == "BLUE").First().Value =
                        i.ToString();
                field.QueryState = new FieldState(fieldStateElement.ToString());
                FieldState actualState = fc.ReadField(i++);
                actualState.ToString().Should().Equal(field.QueryState.ToString());
            }
        }

        [Test]
        public void ResetTest()
        {
            foreach (var field in fields)
            {
                fc.ResetField(field.Id);
                field.TimesResetCalled.Should().Equal(1);
            }
        }

        [Test]
        public void RemoveByAddressTest()
        {
            foreach (var field in fields)
            {
                var address = field.Address;
                var originalCount = fc.Count;
                fc.RemoveField(address.ToString());
                fc.Count.Should().Equal(originalCount - 1);
            }            
        }
    }
}
