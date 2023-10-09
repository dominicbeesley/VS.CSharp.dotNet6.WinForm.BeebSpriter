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

        int _red;
        public int Red { 
            get => _red; 
            init => _red = lim(value); 
        }

        int _green;
        public int Green { 
            get => _green; 
            init => _green = lim(value); 
        }

        int _blue;
        public int Blue {
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
            var s = reader.ReadElementContentAsString().Trim();
            var m = reCol.Match(s);
            if (m.Success)
            {
                _red = int.Parse(m.Groups[1].Value.Substring(0, 1), NumberStyles.HexNumber);
                _green = int.Parse(m.Groups[1].Value.Substring(1, 1), NumberStyles.HexNumber);
                _blue = int.Parse(m.Groups[1].Value.Substring(2, 1), NumberStyles.HexNumber);
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(this.ToString());
        }


        public override string ToString()
        {
            return $"#{(Red & 0xF):X1}{(Green & 0xF):X1}{(Blue & 0xF):X1}";
        }

        private static int lim(int v)
        {
            return v <= 0 ? 0 : v >= 15 ? 15 : v;
        }

        private int exp(int v)
        {
            return v * 16 | (v & 1) * 15;
        }
    }

}
