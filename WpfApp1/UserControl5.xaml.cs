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
        public DistributedLoad DistributedLoad { get; set; }
        #endregion


        public UserControl5()
        {

            PointLoads = new List<PointLoad>
            {
                new PointLoad(10,200, LoadDirection.x),
                new PointLoad(35, 700, LoadDirection.Z),
                new PointLoad(35, 650, LoadDirection.Mx),
                new PointLoad(35, 550, LoadDirection.My),
                new PointLoad(35, 850, LoadDirection.Mz)
            };
            DistributedLoad = new DistributedLoad
            {
                StartMagnitude = 100,
                EndMagnitude = 100,
                Direction = LoadDirection.Z,
                StartLocation = 100,
                EndLocation = 900,
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
            var inode = new Ellipse { Width = 8, Height = 8, Stroke = Brushes.Black, Fill = Brushes.Green, StrokeThickness = 0.5 };
            var jnode = new Ellipse { Width = 8, Height = 8, Stroke = Brushes.Black, Fill = Brushes.Green, StrokeThickness = 0.5 };
            canvas.Children.Add(member);
            canvas.Children.Add(ioffsetline);
            canvas.Children.Add(joffsetline);
            Canvas.SetTop(inode, 246);
            canvas.Children.Add(inode);
            Canvas.SetTop(jnode, 246);
            Canvas.SetLeft(jnode, 1000);
            canvas.Children.Add(jnode);

            DrawText(10, 255, INode, Color.FromRgb(0, 255, 100));
            DrawText(1010, 255, JNode, Color.FromRgb(0, 255, 0));
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
            pthFigure.StartPoint = new Point(item.Location - 20, 220);

            ArcSegment arcSeg = new ArcSegment();
            arcSeg.Point = new Point(item.Location - 10, 280); ;
            arcSeg.Size = new Size(3, 3);
            arcSeg.IsLargeArc = false;
            arcSeg.SweepDirection = SweepDirection.Clockwise;
            arcSeg.RotationAngle = 280;

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
            var line = new Line { X1 = load.Location - load.Magnitude, X2 = load.Location + load.Magnitude, Y1 = 250, Y2 = 250, Stroke = Brushes.Blue, StrokeThickness = 2 };
            canvas.Children.Add(line);
        }

        private void DrawMxPointLoad(PointLoad load)
        {
            var line = new Line { X1 = load.Location, X2 = load.Location, Y1 = 250 + load.Magnitude, Y2 = 250 - load.Magnitude, Stroke = Brushes.Blue };
            canvas.Children.Add(line);
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
            if (DistributedLoad == null)
                return;

            switch (DistributedLoad.Direction)
            {
                case LoadDirection.X:
                case LoadDirection.x:
                    break;
                case LoadDirection.Y:
                case LoadDirection.y:
                    DrawYDistributedLoad();
                    break;
                case LoadDirection.Z:
                case LoadDirection.z:
                    DrawZDistributedLoad();
                    break;
                case LoadDirection.Mx:
                    break;
            }

        }


        #endregion
        private void DrawYDistributedLoad()
        {
            var lineload = new Line
            {
                X1 = DistributedLoad.StartLocation,
                X2 = DistributedLoad.EndLocation,
                Y1 = DistributedLoad.StartMagnitude,
                Y2 = DistributedLoad.EndMagnitude, 
                Stroke = Brushes.DarkViolet,
                StrokeThickness = 2
            }; ;
            canvas.Children.Add(lineload);
            for (int i = 0; i < Math.Floor((1000 - JOffset - IOffset) / 20) + 1; i++)
            {
                var newx = DistributedLoad.StartLocation + i * 20;
                var line = new Line
                {
                    X1 = newx,
                    X2 = newx,
                    Y1 = 250,
                    Y2 = Line(newx),
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                var larrow = new Line
                {
                    X1 = newx,
                    X2 = newx - 5,
                    Y1 = 250,
                    Y2 = 235,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                var rarrow = new Line
                {
                    X1 = newx,
                    X2 = newx + 5,
                    Y1 = 250,
                    Y2 = 235,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                canvas.Children.Add(line);
                canvas.Children.Add(larrow);
                canvas.Children.Add(rarrow);
            }

            DrawText(DistributedLoad.StartLocation - 45, DistributedLoad.StartMagnitude, DistributedLoad.StartLabelLoad, Color.FromRgb(0, 0, 0));
            DrawText(DistributedLoad.EndLocation + 10, DistributedLoad.EndMagnitude - 10, DistributedLoad.EndLabelLoad, Color.FromRgb(0, 0, 0));
        }

        private void DrawZDistributedLoad()
        {
            var load = new Line
            {
                X1 = 0 + DistributedLoad.StartLocation,
                Y1 = 250,
                X2 = DistributedLoad.EndLocation,
                Y2 = 250,
                StrokeThickness = 6,
                StrokeDashArray = new DoubleCollection() { 1, 2 },
                Stroke = Brushes.DarkViolet
            };
            canvas.Children.Add(load);
        }

        private double Line(double x)
        {
            return ((x - DistributedLoad.StartLocation) * (DistributedLoad.EndMagnitude - DistributedLoad.StartMagnitude)) / (DistributedLoad.EndLocation - DistributedLoad.StartLocation) + DistributedLoad.StartMagnitude;
        }



    }

}
