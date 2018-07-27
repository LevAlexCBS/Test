using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControl5.xaml
    /// </summary>
    public partial class UserControl5 : UserControl
    {
        #region Binding members
        public double IOffset { get; set; } = 100;
        public double JOffset { get; set; } = 100;
        public string INode { get; set; } = "INode";
        public string JNode { get; set; } = "JNode";

        public List<PointLoad> PointLoads { get; set; }
        public List<DistributedLoad> DistributedLoads { get; set; }
        #endregion


        public UserControl5()
        {

            PointLoads = new List<PointLoad>
            {
                //new PointLoad(10,200, LoadDirection.x),
                //new PointLoad(35, 700, LoadDirection.Z),
                //new PointLoad(35, 650, LoadDirection.Mx),
                //new PointLoad(-1, 250, LoadDirection.Mx),
                //new PointLoad(35, 550, LoadDirection.My),
                //new PointLoad(-1, 350, LoadDirection.My),
                new PointLoad(35, 850, LoadDirection.Mz),
                new PointLoad(-1, 500, LoadDirection.Mz),
            };
            DistributedLoads = new List<DistributedLoad>
            {
                //new DistributedLoad
                //{
                //    StartMagnitude = 0,
                //    EndMagnitude = -10,
                //    Direction = LoadDirection.Z,
                //    StartLocation = 250,
                //    EndLocation = 500,
                //},
                //new DistributedLoad
                //{
                //    StartMagnitude = 100,
                //    EndMagnitude = 50,
                //    Direction = LoadDirection.Y,
                //    StartLocation = 200,
                //    EndLocation = 500,
                //},

                //new DistributedLoad
                //{
                //    StartMagnitude = 100,
                //    EndMagnitude = 100,
                //    Direction = LoadDirection.X,
                //    StartLocation = 100,
                //    EndLocation = 900,
                //},
                //new DistributedLoad
                //{
                //    StartMagnitude = 100,
                //    EndMagnitude = 100,
                //    Direction = LoadDirection.Mx,
                //    StartLocation = 100,
                //    EndLocation = 900,
                //}

            };

            InitializeComponent();
            DrawMember();
            DrawPointLoads();
            DrawDistributedLoad();

            DrawText(500, 300, "Loading", Color.FromRgb(0, 0, 0));

        }


        private void DrawMember()
        {
            var member = new Line
            {
                X1 = 0 + IOffset,
                Y1 = 250,
                X2 = 1000 - JOffset,
                Y2 = 250,
                StrokeThickness = 2,
                Stroke = Brushes.Black
            };
            var ioffsetline = new Line
            {
                X1 = 0,
                Y1 = 250,
                X2 = IOffset,
                Y2 = 250,
                StrokeThickness = 4,
                Stroke = Brushes.Gray
            };
            var joffsetline = new Line
            {
                X1 = 1000,
                Y1 = 250,
                X2 = 1000 - JOffset,
                Y2 = 250,
                StrokeThickness = 4,
                Stroke = Brushes.Gray
            };
            var inode = new Ellipse
            {
                Width = 8,
                Height = 8,
                Stroke = Brushes.Black,
                Fill = Brushes.Green,
                StrokeThickness = 0.5
            };
            var jnode = new Ellipse
            {
                Width = 8,
                Height = 8,
                Stroke = Brushes.Black,
                Fill = Brushes.Green,
                StrokeThickness = 0.5
            };
            canvas.Children.Add(member);
            canvas.Children.Add(ioffsetline);
            canvas.Children.Add(joffsetline);
            Canvas.SetTop(inode, 246);
            canvas.Children.Add(inode);
            Canvas.SetTop(jnode, 246);
            Canvas.SetLeft(jnode, 1000);
            canvas.Children.Add(jnode);

            DrawText(10, 255, INode, Color.FromRgb(0, 128, 0));
            DrawText(1010, 255, JNode, Color.FromRgb(0, 128, 0));
        }

        private void DrawText(double x, double y, string text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvas.Children.Add(textBlock);
        }

        #region DrawingPointLoads
        private void DrawPointLoads()
        {
            if (PointLoads == null)
                return;
            foreach (var item in PointLoads)
            {
                switch (item.Direction)
                {
                    case LoadDirection.X:
                    case LoadDirection.x:
                        DrawXPointLoads(item);
                        break;
                    case LoadDirection.Y:
                    case LoadDirection.y:
                        DrawYPointLoads(item);
                        break;
                    case LoadDirection.Z:
                    case LoadDirection.z:
                        DrawZPointLoads(item);
                        break;
                    case LoadDirection.Mx:
                        DrawMxPointLoad(item);
                        break;
                    case LoadDirection.My:
                        DrawMyPointLoad(item);
                        break;
                    case LoadDirection.Mz:
                        DrawMzPointLoad(item);
                        break;
                }
            }
        }

        private void DrawMzPointLoad(PointLoad item)
        {
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new Point(item.Location - 20, 225);

            ArcSegment arcSeg = new ArcSegment();
            arcSeg.Point = new Point(item.Location - 20 , 275); 
            arcSeg.Size = new Size(30, 30);
            arcSeg.IsLargeArc = true;
            arcSeg.SweepDirection = SweepDirection.Clockwise;
            

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(arcSeg);

            pthFigure.Segments = myPathSegmentCollection;

            PathFigureCollection pthFigureCollection = new PathFigureCollection();
            pthFigureCollection.Add(pthFigure);

            PathGeometry pthGeometry = new PathGeometry();
            pthGeometry.Figures = pthFigureCollection;

            Path arcPath = new Path();
            arcPath.Stroke = new SolidColorBrush(Colors.Blue);
            arcPath.StrokeThickness = 1;
            arcPath.Data = pthGeometry;
            canvas.Children.Add(arcPath);
        }

        private void DrawXPointLoads(PointLoad load)
        {

        }

        private void DrawMyPointLoad(PointLoad load)
        {
            var line = new Line { X1 = load.Location - 30, X2 = load.Location + 30, Y1 = 250, Y2 = 250, Stroke = Brushes.Blue, StrokeThickness = 2 };
            canvas.Children.Add(line);
            DrawText(load.Location + 30, 230, load.Magnitude + "k-ft", Color.FromRgb(0, 0, 255));
        }

        private void DrawMxPointLoad(PointLoad load)
        {
            var line = new Line { X1 = load.Location, X2 = load.Location, Y1 = 285, Y2 = 215, Stroke = Brushes.Blue };
            canvas.Children.Add(line);
            DrawText(load.Location + 5, 200, load.Magnitude + "k-ft", Color.FromRgb(0, 0, 255));
        }

        private void DrawYPointLoads(PointLoad load)
        {

            var line = new Line { X1 = load.Location, X2 = load.Location, Y1 = 250, Y2 = 250 - load.Magnitude, Stroke = Brushes.Blue };
            var arrow = new Polyline()
            {
                Points = new PointCollection
                {
                    new Point(load.Location, 250),
                    new Point(load.Location - 10, 235),
                    new Point(load.Location + 10, 235),
                },
                Fill = Brushes.Blue
            };
            canvas.Children.Add(line);
            canvas.Children.Add(arrow);

            DrawText(load.Location + 15, line.Y2 - 10, load.Magnitude + "k", Color.FromRgb(0, 0, 255));
        }

        private void DrawZPointLoads(PointLoad load)
        {
            var outcircle = new Ellipse { Width = load.Magnitude, Height = load.Magnitude, Stroke = Brushes.Blue };
            Canvas.SetLeft(outcircle, load.Location);
            Canvas.SetTop(outcircle, 250 - outcircle.Height / 2);
            var incircle = new Ellipse { Width = load.Magnitude / 5, Height = load.Magnitude / 5, Stroke = Brushes.Blue, Fill = Brushes.Blue };
            Canvas.SetLeft(outcircle, load.Location);
            Canvas.SetTop(outcircle, 250 - outcircle.Width / 2);
            Canvas.SetLeft(incircle, load.Location + outcircle.Width / 2 - incircle.Width / 2);
            Canvas.SetTop(incircle, 250 - incircle.Width / 2);
            DrawText(load.Location + 20, 230 - outcircle.Height / 2, load.Magnitude + "k", Color.FromRgb(0, 0, 255));
            canvas.Children.Add(outcircle);
            canvas.Children.Add(incircle);
        }

        #endregion


        #region DrawingDistributedLoad
        private void DrawDistributedLoad()
        {
            if (DistributedLoads == null)
                return;
            foreach (var item in DistributedLoads)
            {
                switch (item.Direction)
                {
                    case LoadDirection.X:
                    case LoadDirection.x:
                        DrawXDistributedLoad(item);
                        break;
                    case LoadDirection.Y:
                    case LoadDirection.y:
                        DrawYDistributedLoad(item);
                        break;
                    case LoadDirection.Z:
                    case LoadDirection.z:
                        DrawZDistributedLoad(item);
                        break;
                    case LoadDirection.Mx:
                        DrawMxDistributedLoad(item);
                        break;
                }
            }

        }

        private void DrawMxDistributedLoad(DistributedLoad load)
        {
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 20) + 1; i++)
            {
                var newx = load.StartLocation + i * 20;
                var line = new Line
                {
                    X1 = newx,
                    X2 = newx,
                    Y1 = 240,
                    Y2 = 260,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                canvas.Children.Add(line);
            }
        }

        private void DrawXDistributedLoad(DistributedLoad load)
        {
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 20) + 1; i++)
            {
                var newx = load.StartLocation + i * 20;
                var uparrow = new Line
                {
                    X1 = newx,
                    X2 = newx + 25,
                    Y1 = 250,
                    Y2 = 240,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                var downarrow = new Line
                {
                    X1 = newx,
                    X2 = newx + 25,
                    Y1 = 250,
                    Y2 = 260,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                canvas.Children.Add(uparrow);
                canvas.Children.Add(downarrow);
            }
        }


        private void DrawYDistributedLoad(DistributedLoad load)
        {

            var lineload = new Line
            {
                X1 = load.StartLocation,
                X2 = load.EndLocation,
                Y1 = load.StartMagnitude,
                Y2 = load.EndMagnitude,
                Stroke = Brushes.DarkViolet,
                StrokeThickness = 2
            }; ;
            canvas.Children.Add(lineload);
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 20) + 1; i++)
            {
                var newx = load.StartLocation + i * 20;
                var line = new Line
                {
                    X1 = newx,
                    X2 = newx,
                    Y1 = 250,
                    Y2 = Line(newx, load),
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                var larrow = new Line
                {
                    X1 = newx,
                    X2 = newx - 10,
                    Y1 = 250,
                    Y2 = 230,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                var rarrow = new Line
                {
                    X1 = newx,
                    X2 = newx + 10,
                    Y1 = 250,
                    Y2 = 230,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                canvas.Children.Add(line);
                canvas.Children.Add(larrow);
                canvas.Children.Add(rarrow);
            }

            DrawText(load.StartLocation - 45, load.StartMagnitude, load.StartLabelLoad, Color.FromRgb(0, 0, 0));
            DrawText(load.EndLocation + 10, load.EndMagnitude - 10, load.EndLabelLoad, Color.FromRgb(0, 0, 0));
        }

        private void DrawZDistributedLoad(DistributedLoad load)
        {
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 20) + 1; i++)
            {
                var circle = new Ellipse()
                {
                    Height = 6,
                    Width = 6,
                    Fill = Brushes.DarkViolet,
                    Stroke = Brushes.DarkViolet
                };
                Canvas.SetTop(circle, 247);
                Canvas.SetLeft(circle, load.StartLocation + i * 20);
                canvas.Children.Add(circle);
                DrawText(load.StartLocation, 230, load.StartMagnitude + "k/ft", Color.FromRgb(148, 0, 211));
                DrawText(load.EndLocation, 230, load.EndMagnitude + "k/ft", Color.FromRgb(148, 0, 211));
            }
        }
        #endregion
        private double Line(double x, DistributedLoad load)
        {
            return ((x - load.StartLocation) * (load.EndMagnitude - load.StartMagnitude)) / (load.EndLocation - load.StartLocation) + load.StartMagnitude;
        }



    }

}
