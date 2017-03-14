using System;
using System.Collections.Generic;

namespace Cs_Pong
{
    abstract class Triangle : Circle
    {
        public bool IsOff { get; set; }
        
        public Triangle(float posX = 0, float posY = 0, float __radius = 0, float celX = 0, float celY = 0, float _rotZ = 0, bool _isFixed = false) : base(posX, posY, __radius, 0, 0, 0, celX, celY, _rotZ, _isFixed)
        {
            isFixed = false;
        }

        public override void Step(float dt, float gravityX, float gravityY, List<Rectangle> rects, List<Circle> circs, List<Triangle> trigs, float width, float height)
        {
           if (!isFixed)
            {
                pos[0] += cel[0] * dt;
                pos[1] += cel[1] * dt;

                alpha += rotZ * dt * 180 / ((float)Math.PI);
                if (alpha > 360) alpha -= 360;
                else if (alpha < 360) alpha += 360;
                if (pos[0] < 2 * Radius || pos[0] > width || pos[1] < 2 * Radius || pos[1] > height)
                {
                    IsOff = true;
                }
            }
        }

        public abstract void Effects(Circle circ);
    }
}
