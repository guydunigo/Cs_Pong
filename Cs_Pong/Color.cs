using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs_Pong
{
    class Color
    {
        public byte red { get; set; }
        public byte green { get; set; }
        public byte blue { get; set; }

        public Color(byte r, byte g, byte b)
        {
            setRGB(r, g, b);
        }
        public Color(Color other)
        {
            red = other.red;
            green = other.green;
            blue = other.blue;
        }

        public void getRGB(out int r, out int g, out int b)
        {
            r = red; g = green; b = blue;
        }
        public void setRGB(byte r, byte g, byte b)
        {
            red = r; green = g; blue = b;
        }

        public Color Clone()
        {
            return new Color(this);
        }
    }
}
