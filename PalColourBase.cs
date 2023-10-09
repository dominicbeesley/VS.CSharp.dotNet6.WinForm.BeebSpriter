using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BeebSpriter
{
    public abstract class PalColourBase
    {
        [XmlIgnore]
        public abstract Color WindowsColour { get; }
        [XmlIgnore]
        public abstract Color WindowsColourFlash { get; }

        public override abstract string ToString();
    }



}
