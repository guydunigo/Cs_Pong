using System;
using System.Collections.Generic;

namespace Cs_Pong
{
    class Pong
    {
        private int Width { get; set; }
        private int Height { get; set; }
        public float GravityX { get; set; }
        public float GravityY { get; set; }

        private List<Shape> shapes;
        private List<Rectangle> rects;
        private List<Circle> circs;
        private List<Triangle> trigs;
        public List<Shape> Shapes {
            get
            {
                return shapes;
            }
        }
        public List<Rectangle> Rects {
            get
            {
                return rects;
            }
        }
        public List<Circle> Circs {
            get
            {
                return circs;
            }
        }
        public List<Triangle> Trigs
        {
            get
            {
                return trigs;
            }
        }

        public Pong(int _width = 800, int _height = 600, float _gravityX = 0, float _gravityY = Options.ACCg)
        {
            int i = 0;
            shapes = new List<Shape>();
            rects = new List<Rectangle>();
            circs = new List<Circle>();
            trigs = new List<Triangle>();

            Width = _width; Height = _height;

            Random r = new Random();

            // Adds the four moving walls :
            AddRect(new Rectangle(10, (r.Next() % (int)(.66 * Height)), 10, .33f * Height, 255, 0, 0, 0, (r.Next() % 10 + 10) * 30, false));
            AddRect(new Rectangle(Width - 20, (r.Next() % (int)(.66 * Height)), 10, .33f * Height, 0, 255, 0, 0, (r.Next() % 10 + 10) * 30, false));
            AddRect(new Rectangle((r.Next() % (int)(.66 * Width)), 10, .33f * Width, 10, 0, 0, 255, (r.Next() % 10 + 10) * 30, 0, false));
            AddRect(new Rectangle((r.Next() % (int)(.66 * Width)), Height - 20, .33f * Width, 10, 255, 255, 0, (r.Next() % 10 + 10) * 30, 0, false));

            // Adds the ball :
            AddCirc(new Circle(r.Next()%500,0,20+r.Next()%10,(byte)(r.Next()%255), (byte)(r.Next() % 255), (byte)(r.Next() % 255), r.Next()%100+100,r.Next()%100, r.Next()%20 - 10));

            // If more than one ball is to be added :
            for (i = 0; i < Options.NB_BALLS - 1; i++)
                AddCirc(new Circle(r.Next() % 500, 0, 10 + r.Next() % 30, (byte)(r.Next() % 255), (byte)(r.Next() % 255), (byte)(r.Next() % 255), r.Next() % 100, r.Next() % 100, r.Next() % 20 - 10));
        }

        public void AddRect(Rectangle rect)
        {
            shapes.Add(rect);
            rects.Add(rect);
        }
        public void AddCirc(Circle circ)
        {
            shapes.Add(circ);
            circs.Add(circ);
        }
        public void AddTrig(Triangle trig)
        {
            shapes.Add(trig);
            trigs.Add(trig);
        }

        public int GetPoints()
        {
            int points = 0;
            foreach(Circle c in circs)
            {
                points += c.points;
            }
            return points;
        }
        public void ResetPoints()
        {
            foreach(Circle c in circs)
            {
                c.resetPoints();
            }
        }

        public void MoveAll(float dx, float dy)
        {
            foreach(Shape s in shapes)
            {
                s.Move(dx, dy);
            }
        }

        // Method for lazy people : avoid writing again initialization code for each triangle
        private void AddRandomTrig<T>() where T:Triangle,new()
        {
            Random ra = new Random();
            T temp = new T()
            {
                X = ra.Next() % (Width / 2) + Width / 4,
                Y = ra.Next() % (Height / 2) + Height / 4,
                Radius = Options.TRIG_RADIUS,
                CelX = ra.Next() % 200 - 100,
                CelY = ra.Next() % 200 - 100,
                rotZ = ra.Next() % 8 - 4,
                isFixed = false
            };
            AddTrig(temp);
        }
        public void Step(float dt)
        {
            int r = new Random().Next() % Options.PROBA_BONUS_MALUS;
            // Add a new triangle randomly :
            switch(r)
            {
                case 0:
                    AddRandomTrig<Triangle_Points_Bonus>();
                    break;
                case 1:
                    AddRandomTrig<Triangle_Points_Malus>();
                    break;
                case 2:
                    AddRandomTrig<Triangle_Cel_Bonus>();
                    break;
                case 3:
                    AddRandomTrig<Triangle_Cel_Malus>();
                    break;
                case 4:
                    AddRandomTrig<Triangle_Size_Bonus>();
                    break;
                case 5:
                    AddRandomTrig<Triangle_Size_Malus>();
                    break;
                default:
                    break;
            }
            // Move every object :
            foreach(Shape s in shapes)
            {
                s.Step(dt, GravityX, GravityY, rects, circs, trigs, Width, Height);
            }
        }

        public void Resize(double newWidth, double newHeight)
        {
            Width = (int)newWidth;
            Height = (int)newHeight;

            // Replace right and bottom rectangles and resize all of them :
            rects[1].X = Width - 20;
            rects[3].Y = Height - 20;
            rects[0].Height = rects[1].Height = (int)(0.33 * Height);
            rects[2].Width = rects[3].Width = (int)(0.33 * Width);
        }
        public void DeleteTrigAt(int rank)
        {
            shapes.Remove(trigs[rank]);
            trigs.RemoveAt(rank);
        }
    }
}