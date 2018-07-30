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
    public class DrawingDistributedLoadHelper
    {
        private Canvas _canvas;
        private IEnumerable<DistributedLoad> _distributedLoads;
        public DrawingDistributedLoadHelper(Canvas canvas, IEnumerable<DistributedLoad> distributedLoads)
        {
            _canvas = canvas;
            _distributedLoads = distributedLoads;
            DrawDistributedLoad();

        }
        #region DrawingDistributedLoad
        private void DrawDistributedLoad()
        {
            if (_distributedLoads == null)
                return;
            foreach (var item in _distributedLoads)
            {
                switch (item.Direction)
                {
                    case LoadDirection.X:
                    case LoadDirection.x:
                        DrawXDistributedLoad(item);
                        break;
                    case LoadDirection.Y:
                    case LoadDirection.y:
                    case LoadDirection.T:
                    case LoadDirection.PY:
                    case LoadDirection.Sy:
                    case LoadDirection.SY:
                        DrawYDistributedLoad(item);
                        break;
                    case LoadDirection.Z:
                    case LoadDirection.z:
                        DrawZDistributedLoad(item);
                        break;
                    case LoadDirection.Mx:
                        DrawMxDistributedLoad(item);
                        break;
                    case LoadDirection.SX:
                        DrawSXDistributedLoad(item);
                        break;
                    case LoadDirection.SZ:
                    case LoadDirection.Sz:
                        DrawSzDistributedLoad(item);
                        break;
                }
            }

        }

        private void DrawSzDistributedLoad(DistributedLoad load)
        {
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 20) + 1; i++)
            {
                var newx = load.StartLocation + i * 20;
                var rhombus = new Polyline()
                {
                    Points = new PointCollection
                {
                    new Point(newx, 250),
                    new Point(newx + 5, 245),
                    new Point(newx + 10, 250),
                    new Point(newx + 5, 255),
                },
                    Fill = Brushes.DarkGreen,
                };
                _canvas.Children.Add(rhombus);
                DrawText(load.StartLocation, 230, load.StartMagnitude + "kfs", Color.FromRgb(0, 100, 0));
                DrawText(load.EndLocation, 230, load.EndMagnitude + "kfs", Color.FromRgb(0, 100, 0));
            }
        }

        private void DrawSXDistributedLoad(DistributedLoad load)
        {
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 20) + 1; i++)
            {
                var newx = load.StartLocation + i * 20;
                var arrow = new Polyline()
                {
                    Points = new PointCollection
                {
                    new Point(newx, 245),
                    new Point(newx + 10, 250),
                    new Point(newx, 255),
                },
                    Fill = Brushes.DarkGreen,
                };
                _canvas.Children.Add(arrow);
                DrawText(load.StartLocation, 230, load.StartMagnitude + "kfs", Color.FromRgb(0, 100, 0));
                DrawText(load.EndLocation, 230, load.EndMagnitude + "kfs", Color.FromRgb(0, 100, 0));
            }
        }
        private void DrawMxDistributedLoad(DistributedLoad load)
        {
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 10) + 1; i++)
            {
                var newx = load.StartLocation + i * 10;
                var line = new Line
                {
                    X1 = newx,
                    X2 = newx,
                    Y1 = 230,
                    Y2 = 270,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1
                };
                _canvas.Children.Add(line);
                DrawText(load.StartLocation, 210, load.StartMagnitude + "k-ft/ft", Color.FromRgb(148, 0, 211));
                DrawText(load.EndLocation, 210, load.EndMagnitude + "k-ft/ft", Color.FromRgb(148, 0, 211));
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
                    X2 = load.StartMagnitude < load.EndMagnitude ? newx - 25 : newx + 25,
                    Y1 = 250,
                    Y2 = 240,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1,

                };
                var downarrow = new Line
                {
                    X1 = newx,
                    X2 = load.StartMagnitude < load.EndMagnitude ? newx - 25 : newx + 25,
                    Y1 = 250,
                    Y2 = 260,
                    Stroke = Brushes.DarkViolet,
                    StrokeThickness = 1,
                };
                _canvas.Children.Add(uparrow);
                _canvas.Children.Add(downarrow);
                DrawText(load.StartLocation - 20, 220, load.StartMagnitude + " k/ft", Color.FromRgb(148, 0, 211));
                DrawText(load.EndLocation, 220, load.EndMagnitude + " k/ft", Color.FromRgb(148, 0, 211));
            }
        }


        private void DrawYDistributedLoad(DistributedLoad load)
        {
            var color = Brushes.DarkViolet;
            var units = "k/ft";
            if (load.Direction == LoadDirection.T)
            {
                units = "F";
            }
            if (load.Direction == LoadDirection.SY || load.Direction == LoadDirection.Sy)
            {
                units = "ksf";
                color = Brushes.DarkGreen;
            }
            var lineload = new Line
            {
                X1 = load.StartLocation,
                X2 = load.EndLocation,
                Y1 = 250 + load.StartMagnitude,
                Y2 = 250 + load.EndMagnitude,
                Stroke = color,
                StrokeThickness = 2
            }; ;
            _canvas.Children.Add(lineload);
            for (int i = 0; i < Math.Floor((load.EndLocation - load.StartLocation) / 20) + 1; i++)
            {
                var newx = load.StartLocation + i * 20;
                var line = new Line
                {
                    X1 = newx,
                    X2 = newx,
                    Y1 = 250,
                    Y2 = 250 + Line(newx, load),
                    Stroke = color,
                    StrokeThickness = 1
                };
                var larrow = new Line
                {
                    X1 = newx,
                    X2 = newx - 10,
                    Y1 = 250,
                    Y2 = (250 - Line(newx, load)) > 250 ? 230 : 270,
                    Stroke = color,
                    StrokeThickness = 1
                };
                var rarrow = new Line
                {
                    X1 = newx,
                    X2 = newx + 10,
                    Y1 = 250,
                    Y2 = (250 - Line(newx, load)) > 250 ? 230 : 270,
                    Stroke = color,
                    StrokeThickness = 1
                };
                _canvas.Children.Add(line);
                _canvas.Children.Add(larrow);
                _canvas.Children.Add(rarrow);
            }

            DrawText(load.StartLocation - 45, 250 + load.StartMagnitude, load.StartMagnitude + units, Color.FromRgb(0, 0, 0));
            DrawText(load.EndLocation + 10, 250 + load.EndMagnitude - 10, load.EndMagnitude + units, Color.FromRgb(0, 0, 0));
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
                _canvas.Children.Add(circle);
                DrawText(load.StartLocation, 230, load.StartMagnitude + "k/ft", Color.FromRgb(148, 0, 211));
                DrawText(load.EndLocation, 230, load.EndMagnitude + "k/ft", Color.FromRgb(148, 0, 211));
            }
        }
        #endregion
        private double Line(double x, DistributedLoad load)
        {
            return ((x - load.StartLocation) * (load.EndMagnitude - load.StartMagnitude)) / (load.EndLocation - load.StartLocation) + load.StartMagnitude;
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
