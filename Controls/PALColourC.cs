using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeebSpriter.Controls
{
    public partial class PalColourC : UserControl
    {
        PalColourBase _colour;
        public PalColourBase PalColour
        {
            get => _colour;
            set
            {
                _colour = value;
                Invalidate();
            }
        }

        public PalColourC()
        {
            InitializeComponent();
        }

        private void PALColourC_Paint(object sender, PaintEventArgs e)
        {
            using (var b = new SolidBrush(PalColour?.WindowsColour ?? Color.Gray))
            using (var p = new SolidBrush(PalColour?.WindowsColourFlash ?? Color.Black))
            {
                e.Graphics.FillRectangle(b, this.ClientRectangle);
                string t = PalColour == null ? "?" : "*";
                e.Graphics.DrawString(t, this.Font, p, new PointF(0, 0));
            }

        }
    }
}
