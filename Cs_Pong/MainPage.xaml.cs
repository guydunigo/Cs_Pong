using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Cs_Pong
{
    enum Game_state { Menu, Game, Over };

    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Pong world;
        private Game_state state;
        private TextBlock selected;

        public MainPage()
        {
            this.InitializeComponent();
            // Do that only when this page is fully loaded :
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeChanged += OnResizing;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;

            NewGame();
            SetMenu();

            Game_loop();
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (state == Game_state.Over)
            {
                SetMenu();
            }
            else
            {
                switch (args.VirtualKey)
                {
                    case Windows.System.VirtualKey.Left:
                        world.GravityX = -Options.ACCg;
                        world.GravityY = 0;
                        break;
                    case Windows.System.VirtualKey.Right:
                        world.GravityX = +Options.ACCg;
                        world.GravityY = 0;
                        break;
                    case Windows.System.VirtualKey.Up:
                        world.GravityX = 0;
                        world.GravityY = -Options.ACCg;
                        MoveInMenu();
                        break;
                    case Windows.System.VirtualKey.Down:
                        world.GravityX = 0;
                        world.GravityY = +Options.ACCg;
                        MoveInMenu();
                        break;
                    case Windows.System.VirtualKey.Back:
                    case Windows.System.VirtualKey.Escape:
                        if (state == Game_state.Game)
                        {
                            SetGameOver();
                        }
                        else if (state == Game_state.Menu)
                        {
                            Application.Current.Exit();
                        }
                        break;
                    case Windows.System.VirtualKey.Enter:
                        if (selected == play)
                        {
                            Play_Click(null, null);
                        }
                        else if (selected == quit)
                        {
                            Quit_Click(null, null);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private void MoveInMenu()
        {
            if (selected == play)
            {
                selected = quit;
                play.Foreground = new SolidColorBrush(Windows.UI.Colors.Lime);
                quit.Foreground = new SolidColorBrush(Windows.UI.Colors.LimeGreen);
            }
            else
            {
                selected = play;
                quit.Foreground = new SolidColorBrush(Windows.UI.Colors.Lime);
                play.Foreground = new SolidColorBrush(Windows.UI.Colors.LimeGreen);
            }
        }

        private void OnResizing(object sender, SizeChangedEventArgs e)
        {
            world.Resize(e.NewSize.Width, e.NewSize.Height);
            CleanScreenElements();
            InitScreen();
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            SetGame();
        }
        private void Game_over_Click(object sender, RoutedEventArgs e)
        {
            SetMenu();
            NewGame();
        }

        private void SetMenu()
        {
            state = Game_state.Menu;
            game_over.Visibility = Visibility.Collapsed;
            play.Visibility = Visibility.Visible;
            quit.Visibility = Visibility.Visible;
            score.Visibility = Visibility.Collapsed;
            score_value.Visibility = Visibility.Collapsed;
            selected = play;
        }
        public void SetGame()
        {
            state = Game_state.Game;

            play.Visibility = Visibility.Collapsed;
            quit.Visibility = Visibility.Collapsed;
            game_over.Visibility = Visibility.Collapsed;
            score.Visibility = Visibility.Visible;
            score_value.Visibility = Visibility.Visible;

            world.ResetPoints();
        }
        private void SetGameOver()
        {
            state = Game_state.Over;

            game_over.Visibility = Visibility.Visible;
            play.Visibility = Visibility.Collapsed;
            quit.Visibility = Visibility.Collapsed;
            score.Visibility = Visibility.Visible;
            score_value.Visibility = Visibility.Visible;
        }

        private void NewGame()
        {
            world = new Pong((int)Window.Current.CoreWindow.Bounds.Width, (int)Window.Current.CoreWindow.Bounds.Height, 0, 0);
            CleanScreenElements();
            InitScreen();
        }

        private void CleanScreenElements()
        {
            stars.Children.Clear();
            circs.Children.Clear();
            rects.Children.Clear();
            trigs.Children.Clear();
        }

        private void MakeStarsBackground()
        {
            Random r = new Random();
            int i = 0;
            int nb_stars = (int)(Window.Current.CoreWindow.Bounds.Width * Window.Current.CoreWindow.Bounds.Height * Options.NB_STARS_PER_AREA);
            Windows.UI.Xaml.Shapes.Rectangle temp;
            for (i = 0; i < nb_stars; i++)
            {
                temp = new Windows.UI.Xaml.Shapes.Rectangle()
                {
                    Width = 2,
                    Height = 2,
                    Fill = new SolidColorBrush(Windows.UI.Colors.White),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness() { Left = r.Next() % Window.Current.CoreWindow.Bounds.Width, Top = r.Next() % Window.Current.CoreWindow.Bounds.Height }
                };
                // Adding it to the display structure :
                stars.Children.Add(temp);
            }
        }

        private void ButtonsPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            TextBlock b = sender as TextBlock;
            selected = null;
            if (b != null)
                b.Foreground = new SolidColorBrush(Windows.UI.Colors.LimeGreen);
        }
        private void ButtonsPointerExited(object sender, PointerRoutedEventArgs e)
        {
            TextBlock b = sender as TextBlock;
            selected = null;
            if (b != null)
                b.Foreground = new SolidColorBrush(Windows.UI.Colors.Lime);
        }

        private void InitScreen()
        {
            MakeStarsBackground();

            Windows.UI.Xaml.Shapes.Ellipse ctemp;
            Windows.UI.Xaml.Shapes.Rectangle rtemp;
            foreach (Circle c in world.Circs)
            {
                ctemp = new Windows.UI.Xaml.Shapes.Ellipse()
                {
                    Margin = new Windows.UI.Xaml.Thickness(c.X, c.Y, 0, 0),
                    Width = c.Radius * 2,
                    Height = c.Radius * 2,
                    Fill = new SolidColorBrush(new Windows.UI.Color()
                    {
                        A = (byte)255,
                        R = c.Color.Red,
                        G = c.Color.Green,
                        B = c.Color.Blue
                    }),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                circs.Children.Add(ctemp);
                rtemp = new Windows.UI.Xaml.Shapes.Rectangle()
                {
                    Margin = new Windows.UI.Xaml.Thickness(c.X + c.Radius, c.Y + c.Radius, 0, 0),
                    Width = c.Radius,
                    Height = c.Radius,
                    Fill = new SolidColorBrush(new Windows.UI.Color()
                    {
                        A = (byte)255,
                        R = (byte)(255 - c.Color.Red),
                        G = (byte)(255 - c.Color.Green),
                        B = (byte)(255 - c.Color.Blue)
                    }),
                    RenderTransform = new RotateTransform()
                    {
                        Angle = c.alpha
                    },
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                circs.Children.Add(rtemp);
            }
            foreach (Rectangle r in world.Rects)
            {
                rtemp = new Windows.UI.Xaml.Shapes.Rectangle()
                {
                    Margin = new Thickness(r.X, r.Y, 0, 0),
                    Width = r.Width,
                    Height = r.Height,
                    Fill = new SolidColorBrush(new Windows.UI.Color()
                    {
                        A = (byte)255,
                        R = r.Color.Red,
                        G = r.Color.Green,
                        B = r.Color.Blue
                    }),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                rects.Children.Add(rtemp);
            }
            // There shouldn't be any triangle at the beginning but if you reset the display (e.g. while resizing)
            UpdateTrianglesDisplay();
        }
        private void UpdateScreen()
        {
            // Unless we are at the game over screen, we update the screen :
            if (state != Game_state.Over)
            {
                score_value.Text = world.GetPoints().ToString();
            }
            // Update different forms :
            UpdateCirclesDisplay();
            UpdateRectanglesDisplay();
            UpdateTrianglesDisplay();
        }
        private void UpdateCirclesDisplay()
        {
            // Update positions and rotation and size of the circle and its rectangle
            Windows.UI.Xaml.Shapes.Ellipse e;
            Windows.UI.Xaml.Shapes.Rectangle r;
            for (int i = 0; i < world.Circs.Count; i++)
            {
                e = circs.Children[i * 2] as Windows.UI.Xaml.Shapes.Ellipse;
                e.Margin = new Thickness()
                {
                    Left = world.Circs[i].X,
                    Top = world.Circs[i].Y
                };
                e.Width = world.Circs[i].Radius * 2;
                e.Height = world.Circs[i].Radius * 2;
                // Update the rectangle :
                r = circs.Children[i * 2 + 1] as Windows.UI.Xaml.Shapes.Rectangle;
                r.Margin = new Thickness()
                {
                    Left = world.Circs[i].X + world.Circs[i].Radius,
                    Top = world.Circs[i].Y + world.Circs[i].Radius
                };
                r.Width = world.Circs[i].Radius;
                r.Height = world.Circs[i].Radius;
                // Rotate it :
                RotateTransform rot = r.RenderTransform as RotateTransform;
                rot.Angle = world.Circs[i].alpha;
            }
        }
        private void UpdateRectanglesDisplay()
        {
            // Update positions
            Windows.UI.Xaml.Shapes.Rectangle r;
            SolidColorBrush so;
            for (int i = 0; i < rects.Children.Count; i++)
            {
                r = rects.Children[i] as Windows.UI.Xaml.Shapes.Rectangle;
                so = r.Fill as SolidColorBrush;
                if (world.Rects[i].isCollided)
                {
                    r.Fill = new SolidColorBrush(new Windows.UI.Color()
                    {
                        A = 127,
                        R = so.Color.R,
                        G = so.Color.G,
                        B = so.Color.B
                    });
                }
                else
                {
                    r.Fill = new SolidColorBrush(new Windows.UI.Color()
                    {
                        A = 255,
                        R = so.Color.R,
                        G = so.Color.G,
                        B = so.Color.B
                    });
                }
                r.Margin = new Thickness()
                {
                    Left = world.Rects[i].X,
                    Top = world.Rects[i].Y
                };
            }
        }
        private void UpdateTrianglesDisplay()
        {
            Windows.UI.Xaml.Shapes.Polygon p;
            // Add new triangles if needed
            if (world.Trigs.Count - trigs.Children.Count > 0)
            {
                PointCollection points;

                for (int i = trigs.Children.Count; i < world.Trigs.Count; i++)
                {
                    // Can't move it out of the loop : it generates an ArgumentExeption :/
                    points = new PointCollection
                    {
                        new Point(Options.TRIG_RADIUS, 0),
                        new Point(Options.TRIG_RADIUS * ( 1 - Math.Sqrt(3) / 2.0), Options.TRIG_RADIUS * 1.5 ),
                        new Point(Options.TRIG_RADIUS * ( 1 + Math.Sqrt(3) / 2.0), Options.TRIG_RADIUS * 1.5)
                    };
                    p = new Windows.UI.Xaml.Shapes.Polygon()
                    {
                        Points = points,
                        Margin = new Thickness(world.Trigs[i].X, world.Trigs[i].Y, 0, 0),
                        Fill = new SolidColorBrush(new Windows.UI.Color()
                        {
                            A = (byte)255,
                            R = world.Trigs[i].Color.Red,
                            G = world.Trigs[i].Color.Green,
                            B = world.Trigs[i].Color.Blue
                        }),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        RenderTransform = new RotateTransform()
                        {
                            Angle = world.Trigs[i].alpha
                        }
                    };
                    trigs.Children.Add(p);
                }
            }
            for (int i = 0; i < world.Trigs.Count; i++)
            {
                if (world.Trigs[i].IsOff)
                {
                    trigs.Children.RemoveAt(i);
                    world.DeleteTrigAt(i);
                    i--;
                }
            }
            // Update positions and rotation :
            for (int i = 0; i < world.Trigs.Count; i++)
            {
                p = trigs.Children[i] as Windows.UI.Xaml.Shapes.Polygon;
                p.Margin = new Thickness()
                {
                    Left = world.Trigs[i].X,
                    Top = world.Trigs[i].Y
                };
                // Rotate it :
                RotateTransform rot = p.RenderTransform as RotateTransform;
                rot.Angle = world.Trigs[i].alpha;
            }
        }

        private async void Game_loop()
        {
            System.DateTime prev = System.DateTime.Now;
            while (true)
            {
                world.Step(((float)DateTime.Now.Subtract(prev).Milliseconds) / 1000);
                UpdateScreen();

                prev = System.DateTime.Now;
                await Task.Delay(1);
            }
        }
    }
}
