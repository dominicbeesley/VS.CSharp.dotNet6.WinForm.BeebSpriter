using BeebSpriter.Controls;
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
        public PalColourBase PalColour { 
            get
            {
                return this.GetAllDescendants()
                    .OfType<PalColourC>()
                    .Where(c => c.Selected)
                    .FirstOrDefault()?.PalColour ?? new PalColourBBC() { Colour = PalColourBBC.BBCColour.Black };
            } 
            set
            {
                if (value is PALColourNULA)
                {
                    var c = this.GetAllDescendants()
                        .OfType<PalColourC>()
                        .Where(c => c.PalColour is PALColourNULA && c.PalColour.WindowsColour == value.WindowsColour)
                        .FirstOrDefault();
                    if (c == null)
                    {
                        pcNULA0.PalColour = value;
                        Select(pcNULA0);
                    } else
                    {
                        Select(c);
                    }
                } else if (value is PalColourBBC)
                {
                    var vb = value as PalColourBBC;
                    var c = this.GetAllDescendants()
                        .OfType<PalColourC>()
                        .Where(c =>
                            {
                                if (c.PalColour is PalColourBBC)
                                {
                                    var cb = (PalColourBBC)c.PalColour;
                                    return cb.Colour == vb.Colour;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            )
                        .FirstOrDefault();

                    if (c == null)
                        c = pcBBC0;
                    Select(c);
                }

            }
        }

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

            var r = new Random();
            pcNULA0.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA1.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA2.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA3.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA4.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA5.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA6.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA7.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA8.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA9.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA10.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA11.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA12.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA13.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA14.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };
            pcNULA15.PalColour = new PALColourNULA() { Red = r.Next(15), Green = r.Next(15), Blue = r.Next(15) };

        }

        private void pcNULA_DoubleClick(object sender, EventArgs e)
        {
            var pc = sender as PalColourC;
            if (pc == null) return;


            using (var cd = new ColorDialog())
            {
                cd.Color = pc?.PalColour?.WindowsColour ?? Color.Black;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    pc.PalColour = new PALColourNULA() { 
                        Red = cd.Color.R >> 4, 
                        Blue = cd.Color.B >> 4,
                        Green = cd.Color.G >> 4
                    };
                }
            }
        }

        private void pcAny_Click(object sender, EventArgs e)
        {
            Select(sender as PalColourC);
        }

        private void pcAny_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\n' || e.KeyChar == ' ')
                Select(sender as PalColourC);
        }

        private void Select(PalColourC c)
        {
            c.Selected = true;
            this.GetAllDescendants().OfType<PalColourC>().Where(o => o != c).ToList().ForEach(o => { o.Selected = false; });
        }
    }
}
