using System;
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
        public string StartLabelLoad { get; set; } = "-5k/ft";
        public string EndLabelLoad { get; set; } = "-25k/ft";
        public bool IsZDistributedLoad { get; set; } = false;
        public bool IsXYDistributedLoad { get; set; } = true;
        public double StartMagnitude
        {
            get
            {
                return _startMagnitude;
            }
            set
            {
                _startMagnitude -= value;
            }
        }
        public double EndMagnitude { get; set; } = 0;
        public List<ZPointLoad> ZLoads { get; set; }
        #endregion

        #region Private members
        private double _startMagnitude = 250;
        private double StartPointX => IOffset;
        private double EndPointX => 1000 - JOffset;
        #endregion

        public UserControl5()
        {

            StartMagnitude = 100;
            ZLoads = new List<ZPointLoad>
            {
                new ZPointLoad(10,200),
                new ZPointLoad(25, 500),
                new ZPointLoad(35, 700)
            };


            InitializeComponent();
            DrawMember();
            if (IsZDistributedLoad)
                DrawZDistributedLoads();
            if (IsXYDistributedLoad)
                DrawXYDistributedLoads();
            DrawZPointLoads();

            DrawText(500, 300, "Loading", Color.FromRgb(0, 0, 0));

        }

        private void DrawZPointLoads()
        {
            if (ZLoads == null)
                return;
            foreach (var item in ZLoads)
            {
                var load = new Ellipse { Width = item.Magnitude, Height = item.Magnitude, Stroke = Brushes.Blue};
                Canvas.SetLeft(load, item.Location);
                Canvas.SetTop(load, 250 - load.Height/2);
                var inload = new Ellipse { Width = item.Magnitude/5, Height = item.Magnitude/5, Stroke = Brushes.Blue, Fill = Brushes.Blue };
                Canvas.SetLeft(load, item.Location);
                Canvas.SetTop(load, 250 - load.Width / 2);
                Canvas.SetLeft(inload, item.Location + load.Width/2 - inload.Width/2);
                Canvas.SetTop(inload, 250 - inload.Width/2);
                DrawText(item.Location + 20, 230 - load.Height / 2, item.Magnitude + "k", Color.FromRgb(0,0,255));
                canvas.Children.Add(load);
                canvas.Children.Add(inload);
            }
        }

        private void DrawXYDistributedLoads()
        {
            var lineload = new Line { X1 = StartPointX, X2 = EndPointX, Y1 = StartMagnitude, Y2 = EndMagnitude, Stroke = Brushes.DarkViolet, StrokeThickness = 2 }; ;
            canvas.Children.Add(lineload);
            for (int i = 0; i < Math.Floor((1000 - JOffset - IOffset) / 20) + 1; i++)
            {
                var newx = StartPointX + i * 20;
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

            DrawText(StartPointX - 45, StartMagnitude, StartLabelLoad, Color.FromRgb(0, 0, 0));
            DrawText(EndPointX + 10, EndMagnitude - 10, EndLabelLoad, Color.FromRgb(0, 0, 0));
        }
        private void DrawZDistributedLoads()
        {
            var loads = new Line
            {
                X1 = 0 + IOffset,
                Y1 = 250,
                X2 = 1000 - JOffset,
                Y2 = 250,
                StrokeThickness = 6,
                StrokeDashArray = new DoubleCollection() { 1, 2 },
                Stroke = Brushes.DarkViolet
            };
            canvas.Children.Add(loads);
        }

        private double Line(double x)
        {
            return ((x - StartPointX) * (EndMagnitude - StartMagnitude)) / (EndPointX - StartPointX) + StartMagnitude;
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
                StrokeThickness = 6,
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

            DrawText(10, 255, INode, Color.FromRgb(0, 255, 0));
            DrawText(1010, 255, JNode, Color.FromRgb(0, 255, 0));
        }
    }

}
