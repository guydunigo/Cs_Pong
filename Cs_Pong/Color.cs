namespace Cs_Pong
{
    class Color
    {
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public Color(byte r, byte g, byte b)
        {
            SetRGB(r, g, b);
        }
        public Color(Color other)
        {
            Red = other.Red;
            Green = other.Green;
            Blue = other.Blue;
        }

        public void GetRGB(out int r, out int g, out int b)
        {
            r = Red; g = Green; b = Blue;
        }
        public void SetRGB(byte r, byte g, byte b)
        {
            Red = r; Green = g; Blue = b;
        }

        public Color Clone()
        {
            return new Color(this);
        }
    }
}
