using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BeebSpriter
{
    public class PalColourBBC : PalColourBase, IXmlSerializable
    {

        private static PalColourBBC[] _defaults = new PalColourBBC[]
        {
            new PalColourBBC { Colour = BBCColour.Black },
            new PalColourBBC { Colour = BBCColour.Red },
            new PalColourBBC { Colour = BBCColour.Green },
            new PalColourBBC { Colour = BBCColour.Yellow },
            new PalColourBBC { Colour = BBCColour.Blue },
            new PalColourBBC { Colour = BBCColour.Magenta },
            new PalColourBBC { Colour = BBCColour.Cyan },
            new PalColourBBC { Colour = BBCColour.White },
            new PalColourBBC { Colour = BBCColour.BlackWhite },
            new PalColourBBC { Colour = BBCColour.RedCyan },
            new PalColourBBC { Colour = BBCColour.GreenMagenta },
            new PalColourBBC { Colour = BBCColour.YellowBlue },
            new PalColourBBC { Colour = BBCColour.BlueYellow },
            new PalColourBBC { Colour = BBCColour.MagentaGreen },
            new PalColourBBC { Colour = BBCColour.CyanRed },
            new PalColourBBC { Colour = BBCColour.WhiteBlack }
        };
        public static PalColourBBC[] Defaults { get => _defaults; }

        public enum BBCColour
        {
            Black = 0,
            Red = 1,
            Green = 2,
            Yellow = 3,
            Blue = 4,
            Magenta = 5,
            Cyan = 6,
            White = 7,
            BlackWhite = 8,
            RedCyan = 9,
            GreenMagenta = 10,
            YellowBlue = 11,
            BlueYellow = 12,
            MagentaGreen = 13,
            CyanRed = 14,
            WhiteBlack = 15
        }

        BBCColour _colour;
        public BBCColour Colour { 
            get => _colour;
            init => _colour = value; 
        }

        public override Color WindowsColour
        {
            get
            {
                switch (Colour)
                {
                    case BBCColour.Black: return Color.FromArgb(0, 0, 0);
                    case BBCColour.Red: return Color.FromArgb(255, 0, 0);
                    case BBCColour.Green: return Color.FromArgb(0, 255, 0);
                    case BBCColour.Yellow: return Color.FromArgb(255, 255, 0);
                    case BBCColour.Blue: return Color.FromArgb(0, 0, 255);
                    case BBCColour.Magenta: return Color.FromArgb(255, 0, 255);
                    case BBCColour.Cyan: return Color.FromArgb(0, 255, 255);
                    case BBCColour.White: return Color.FromArgb(255, 255, 255);
                    case BBCColour.BlackWhite: return Color.FromArgb(0, 0, 0);
                    case BBCColour.RedCyan: return Color.FromArgb(255, 0, 0);
                    case BBCColour.GreenMagenta: return Color.FromArgb(0, 255, 0);
                    case BBCColour.YellowBlue: return Color.FromArgb(255, 255, 0);
                    case BBCColour.BlueYellow: return Color.FromArgb(0, 0, 255);
                    case BBCColour.MagentaGreen: return Color.FromArgb(255, 0, 255);
                    case BBCColour.CyanRed: return Color.FromArgb(0, 255, 255);
                    case BBCColour.WhiteBlack: return Color.FromArgb(255, 255, 255);
                    default: throw new Exception("Unknown colour");
                }
            }
        }
        public override Color WindowsColourFlash
        {
            get
            {
                switch (Colour)
                {
                    case BBCColour.Black: return Color.FromArgb(0, 0, 0);
                    case BBCColour.Red: return Color.FromArgb(255, 0, 0);
                    case BBCColour.Green: return Color.FromArgb(0, 255, 0);
                    case BBCColour.Yellow: return Color.FromArgb(255, 255, 0);
                    case BBCColour.Blue: return Color.FromArgb(0, 0, 255);
                    case BBCColour.Magenta: return Color.FromArgb(255, 0, 255);
                    case BBCColour.Cyan: return Color.FromArgb(0, 255, 255);
                    case BBCColour.White: return Color.FromArgb(255, 255, 255);
                    case BBCColour.WhiteBlack: return Color.FromArgb(0, 0, 0);
                    case BBCColour.CyanRed: return Color.FromArgb(255, 0, 0);
                    case BBCColour.MagentaGreen: return Color.FromArgb(0, 255, 0);
                    case BBCColour.BlueYellow: return Color.FromArgb(255, 255, 0);
                    case BBCColour.YellowBlue: return Color.FromArgb(0, 0, 255);
                    case BBCColour.GreenMagenta: return Color.FromArgb(255, 0, 255);
                    case BBCColour.RedCyan: return Color.FromArgb(0, 255, 255);
                    case BBCColour.BlackWhite: return Color.FromArgb(255, 255, 255);
                    default: throw new Exception("Unknown colour");
                }
            }
        }

        public override string ToString()
        {
            return Colour.ToString();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            this._colour = (BBCColour)System.Enum.Parse(typeof(BBCColour), reader.ReadElementContentAsString().Trim());
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(Colour.ToString());
        }
    }

}
