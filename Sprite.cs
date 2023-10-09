﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BeebSpriter
{
    public class Sprite
    {
        private string name;
        private int width;
        private int height;
        private byte[] bitmap;
        private PalColourBase[] palette;
        private SpritePanel spritePanel;

        [XmlAttribute]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        [XmlAttribute]
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        [XmlAttribute]
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public byte[] Bitmap
        {
            get { return this.bitmap; }
            set { this.bitmap = value; }
        }

        [XmlArrayItem(),
         XmlArrayItem(typeof(PalColourBBC), ElementName="Colour"),
         XmlArrayItem(typeof(PALColourNULA), ElementName = "ColourNULA"),
         ]
        public PalColourBase[] Palette
        {
            get { return this.palette; }
            set { this.palette = value; }
        }

        /// <summary>
        /// Get the number of colours used by the sprite
        /// </summary>
        public int NumColours => Palette.Length;

        public SpritePanel SpritePanel
        {
            get { return this.spritePanel; }
        }

        public Sprite()
        {
        }

        public Sprite(string name, int width, int height, PalColourBase[] palette)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Bitmap = new byte[this.Width * this.Height];
            this.Palette = (PalColourBase[])palette.Clone();
        }

        public Sprite(string name, Sprite toCopy)
        {
            this.Name = name;
            this.Width = toCopy.Width;
            this.Height = toCopy.Height;
            this.Bitmap = (byte[])toCopy.Bitmap.Clone();
            this.Palette = (PalColourBase[])toCopy.Palette.Clone();
        }

        public void SetSpritePanel(SpritePanel spritePanel)
        {
            this.spritePanel = spritePanel;
        }

        public override string ToString()
        {
            if (Name == "")
            {
                return "<Unnamed sprite>";
            }
            else
            {
                return Name;
            }
        }

        /// <summary>
        /// Rotate Image Clockwise 90
        /// </summary>        
        public void rotateClockWise()
        {
            byte[] clonedImage = (byte[])this.Bitmap.Clone();

            int index = 0;
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = this.Height - 1; y >= 0; y--)
                {
                    this.Bitmap[index++] = clonedImage[y * Width + x];
                }
            }

            int tempVar = this.Width;
            this.Width = this.Height;
            this.Height = tempVar;
        }

        /// <summary>
        /// Rotate Image Anti Clockwise 90
        /// </summary>        
        public void rotateAntiClockWise()
        {
            byte[] clonedImage = (byte[])this.Bitmap.Clone();

            int index = 0;
            for (int x = this.Width - 1; x >=0; x--)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this.Bitmap[index++] = clonedImage[y * Width + x];
                }
            }

            int tempVar = this.Width;
            this.Width = this.Height;
            this.Height = tempVar;
        }

        /// <summary>
        /// Replace one colour with another in the sprite image
        /// </summary>
        /// <param name="oldColour"></param>
        /// <param name="newColour"></param>
        public void ReplaceColours(int oldColour, int newColour)
        {
            for (int index = 0; index < Bitmap.Length; index++)
            {
                if (Bitmap[index] == (byte)oldColour) Bitmap[index] = (byte)newColour;
            }
        }

        /// <summary>
        /// Make all colours negative in the sprite image
        /// </summary>
        /// <param name="colourDepth"></param>
        public void Negative(int colourDepth)
        {
            // 255 = Transparent Colour

            for (int index = 0; index < Bitmap.Length; index++)
            {
                if (Bitmap[index] != 255) 
                    Bitmap[index] ^= (byte)(colourDepth - 1);
            }
        }
    }
}
