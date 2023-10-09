using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BeebSpriter
{
    public enum ScreenMode {
        [XmlEnum("0")]
        Mode0,
        [XmlEnum("1")]
        Mode1,
        [XmlEnum("2")]
        Mode2,
        [XmlEnum("4")]
        Mode4,
        [XmlEnum("4")]
        Mode5,
        [XmlEnum("Sprite4")]
        BlitSprite4,
        [XmlEnum("Sprite16")]
        BlitSprite16
    }

}
