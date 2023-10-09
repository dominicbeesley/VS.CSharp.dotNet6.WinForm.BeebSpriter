using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;

namespace BeebSpriter
{
    public class SpriteSheet
    {
        #region Private members

        public enum BackColour
        {
            Transparent,
            Black,
            Red,
            Green,
            Yellow,
            Blue,
            Magenta,
            Cyan,
            White
        };

        private ScreenMode mode;
        private int xScale;
        private int yScale;
        private int bitsPerPixel;
        private List<Sprite> spriteList = new List<Sprite>();
        private BackColour backgroundColour = BackColour.Transparent;
        private PalColourBase[] defaultPalette;
        private bool defaultShowGridLines = true;
        private int horizontalBlockDividers = 0;
        private int verticalBlockDividers = 0;
        private int defaultZoom = 6;

        // Members relating to export options

        public enum SpriteDataLayout
        {
            RowMajor,
            ColumnMajor,
            Linear
        };

        private bool hasSetExportSettings = false;
        private SpriteDataLayout spriteLayout = SpriteDataLayout.RowMajor;
        private bool shouldBreakSprites = false;
        private int subSpriteWidth = 2;
        private int subSpriteHeight = 2;
        private bool shouldExportSeparateMask = false;
        private bool shouldGenerateHeader = true;
        private string assemblerSyntax = "{n} = &{v}";

        #endregion


        [XmlAttribute]
        public ScreenMode Mode
        {
            get
            {
                return this.mode;
            }

            set
            {
                this.mode = value;
                this.yScale = 2;

                switch (this.mode)
                {
                    case ScreenMode.Mode0:
                        this.xScale = 1;
                        this.bitsPerPixel = 1;
                        break;

                    case ScreenMode.Mode1:
                        this.xScale = 2;
                        this.bitsPerPixel = 2;
                        break;

                    case ScreenMode.Mode2:
                        this.xScale = 4;
                        this.bitsPerPixel = 4;
                        break;

                    case ScreenMode.Mode4:
                        this.xScale = 2;
                        this.bitsPerPixel = 1;
                        break;

                    case ScreenMode.Mode5:
                        this.xScale = 4;
                        this.bitsPerPixel = 2;
                        break;

                    case ScreenMode.BlitSprite4:
                        this.xScale = 2;
                        this.bitsPerPixel = 2;
                        break;

                    case ScreenMode.BlitSprite16:
                        this.xScale = 2;
                        this.bitsPerPixel = 4;
                        break;

                    default:
                        throw new Exception("Bad Mode (" + mode + ")");
                }
            }
        }

        public int XScale
        {
            get { return this.xScale; }
        }

        public int YScale
        {
            get { return this.yScale; }
        }

        public int NumColours
        {
            get { return 1 << this.bitsPerPixel; }
        }

        public int PixelsPerByte
        {
            get { return 8 / this.bitsPerPixel; }
        }

        public int BitsPerPixel
        {
            get { return this.bitsPerPixel; }
        }

        public BackColour BackgroundColour
        {
            get { return this.backgroundColour; }
            set { this.backgroundColour = value; }
        }

        [XmlArray(),
         XmlArrayItem(typeof(PalColourBBC), ElementName = "Colour"),
         XmlArrayItem(typeof(PALColourNULA), ElementName = "ColourNULA"),
        ]
        public PalColourBase[] DefaultPalette
        {
            get { return this.defaultPalette; }
            set { this.defaultPalette = value; }
        }

        public bool DefaultShowGridLines
        {
            get { return this.defaultShowGridLines; }
            set { this.defaultShowGridLines = value; }
        }

        public int HorizontalBlockDividers
        {
            get { return this.horizontalBlockDividers; }
            set { this.horizontalBlockDividers = value; }
        }

        public int VerticalBlockDividers
        {
            get { return this.verticalBlockDividers; }
            set { this.verticalBlockDividers = value; }
        }

        public int DefaultZoom
        {
            get { return this.defaultZoom; }
            set { this.defaultZoom = value; }
        }

        public bool HasSetExportSettings
        {
            get { return this.hasSetExportSettings; }
            set { this.hasSetExportSettings = value; }
        }

        public SpriteDataLayout SpriteLayout
        {
            get { return this.spriteLayout; }
            set { this.spriteLayout = value; }
        }

        public bool ShouldBreakSprites
        {
            get { return this.shouldBreakSprites; }
            set { this.shouldBreakSprites = value; }
        }

        public int SubSpriteWidth
        {
            get { return this.subSpriteWidth; }
            set { this.subSpriteWidth = value; }
        }

        public int SubSpriteHeight
        {
            get { return this.subSpriteHeight; }
            set { this.subSpriteHeight = value; }
        }

        public bool ShouldExportSeparateMask
        {
            get { return this.shouldExportSeparateMask; }
            set { this.shouldExportSeparateMask = value; }
        }

        public bool ShouldGenerateHeader
        {
            get { return this.shouldGenerateHeader; }
            set { this.shouldGenerateHeader = value; }
        }

        public string AssemblerSyntax
        {
            get { return this.assemblerSyntax; }
            set { this.assemblerSyntax = value; }
        }


        public List<Sprite> SpriteList
        {
            get { return this.spriteList; }
            set { this.spriteList = value; }
        }



        public SpriteSheet()
        {
        }

        public SpriteSheet(ScreenMode mode)
        {
            this.Mode = mode;
            this.DefaultPalette = new PalColourBase[this.NumColours];

            switch (this.NumColours)
            {
                case 2:
                    this.DefaultPalette[0] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Black };
                    this.DefaultPalette[1] = new PalColourBBC { Colour = PalColourBBC.BBCColour.White };
                    break;

                case 4:
                    this.DefaultPalette[0] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Black };
                    this.DefaultPalette[1] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Red };
                    this.DefaultPalette[2] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Yellow };
                    this.DefaultPalette[3] = new PalColourBBC { Colour = PalColourBBC.BBCColour.White };
                    break;

                case 16:
                    this.DefaultPalette[0] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Black };
                    this.DefaultPalette[1] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Red };
                    this.DefaultPalette[2] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Green };
                    this.DefaultPalette[3] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Yellow };
                    this.DefaultPalette[4] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Blue };
                    this.DefaultPalette[5] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Magenta };
                    this.DefaultPalette[6] = new PalColourBBC { Colour = PalColourBBC.BBCColour.Cyan };
                    this.DefaultPalette[7] = new PalColourBBC { Colour = PalColourBBC.BBCColour.White };
                    this.DefaultPalette[8] = new PalColourBBC { Colour = PalColourBBC.BBCColour.BlackWhite };
                    this.DefaultPalette[9] = new PalColourBBC { Colour = PalColourBBC.BBCColour.RedCyan };
                    this.DefaultPalette[10] = new PalColourBBC { Colour = PalColourBBC.BBCColour.GreenMagenta };
                    this.DefaultPalette[11] = new PalColourBBC { Colour = PalColourBBC.BBCColour.YellowBlue };
                    this.DefaultPalette[12] = new PalColourBBC { Colour = PalColourBBC.BBCColour.BlueYellow };
                    this.DefaultPalette[13] = new PalColourBBC { Colour = PalColourBBC.BBCColour.MagentaGreen };
                    this.DefaultPalette[14] = new PalColourBBC { Colour = PalColourBBC.BBCColour.CyanRed };
                    this.DefaultPalette[15] = new PalColourBBC { Colour = PalColourBBC.BBCColour.WhiteBlack };
                    break;
            }
        }

    }
}
