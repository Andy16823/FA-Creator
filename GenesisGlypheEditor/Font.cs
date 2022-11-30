using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace GenesisGlypheEditor
{
    public class Font
    {
        public String Name { get; set; }
        public float LetterSpacing { get; set; }
        public int GlypheWidth { get; set; }
        public int GlypheHeight { get; set; }
        public List<Glyphe> Glyphes { get; set; }
        public Bitmap FontAtlas { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }

        public Font()
        {
            this.Glyphes = new List<Glyphe>();
        }

        public void Save(String filename)
        {
            System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
            XmlDeclaration xmlDeclaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = xml.DocumentElement;
            xml.InsertBefore(xmlDeclaration, root);

            XmlElement element = xml.CreateElement("GenesisFont");
            XmlAttribute attribute = xml.CreateAttribute("GlypheWidth");
            attribute.Value = GlypheWidth.ToString();
            element.Attributes.Append(attribute);

            attribute = xml.CreateAttribute("GlypheHeight");
            attribute.Value = GlypheHeight.ToString();
            element.Attributes.Append(attribute);

            attribute = xml.CreateAttribute("LetterSpacing");
            attribute.Value = LetterSpacing.ToString();
            element.Attributes.Append(attribute);

            attribute = xml.CreateAttribute("Columns");
            attribute.Value = Columns.ToString();
            element.Attributes.Append(attribute);

            attribute = xml.CreateAttribute("Rows");
            attribute.Value = Rows.ToString();
            element.Attributes.Append(attribute);

            attribute = xml.CreateAttribute("Name");
            attribute.Value = Name;
            element.Attributes.Append(attribute);

            XmlElement glyphes = xml.CreateElement("Glyphes");
            foreach (var item in this.Glyphes)
            {
                glyphes.AppendChild(item.GetXmlElement(xml));
            }
            element.AppendChild(glyphes);

            XmlElement atlasNode = xml.CreateElement("Atlas");
            atlasNode.InnerText = AtlasToBase64();
            element.AppendChild(atlasNode);

            xml.AppendChild(element);
            xml.Save(filename);
        }

        public String AtlasToBase64()
        {
            System.IO.MemoryStream ms = new MemoryStream();
            FontAtlas.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();
            var base64 = Convert.ToBase64String(byteImage);
            return base64; 
        }

    }
}
