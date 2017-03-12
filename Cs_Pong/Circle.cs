using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs_Pong
{
    class Circle : Shape
    {
        protected float _radius;
        public float radius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                updateMass();
            }
        }
        public float alpha, rotZ;
        public int points { get; set; }

        public Circle(float posX, float posY, float __radius, byte r, byte g, byte b, float celX, float celY, float _rotZ, bool _isFixed = false) : base(posX, posY, r, g, b, celX, celY, _isFixed)
        {
            radius = __radius;
            rotZ = _rotZ;
            alpha = 0;
        }

        public void resetPoints()
        {
            points = 0;
        }
        protected override void updateMass()
        {
            mass = (float)Math.PI * radius * radius * Options.MASS_PER_VOLUME_CIRC;
        }

        public override void step(float dt, float gravityX, float gravityY, List<Rectangle> rects, List<Circle> circs, List<Triangle> trigs, float width, float height)
        {
            if (!isFixed)
            {
                float massO = 0;
                float radO = 0;
                float[] gravity = { gravityX, gravityY };
                float[] norm = { 0, 0 };
                float[] tang = { 0, 0 };

                float[] new_pos = pos.Clone() as float[];
                float[] new_cel = cel.Clone() as float[];
                float[] other_cel = { 0, 0 };
                float new_rotZ = rotZ;
                float a = 0;
                float b = 0;
                float c = 0;
                float d = 0;

                int i = 0;

                Circle other;
                Rectangle rec;
                Triangle trig;

                bool collision = false;

                for (i = 0; i < 2; i++)
                {
                    new_cel[i] = cel[i] + gravity[i] * dt;
                    new_pos[i] = pos[i] + new_cel[i] * dt;
                    norm[i] = tang[i] = 0;
                }

                alpha += rotZ * dt * 360 / ((float)Math.PI * 2);
                if (alpha > 360) alpha -= 360;
                if (alpha < -360) alpha += 360;

                // Bouncing against the walls (malus)
                if (new_pos[0] < 0 || new_pos[0] + radius * 2 > width || new_pos[1] < 0 || new_pos[1] + radius * 2 > height)
                {
                    points += Options.WALLS_MALUS;
                    if (new_pos[0] < 0)
                    { // Left
                        norm[0] = 1;
                        norm[1] = 0; // tang = [0,-1]
                        new_pos[0] = 0;
                        //new_cel[1] = (2*radius*new_rotZ + 5*new_cel[1])/7;
                    }
                    else if (new_pos[0] + radius * 2 > width)
                    { // Right
                        norm[0] = -1;
                        norm[1] = 0; // tang = [0, 1]
                        new_pos[0] = width - radius * 2;
                        //new_cel[1] = (-2*radius*new_rotZ + 5*new_cel[1])/7;
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
                        //new_cel[0] = (-2*radius*new_rotZ + 5*new_cel[0])/7;
                    }
                    else if (new_pos[1] + radius * 2 > height)
                    { // Bottom
                        norm[0] += 0;
                        norm[1] += -1; // tang = [-1,0]
                        new_pos[1] = height - radius * 2;
                        //new_cel[0] = (2*radius*new_rotZ + 5*new_cel[0])/7;
                    }

                    getTang(norm, out tang);

                    new_rotZ = (2 * radius * new_rotZ - 5 * (new_cel[0] * tang[0] + new_cel[1] * tang[1])) / (7 * radius);
                    a = (-1) * Options.BOUNCE_COEF * (new_cel[0] * norm[0] + new_cel[1] * norm[1]);
                    for (i = 0; i < 2; i++)
                    {
                        new_cel[i] = a * norm[i] - rotZ * radius * tang[i];
                    }
                }

                // Bouncing against Rectangles
                for (i = 0; i < rects.Count; i++)
                {
                    rec = rects[i];
                    collision = false;
                    norm[0] = 0; norm[1] = 0;
                    // Detect if in the rect
                    // Components of the vector from the ball to the rectangle :
                    a = new_pos[0] + radius - rec.X - rec.width / 2;
                    b = new_pos[1] + radius - rec.Y - rec.height / 2;
                    c = (float)Math.Sqrt(a * a + b * b); // Norm of this vector
                    if (c < radius + (float)Math.Sqrt(width * width + height * height) / 2)
                    {
                        // Vertical collisions :
                        if (new_pos[0] + radius > rec.X && new_pos[0] + radius < rec.X + rec.width)
                        {
                            // Collision on a (moving) plane
                            if (new_pos[1] + 2 * radius > rec.Y && new_pos[1] < rec.Y)
                            {
                                collision = true;
                                // Top collision
                                norm[0] = 0;
                                norm[1] = -1; // tang = [-1,0]
                                              //new_pos[1] = rec.Y - 2 * radius;
                            }
                            else if (new_pos[1] + 2 * radius > rec.Y + rec.height && new_pos[1] < rec.Y + rec.height)
                            {
                                collision = true;
                                // Bottom collision
                                norm[0] = 0;
                                norm[1] = 1; // tang = [1, 0]
                                             //new_pos[1] = rec.Y + rec.height;
                            }
                        }
                        // Horizontal collisions :
                        else if (new_pos[1] + radius > rec.Y && new_pos[1] + radius < rec.Y + rec.height)
                        {
                            // Collision on a (moving) plane
                            if (new_pos[0] + 2 * radius > rec.X && new_pos[0] < rec.X)
                            {
                                collision = true;
                                // Left collision
                                norm[0] = -1;
                                norm[1] = 0; // tang = [0, 1]
                                             //new_pos[0] = rec.X - radius * 2;
                            }
                            else if (new_pos[0] + 2 * radius > rec.X + rec.width && new_pos[0] < rec.X + rec.width)
                            {
                                collision = true;
                                // Right collision
                                norm[0] = 1;
                                norm[1] = 0; // tang = [0,-1]
                                             //new_pos[0] = rec.X + rec.width;
                            }
                        }
                        else
                        {
                            if ((a > 0 && b < 0))
                            {
                                a = new_pos[0] + radius - rec.X - rec.width;
                                b = new_pos[1] + radius - rec.Y;
                                c = (float)Math.Sqrt(a * a + b * b); //Norm of this vector
                                                          // Collision on an corner (like a little sphere or a 45° plane ?)
                                if (c < radius)
                                {
                                    collision = true;
                                }
                            }
                            else if ((a > 0 && b > 0))
                            {
                                a = new_pos[0] + radius - rec.X - rec.width;
                                b = new_pos[1] + radius - rec.Y - rec.height;
                                c = (float)Math.Sqrt(a * a + b * b); //Norm of this vector
                                                          // Collision on an corner (like a little sphere or a 45° plane ?)
                                if (c < radius)
                                {
                                    collision = true;
                                }
                            }
                            else if ((a < 0 && b < 0))
                            {
                                a = new_pos[0] + radius - rec.X;
                                b = new_pos[1] + radius - rec.Y;
                                c = (float)Math.Sqrt(a * a + b * b); //Norm of this vector
                                                          // Collision on an corner (like a little sphere or a 45° plane ?)
                                if (c < radius)
                                {
                                    collision = true;
                                }
                            }
                            else if ((a < 0 && b > 0))
                            {
                                a = new_pos[0] + radius - rec.X;
                                b = new_pos[1] + radius - rec.Y - rec.height;
                                c = (float)Math.Sqrt(a * a + b * b); //Norm of this vector
                                                          // Collision on an corner (like a little sphere or a 45° plane ?)
                                if (c < radius)
                                {
                                    collision = true;
                                }
                            }
                            if (collision)
                            {
                                norm[0] = a / c;
                                norm[1] = b / c;
                            }
                        }

                        if (collision)
                        {
                            if (!rec.isCollided)
                            {
                                for (i = 0; i < rects.Count; i++)
                                {
                                    rects[i].isCollided = false;
                                }
                                rec.isCollided = true;
                                points += Options.BAR_BONUS;
                            }
                            
                            other_cel[0] = (rec.celX * norm[0] + rec.celY * norm[1]) * norm[0];
                            other_cel[1] = (rec.celX * norm[0] + rec.celY * norm[1]) * norm[1];

                            new_pos[0] += norm[0];
                            new_pos[1] += norm[1];

                            getTang(norm, out tang);
                            new_rotZ = (2 * radius * new_rotZ - 5 * (new_cel[0] * tang[0] + new_cel[1] * tang[1] - other_cel[0] * tang[0] - other_cel[1] * tang[1])) / (7 * radius);
                            a = (-1) * Options.BOUNCE_COEF * (new_cel[0] * norm[0] + new_cel[1] * norm[1]);
                            b = (1 + Options.BOUNCE_COEF * (other_cel[0] * norm[0] + other_cel[1] * norm[1]));
                            for (i = 0; i < 2; i++)
                            {
                                new_cel[i] = (a + b) * norm[i] - rotZ * radius * tang[i];
                            }
                        }
                    }
                }

                for (i = 0; i < trigs.Count; i++)
                {
                    trig = trigs[i];
                    if (!trig.isOff)
                    {
                        // Components of the vector from the ball to the Triangle :
                        a = new_pos[0] + radius - trig.X - trig.radius;
                        b = new_pos[1] + radius - trig.Y - trig.radius;
                        c = (float)Math.Sqrt(a * a + b * b);        //Norm of this vector
                        if (c < trig.radius + radius)
                        {
                            trig.effects(this);
                            points += Options.TRIG_BONUS; // Always gain a bonus with triangles (even for bad ones)
                        }
                    }
                }

                // Bouncing against other balls
                for (i = 0; i < circs.Count; i++)
                {
                    if (circs[i] != this)
                    {
                        other = circs[i];
                        massO = other.mass;
                        radO = other.radius;
                        // Components of the vector from the ball to the other one :
                        a = new_pos[0] + radius - other.X - radO;
                        b = new_pos[1] + radius - other.Y - radO;
                        c = (float)Math.Sqrt(a * a + b * b);        //Norm of this vector
                        d = c - radius - other.radius; // Depth of penetration of the two balls (negative value = penetration)
                                                             // If the 2 balls are colliding
                        if (d < 0)
                        {
                            norm[0] = a / c;
                            norm[1] = b / c;
                            getTang(norm, out tang);
                            a = new_cel[0];
                            b = new_cel[1];
                            c = (mass - massO) / (mass + massO);
                            d = 2 / (mass + massO);
                            new_cel[0] = (c * new_cel[0] + massO * d * other.celX) * Options.BOUNCE_COEF;
                            new_cel[1] = (c * new_cel[1] + massO * d * other.celY) * Options.BOUNCE_COEF;
                            other.celX = (-c * other.celX + mass * d * a) * Options.BOUNCE_COEF;
                            other.celY = (-c * other.celY + mass * d * b) * Options.BOUNCE_COEF;
                            new_pos[0] += norm[0];
                            new_pos[1] += norm[1];
                            other.move(-norm[0], -norm[1]);
                        }
                    }
                }

                // Nullify the celerity when too low
                for (i = 0; i < 2; i++)
                {
                    if (new_cel[i] < Options.CELERITY_LIM && new_cel[i] > -Options.CELERITY_LIM)
                        new_cel[i] = 0;
                }

                // Actually update values
                cel = new_cel;
                pos = new_pos;
                rotZ = new_rotZ;
            }
        }
    }
}
