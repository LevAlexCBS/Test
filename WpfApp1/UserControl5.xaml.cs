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

        public IEnumerable<PointLoad> PointLoads { get; set; }
        public IEnumerable<DistributedLoad> DistributedLoads { get; set; }
        #endregion


        public UserControl5()
        {
            #region Test Loads
            PointLoads = new List<PointLoad>
            {
                //new PointLoad(-100,340, LoadDirection.Y),
                new PointLoad(-15,400, LoadDirection.Y),
                new PointLoad(-10,450, LoadDirection.Y),
                new PointLoad(-5,470, LoadDirection.Y),
                new PointLoad(-1,490, LoadDirection.Y),
                //new PointLoad(100,680, LoadDirection.Y),
                new PointLoad(15,590, LoadDirection.Y),
                new PointLoad(10,560, LoadDirection.Y),
                new PointLoad(5,530, LoadDirection.Y),
                new PointLoad(1,510, LoadDirection.Y),
                //new PointLoad(35, 700, LoadDirection.Z),
                //new PointLoad(35, 650, LoadDirection.Mx),
                //new PointLoad(-1, 250, LoadDirection.Mx),
                //new PointLoad(35, 550, LoadDirection.My),
                //new PointLoad(-1, 350, LoadDirection.My),
                //new PointLoad(35, 850, LoadDirection.Mz),
                //new PointLoad(-1, 500, LoadDirection.Mz),
            };
            DistributedLoads = new List<DistributedLoad>
            {
                //new DistributedLoad
                //{
                //    StartMagnitude = 0,
                //    EndMagnitude = 10,
                //    Direction = LoadDirection.X,
                //    StartLocation = 250,
                //    EndLocation = 500,
                //},
                //new DistributedLoad
                //{
                //    StartMagnitude = 30,
                //    EndMagnitude = 60,
                //    Direction = LoadDirection.Y,
                //    StartLocation = 200,
                //    EndLocation = 500,
                //},

                //new DistributedLoad
                //{
                //    StartMagnitude = 0,
                //    EndMagnitude = 100,
                //    Direction = LoadDirection.Mx,
                //    StartLocation = 300,
                //    EndLocation = 800,
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
            #endregion

            InitializeComponent();
            DrawMember();
            var drawPloads = new DrawingPointLoadsHelper(canvas, PointLoads);
            var drawDistrLoad = new DrawingDistributedLoadHelper(canvas, DistributedLoads);

            DrawText(500, 350, "Loading", Color.FromRgb(0, 0, 0));

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
     
    }

}
