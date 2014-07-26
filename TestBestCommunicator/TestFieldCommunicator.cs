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
    public class TestFieldCommunicator
    {
        private FieldCommunicator fc;
        private MockField[] fields = new MockField[4];
        private static string xmlPath = "Resources/FieldStateValid.xml";

        private XElement fieldStateElement;
        private static XElement baseFieldStateElement =
            XElement.Parse(File.ReadAllText(xmlPath));

        [SetUp]
        public void beforeEach()
        {
            fieldStateElement = baseFieldStateElement;
                
            fc = new FieldCommunicator();

            for(int i = 0; i < 4; i++)
            {
                fields[i] = new MockField();
                fields[i].Id = i + 1;
                fc.AddField(fields[i]);
            }
        }

        [Test]
        public void countTest()
        {
            fc.Count.Should().Equal(4);
        }

        [Test]
        public void readTest()
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
        public void resetTest()
        {
            foreach (var field in fields)
            {
                fc.ResetField(field.Id);
                field.TimesResetCalled.Should().Equal(1);
            }
        }
    }
}
