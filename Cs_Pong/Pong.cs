using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Cs_Pong
{
    class Pong
    {
        private int width { get; set; }
        private int height { get; set; }
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

            width = _width; height = _height;

            Random r = new Random();

            // Adds the four moving walls :
            addRect(new Rectangle(10, (r.Next() % (int)(.66 * height)), 10, .33f * height, 255, 0, 0, 0, (r.Next() % 10 + 10) * 30, false));
            addRect(new Rectangle(width - 20, (r.Next() % (int)(.66 * height)), 10, .33f * height, 0, 255, 0, 0, (r.Next() % 10 + 10) * 30, false));
            addRect(new Rectangle((r.Next() % (int)(.66 * width)), 10, .33f * width, 10, 0, 0, 255, (r.Next() % 10 + 10) * 30, 0, false));
            addRect(new Rectangle((r.Next() % (int)(.66 * width)), height - 20, .33f * width, 10, 255, 255, 0, (r.Next() % 10 + 10) * 30, 0, false));

            // Adds the ball :
            addCirc(new Circle(r.Next()%500,0,20+r.Next()%10,(byte)(r.Next()%255), (byte)(r.Next() % 255), (byte)(r.Next() % 255), r.Next()%100+100,r.Next()%100, r.Next()%20 - 10));

            // If more than one ball is to be added :
            for (i = 0; i < Options.NB_BALLS - 1; i++)
                addCirc(new Circle(r.Next() % 500, 0, 10 + r.Next() % 30, (byte)(r.Next() % 255), (byte)(r.Next() % 255), (byte)(r.Next() % 255), r.Next() % 100, r.Next() % 100, r.Next() % 20 - 10));
        }

        public void addRect(Rectangle rect)
        {
            shapes.Add(rect);
            rects.Add(rect);
        }
        public void addCirc(Circle circ)
        {
            shapes.Add(circ);
            circs.Add(circ);
        }
        public void addTrig(Triangle trig)
        {
            shapes.Add(trig);
            trigs.Add(trig);
        }

        public int getPoints()
        {
            int points = 0;
            foreach(Circle c in circs)
            {
                points += c.points;
            }
            return points;
        }
        public void resetPoints()
        {
            foreach(Circle c in circs)
            {
                c.resetPoints();
            }
        }

        public void moveAll(float dx, float dy)
        {
            foreach(Shape s in shapes)
            {
                s.move(dx, dy);
            }
        }

        // Method for lazy people : avoid writing again initialization code for each triangle
        private void addRandomTrig<T>() where T:Triangle,new()
        {
            Random ra = new Random();
            T temp = new T()
            {
                X = ra.Next() % (width / 2) + width / 4,
                Y = ra.Next() % (height / 2) + height / 4,
                radius = Options.TRIG_RADIUS,
                celX = ra.Next() % 200 - 100,
                celY = ra.Next() % 200 - 100,
                rotZ = ra.Next() % 8 - 4,
                isFixed = false
            };
            addTrig(temp);
        }
        public void step(float dt)
        {
            int r = new Random().Next() % Options.PROBA_BONUS_MALUS;
            // Add a new triangle randomly :
            switch(r)
            {
                case 0:
                    addRandomTrig<Triangle_Points_Bonus>();
                    break;
                case 1:
                    addRandomTrig<Triangle_Points_Malus>();
                    break;
                case 2:
                    addRandomTrig<Triangle_Cel_Bonus>();
                    break;
                case 3:
                    addRandomTrig<Triangle_Cel_Malus>();
                    break;
                case 4:
                    addRandomTrig<Triangle_Size_Bonus>();
                    break;
                case 5:
                    addRandomTrig<Triangle_Size_Malus>();
                    break;
                default:
                    break;
            }
            // Move every object :
            foreach(Shape s in shapes)
            {
                s.step(dt, GravityX, GravityY, rects, circs, trigs, width, height);
            }
            // Delete disabled triangles :
            for (int i = 0 ; i < trigs.Count; i++)
            {
                if (trigs[i].isOff)
                {
                    for (int j = 0; j < shapes.Count; j++)
                    {
                        if (shapes[j] == trigs[i])
                        {
                            shapes.RemoveAt(j);
                        }
                    }
                    trigs.RemoveAt(i);
                }
            }
        }

        public void resize(double newWidth, double newHeight)
        {
            width = (int)newWidth;
            height = (int)newHeight;

            // Replace right and bottom rectangles and resize all of them :
            rects[1].X = width - 20;
            rects[3].Y = height - 20;
            rects[0].height = rects[1].height = (int)(0.33 * height);
            rects[2].width = rects[3].width = (int)(0.33 * width);
        }
        public void deleteTrigAt(int rank)
        {
            shapes.Remove(trigs[rank]);
            trigs.RemoveAt(rank);
        }
    }
}