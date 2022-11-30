using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GenesisGlypheEditor
{
    public class Glyphe
    {
        public char Char { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        public Glyphe(Char character, int col, int row) 
        {
            this.Char = character;
            this.Column = col;
            this.Row = row;
        }

        public XmlElement GetXmlElement(XmlDocument xml)
        {
            XmlElement element = xml.CreateElement("Glyphe");
            
            XmlAttribute attribute = xml.CreateAttribute("Char");
            attribute.Value = Char.ToString();
            element.Attributes.Append(attribute);

            attribute = xml.CreateAttribute("Column");
            attribute.Value = Column.ToString();
            element.Attributes.Append(attribute);

            attribute = xml.CreateAttribute("Row");
            attribute.Value = Row.ToString();
            element.Attributes.Append(attribute);

            return element;
        }

    }
}
