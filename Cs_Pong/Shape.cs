using System.Collections.Generic;

namespace Cs_Pong
{
    abstract class Shape
    {
        public bool isFixed;
        public float[] pos;
        public float X {
            get
            {
                return pos[0];
            }
            set
            {
                pos[0] = value;
            }
        }
        public float Y
        {
            get
            {
                return pos[1];
            }
            set
            {
                pos[1] = value;
            }
        }
        public float[] cel;
        public float CelX
        {
            get
            {
                return cel[0];
            }
            set
            {
                cel[0] = value;
            }
        }
        public float CelY
        {
            get
            {
                return cel[1];
            }
            set
            {
                cel[0] = value;
            }
        }
        public float Mass { get; set; }
        public Color Color;
        public Shape(float posX, float posY, byte r, byte g, byte b, float celX, float celY, bool _isFixed = false)
        {
            isFixed = false;
            pos = new float[]{ posX, posY };
            cel = new float[]{ celX, celY };
            Mass = 42;
            Color = new Color(r, g, b);
        }
        
        protected static void GetTang(float[] norm, out float[] tang)
        {
            tang = new float[] { norm[1], -norm[0] };
        }

        protected abstract void UpdateMass();
        public void Move(float dx, float dy)
        {
            pos[0] += dx;pos[1] += dy;
        }
        public abstract void Step(float dt, float gravityX, float gravityY, List<Rectangle> rects, List<Circle> circs, List<Triangle> trigs, float width, float height);
    }
}
