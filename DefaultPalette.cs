﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BeebSpriter.Controls;

namespace BeebSpriter
{
    public partial class DefaultPalette : Form
    {
        PalColourC[] colourPanels;
        PalColourBase[] defaultPalette;


        public DefaultPalette()
        {
            InitializeComponent();

            // Make an array to look up colour panels by index
            colourPanels = new PalColourC[16]
            {
                button_colour0,
                button_colour1,
                button_colour2,
                button_colour3,
                button_colour4,
                button_colour5,
                button_colour6,
                button_colour7,
                button_colour8,
                button_colour9,
                button_colour10,
                button_colour11,
                button_colour12,
                button_colour13,
                button_colour14,
                button_colour15
            };

            SpriteSheet spriteSheet = SpriteSheetForm.Instance.SpriteSheet;

            switch (spriteSheet.NumColours)
            {
                case 2:
                    for (int i = 2; i < 16; i++)
                    {
                        colourPanels[i].Visible = false;
                    }
                    // this.Size = new Size(184, 111);             - Original line
                    this.Size = new Size(220, 130);
                    break;

                case 4:
                    for (int i = 4; i < 16; i++)
                    {
                        colourPanels[i].Visible = false;
                    }
                    // this.Size = new Size(184, 111);             - Original line
                    this.Size = new Size(220, 130);
                    break;
            }

            defaultPalette = (PalColourBase[])spriteSheet.DefaultPalette.Clone();

            for (int i = 0; i < spriteSheet.NumColours; i++)
            {
                colourPanels[i].PalColour = defaultPalette[i];
                colourPanels[i].MouseClick += new MouseEventHandler(DefaultPalette_MouseClick);
            }

        }


        void DefaultPalette_MouseClick(object sender, MouseEventArgs e)
        {
            SpriteSheet spriteSheet = SpriteSheetForm.Instance.SpriteSheet;
            PalColourC panel = sender as PalColourC;
            if (panel != null)
            {
                int index = panel.TabIndex;

                using (var f = new PALPicker())
                {
                    f.PalColour = defaultPalette[index];
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        defaultPalette[index] = f.PalColour;
                        colourPanels[index].PalColour = f.PalColour;
                    }
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            SpriteSheetForm.Instance.SpriteSheet.DefaultPalette = (PalColourBase[])defaultPalette.Clone();
        }

        private void DefaultPalette_Load(object sender, EventArgs e)
        {

        }

        private void button_colour1_Load(object sender, EventArgs e)
        {

        }
    }
}
