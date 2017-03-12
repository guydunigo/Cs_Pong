using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public float celX
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
        public float celY
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
        public float mass;
        public Color color;
        public Shape(float posX, float posY, byte r, byte g, byte b, float celX, float celY, bool _isFixed = false)
        {
            isFixed = false;
            pos = new float[]{ posX, posY };
            cel = new float[]{ celX, celY };
            mass = 42;
            color = new Color(r, g, b);
        }
        
        protected static void getTang(float[] norm, out float[] tang)
        {
            tang = new float[] { norm[1], -norm[0] };
        }

        protected abstract void updateMass();
        public void move(float dx, float dy)
        {
            pos[0] += dx;pos[1] += dy;
        }
        public abstract void step(float dt, float gravityX, float gravityY, List<Rectangle> rects, List<Circle> circs, List<Triangle> trigs, float width, float height);
    }
}
