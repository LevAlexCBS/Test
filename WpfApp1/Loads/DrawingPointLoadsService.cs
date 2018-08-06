using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class DrawingPointLoadsHelper
    {
        private Canvas _canvas;
        public IEnumerable<PointLoad> _pointLoads;

        public DrawingPointLoadsHelper(Canvas canvas, IEnumerable<PointLoad> pointLoads)
        {
            _canvas = canvas;
            _pointLoads = pointLoads;
            DrawPointLoads();
        }
        private void DrawPointLoads()
        {
            if (_pointLoads == null)
                return;
            foreach (var item in _pointLoads)
            {
                switch (item.Direction)
                {
                    case LoadDirection.X:
                    case LoadDirection.x:
                        DrawXPointLoads(item);
                        break;
                    case LoadDirection.Y:
                    case LoadDirection.y:
                    case LoadDirection.PY:
                        DrawYPointLoads(item);
                        break;
                    case LoadDirection.Z:
                    case LoadDirection.z:
                    case LoadDirection.PZ:
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

        private void DrawMzPointLoad(PointLoad load)
        {
            PathFigure pthFigure = new PathFigure();
            pthFigure.StartPoint = new Point(load.Location - 20, 225);

            ArcSegment arcSeg = new ArcSegment();
            arcSeg.Point = new Point(load.Location - 20, 275);
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

            var uparrow = new Polyline()
            {
                Points = new PointCollection
                {
                    new Point(load.Location - 5, 267),
                    new Point(load.Location - 25, 267),
                    new Point(load.Location -25, 287),
                },
                Fill = Brushes.Blue,
            };
            var downarrow = new Polyline()
            {
                Points = new PointCollection
                {
                    new Point(load.Location - 5, 232),
                    new Point(load.Location - 25, 232),
                    new Point(load.Location -25, 215),
                },
                Fill = Brushes.Blue,
            };
            _canvas.Children.Add(load.Magnitude < 0 ? downarrow : uparrow);
            _canvas.Children.Add(arcPath);
            DrawText(load.Location + 5, 200, load.Magnitude + "k-ft", Color.FromRgb(0, 0, 255));
        }

        private void DrawXPointLoads(PointLoad load)
        {
            var line = new Line
            {
                X1 = load.Magnitude < 0 ? load.Location + 50 : load.Location - 50,
                X2 = load.Location,
                Y1 = 250,
                Y2 = 250,
                Stroke = Brushes.Blue,
                StrokeThickness = 2,
            };
            var arrow = new Polyline()
            {
                Points = new PointCollection
                {
                    new Point(load.Location, 250),
                    new Point(load.Magnitude < 0 ? load.Location + 10 : load.Location - 10 , 240),
                    new Point(load.Magnitude < 0 ? load.Location + 10 : load.Location - 10, 260),
                },
                Fill = Brushes.Blue
            };
            _canvas.Children.Add(line);
            _canvas.Children.Add(arrow);

            DrawText(line.X1, 250, load.Magnitude + "k", Color.FromRgb(0, 0, 255));
        }

        private void DrawMyPointLoad(PointLoad load)
        {

            var line = new Line
            {
                X1 = load.Location - 30,
                X2 = load.Location + 30,
                Y1 = 250,
                Y2 = 250,
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };
            _canvas.Children.Add(line);
            DrawText(load.Location + 30, 230, load.Magnitude + "k-ft", Color.FromRgb(0, 0, 255));
        }

        private void DrawMxPointLoad(PointLoad load)
        {
            var line = new Line { X1 = load.Location, X2 = load.Location, Y1 = 285, Y2 = 215, Stroke = Brushes.Blue };
            _canvas.Children.Add(line);
            DrawText(load.Location + 5, 200, load.Magnitude + "k-ft", Color.FromRgb(0, 0, 255));
        }

        private void DrawYPointLoads(PointLoad load)
        {
            double _magnitude;
            if (load.Magnitude < 1 && load.Magnitude > 0)
                _magnitude = 1;
            else if (load.Magnitude > -1 && load.Magnitude < 0)
                _magnitude = -1;
            else if (load.Magnitude > 15)
                _magnitude = 15;
            else if (load.Magnitude < -15)
                _magnitude = -15;
            else
                _magnitude = load.Magnitude;

            var scale = 3.33;
            var scalearrowY = 0.866;
            var scalearrowX = _magnitude > 0 ? 0.33 : -0.33;


            var line = new Line
            {
                X1 = load.Location,
                X2 = load.Location,
                Y1 = 250,
                Y2 = load.Magnitude < 0 ? _magnitude * scale + 230 : _magnitude * scale + 270,
                Stroke = Brushes.Blue
            };
            var arrow = new Polyline()
            {
                Points = new PointCollection
                {
                    new Point(load.Location, 250),
                    new Point(load.Location - (_magnitude* scalearrowX) - 5,_magnitude < 0 ? _magnitude * scalearrowY + 240 : _magnitude * scalearrowY + 260),
                    new Point(load.Location + (_magnitude* scalearrowX) + 5,_magnitude < 0 ? _magnitude * scalearrowY + 240 : _magnitude * scalearrowY + 260),
                },
                Fill = Brushes.Blue
            };
            _canvas.Children.Add(line);
            _canvas.Children.Add(arrow);

            DrawText(load.Location + 5, line.Y2 - 10, load.Magnitude + "k", Color.FromRgb(0, 0, 255));
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
            _canvas.Children.Add(outcircle);
            _canvas.Children.Add(incircle);
        }

        private void DrawText(double x, double y, string text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            _canvas.Children.Add(textBlock);
        }
    }
}
