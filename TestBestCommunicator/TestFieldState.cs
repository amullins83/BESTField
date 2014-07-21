using System;
using NUnit.Framework;
using Should.Fluent;
using System.IO;

using BEST2014;

namespace TestBestCommunicator
{
    [TestFixture]
    public class TestFieldState
    {
        [Test]
        public void ParseValidXmlTest()
        {
            string xmlFileContents =
                File.ReadAllText("Resources/FieldStateValid.xml");
            
            FieldState fs = new FieldState(xmlFileContents);

            fs.IsConfigured.Should().Be.True();
            fs.Blue.Rank.Should().Equal(1);
        }
    }
}
