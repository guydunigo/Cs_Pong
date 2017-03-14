using System.Collections.Generic;

namespace Cs_Pong
{
    class Rectangle : Shape
    {
        protected float width, height;
        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                UpdateMass();
            }
        }
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                UpdateMass();
            }
        }
        public bool isCollided;

        public Rectangle(float posX = 0, float posY = 0, float width = 0, float height = 0, byte r = 0, byte g = 0, byte b = 0, float celX = 0, float celY = 0, bool _isFixed = false)
            :base(posX,posY,r,g,b,celX,celY, _isFixed)
        {
            this.width = width;
            this.height = height;
            isCollided = false;
            UpdateMass();
        }

        protected override void UpdateMass()
        {
            Mass = Width * Height * Options.MASS_PER_VOLUME_RECT;
        }

        public override void Step(float dt, float gravityX, float gravityY, List<Rectangle> rects, List<Circle> circs, List<Triangle> trigs, float width, float height)
        {
            if (!isFixed)
            {
                int i = 0;
                float[] norm = { 0, 0 };
                float[] tang = { 0, 0 };
                float a = 0;

                float[] new_pos = pos.Clone() as float[];
                float[] new_cel = cel.Clone() as float[];

                for (i = 0; i< 2; i++)
                {
                    new_pos[i] += new_cel[i] * dt;
                }
                if (new_pos[0] < 0 || new_pos[0] + this.Width > width || new_pos[1] < 0 || new_pos[1] + this.Height > height)
                {
                    if (new_pos[0] < 0)
                    { // Left
                        norm[0] = 1;
                        norm[1] = 0; // tang = [0,-1]
                        new_pos[0] = 0;
                    }
                    else if (new_pos[0] + this.Width > width)
                    { // Right
                        norm[0] = -1;
                        norm[1] = 0; // tang = [0, 1]
                        new_pos[0] = width - this.Width;
                    }
                    else
                    {
                        norm[0] = 0;
                        norm[1] = 0;
                    }
                    if (new_pos[1] < 0)
                    { // Top
                        norm[0] += 0;
                        norm[1] += 1; // tang = [1, 0]
                        new_pos[1] = 0;
                    }
                    else if (new_pos[1] + this.Height > height)
                    { // Bottom
                        norm[0] += 0;
                        norm[1] += -1; // tang = [-1,0]
                        new_pos[1] = height - this.Height;
                    }

                    GetTang(norm, out tang);

                    a = (-1) * Options.BOUNCE_COEF * (new_cel[0] * norm[0] + new_cel[1] * norm[1]);
                    for (i = 0; i < 2; i++)
                    {
                        new_cel[i] = a * norm[i];
                    }
                }


                // Actually updates values
                cel = new_cel;
                pos = new_pos;
            }
        }
    }
}
