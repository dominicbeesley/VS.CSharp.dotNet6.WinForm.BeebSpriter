using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeebSpriter
{
    public partial class PALPicker : Form
    {
        public PalColourBase PalColour { get; set; }

        public PALPicker()
        {
            InitializeComponent();

            pcBBC0.PalColour = PalColourBBC.Defaults[0];
            pcBBC1.PalColour = PalColourBBC.Defaults[1];
            pcBBC2.PalColour = PalColourBBC.Defaults[2];
            pcBBC3.PalColour = PalColourBBC.Defaults[3];
            pcBBC4.PalColour = PalColourBBC.Defaults[4];
            pcBBC5.PalColour = PalColourBBC.Defaults[5];
            pcBBC6.PalColour = PalColourBBC.Defaults[6];
            pcBBC7.PalColour = PalColourBBC.Defaults[7];
            pcBBC8.PalColour = PalColourBBC.Defaults[8];
            pcBBC9.PalColour = PalColourBBC.Defaults[9];
            pcBBC10.PalColour = PalColourBBC.Defaults[10];
            pcBBC11.PalColour = PalColourBBC.Defaults[11];
            pcBBC12.PalColour = PalColourBBC.Defaults[12];
            pcBBC13.PalColour = PalColourBBC.Defaults[13];
            pcBBC14.PalColour = PalColourBBC.Defaults[14];
            pcBBC15.PalColour = PalColourBBC.Defaults[15];
        }

    }
}
