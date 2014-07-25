using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace BEST2014
{
    public class Quadrant
    {
        public Quadrant(int rank, bool isOn)
        {
            Rank = rank;
            IsSwitchOn = isOn;
        }

        public int Rank { get; private set; }
        public bool IsSwitchOn { get; private set; }
    }

    public class FieldState
    {
        private const int yellowIndex = 0;
        private const int redIndex = 1;
        private const int blueIndex = 2;
        private const int greenIndex = 3;

        public bool IsConfigured { get; private set; }
        private Quadrant[] Quadrants = new Quadrant[4];

        public Quadrant Yellow { get { return Quadrants[yellowIndex]; } }
        public Quadrant Red { get { return Quadrants[redIndex]; } }
        public Quadrant Blue { get { return Quadrants[blueIndex]; } }
        public Quadrant Green { get { return Quadrants[greenIndex]; } }

        private XElement fieldElement;
        private XElement tieBreak;
        private XElement swStatus;
        private XElement devStatus;

        public FieldState(string fieldXml)
        {
            try
            {
                fieldElement = XElement.Parse(fieldXml);
                
                if(fieldElement.Name.LocalName != "DATA") // Doesn't match protocol
                {
                    throw (new XmlException());
                }

                devStatus = getElementByName("DEVSTATUS");
                if (isDevStatusConfigured())
                {
                    IsConfigured = true;
                }
                else
                {
                    throw (new XmlException());
                }

                tieBreak = getElementByName("TIEBREAK");
                swStatus = getElementByName("SWSTATUS");

                Quadrants[yellowIndex] = getQuadrantByColor("YELLOW");
                Quadrants[redIndex] = getQuadrantByColor("RED");
                Quadrants[blueIndex] = getQuadrantByColor("BLUE");
                Quadrants[greenIndex] = getQuadrantByColor("GREEN");
            }
            catch(XmlException)
            {
                IsConfigured = false;
            }
        }

        private XElement getElementByName(string name)
        {
            XElement el = fieldElement.Elements()
                                        .FirstOrDefault(e => e.Name.LocalName == name);

            if (el == null)
            {
                throw (new XmlException());
            }

            return el;
        }

        private bool isDevStatusConfigured()
        {
            return int.Parse(
                     devStatus.Elements()
                       .First(e => e.Name.LocalName == "CONFIG")
                       .Value) > 0;
        }

        private Quadrant getQuadrantByColor(string color)
        {
            int rank = int.Parse(tieBreak.Elements()
                         .First(e => e.Name.LocalName == color)
                         .Value);

            bool isOn = int.Parse(swStatus.Elements()
                          .First(e => e.Name.LocalName == color)
                          .Value) > 0;

            return new Quadrant(rank, isOn);
        }

        public override string ToString()
        {
            if (IsConfigured)
            {
                return fieldElement.ToString();
            }
            else
            {
                return "<DATA>\r\n\t<DEVSTATUS>\r\n\t\t<CONFIG>0</CONFIG>" +
                       "\r\n\t</DEVSTATUS>\r\n</DATA>";
            }
        }
    }
}
