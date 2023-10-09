using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using static BeebSpriter.PalColourBBC;

namespace BeebSpriter
{
    public class PALColourNULA : PalColourBase, IXmlSerializable
    {

        byte _red;
        public byte Red { 
            get => _red; 
            init => _red = lim(value); 
        }

        byte _green;
        public byte Green { 
            get => _green; 
            init => _green = lim(value); 
        }

        byte _blue;
        public byte Blue {
            get => _blue;
            init => _blue = lim(value); 
        }

        
        public override Color WindowsColour
        {
            get
            {
                return Color.FromArgb(exp(Red), exp(Green), exp(Blue));
            }
        }

        public override Color WindowsColourFlash
        {
            get
            {
                return WindowsColour;
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        static Regex reCol = new Regex("^#([0-9A-F]{3})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public void ReadXml(XmlReader reader)
        {
            var s = reader.ReadContentAsString().Trim();
            var m = reCol.Match(s);
            if (m.Success)
            {
                _red = byte.Parse(m.Captures[1].Value.Substring(0, 1), NumberStyles.HexNumber);
                _green = byte.Parse(m.Captures[1].Value.Substring(1, 1), NumberStyles.HexNumber);
                _blue = byte.Parse(m.Captures[1].Value.Substring(2, 1), NumberStyles.HexNumber);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("ColourNULA");
            writer.WriteString(this.ToString());
            writer.WriteEndElement();
        }


        public override string ToString()
        {
            return $"#{(Red & 0xF):X1}{(Green & 0xF):X1}{(Blue & 0xF):X1}";
        }

        private static byte lim(byte v)
        {
            return (byte)(v <= 0 ? 0 : v >= 15 ? 15 : v);
        }

        private int exp(byte v)
        {
            return v * 16 | (v & 1) * 15;
        }
    }

}
