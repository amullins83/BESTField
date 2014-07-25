using System;
using NUnit.Framework;
using Should.Fluent;
using System.IO;

using BEST2014;

using System.Xml.Linq;

namespace TestBestCommunicator
{
    [TestFixture]
    public class TestFieldState
    {
        private string validXmlFileContents =
            File.ReadAllText("Resources/FieldStateValid.txt");
        private string invalidXmlFileContents =
            File.ReadAllText("Resources/FieldStateInvalid.txt");
        private string notConfigXmlFileContents =
            File.ReadAllText("Resources/FieldStateNotConfigured.txt");

        [Test]
        public void ParseValidXmlTest()
        {
            FieldState fs = new FieldState(validXmlFileContents);

            fs.IsConfigured.Should().Be.True();
            fs.Blue.Rank.Should().Equal(1);
        }

        [Test]
        public void ParseInvalidXmlTest()
        {
            FieldState fs = new FieldState(invalidXmlFileContents);
            fs.IsConfigured.Should().Be.False();
        }

        [Test]
        public void ParseNotConfiguredTest()
        {
            FieldState fs = new FieldState(notConfigXmlFileContents);
            fs.IsConfigured.Should().Be.False();
        }

        [Test]
        public void ToStringTest()
        {
            FieldState fs = new FieldState(validXmlFileContents);

            fs.ToString().Should().Equal(
                XElement.Parse(validXmlFileContents).ToString());
        }

        [Test]
        public void InvalidToStringTest()
        {
            FieldState fs = new FieldState("");

            fs.ToString().Should().Equal(
                "<DATA>\r\n\t<DEVSTATUS>\r\n\t\t<CONFIG>0</CONFIG>" +
                       "\r\n\t</DEVSTATUS>\r\n</DATA>");
        }
    }
}
