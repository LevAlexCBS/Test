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
    /// Interaction logic for UserControl4.xaml
    /// </summary>
    public partial class UserControl4 : UserControl
    {
        public double StartPointX { get; set; } = 0;
        public double StartPointY { get; set; } = 400;
        public double EndtPointX { get; set; } = 1000;
        public double EndPointY { get; set; } = 100;
        public double iOffset { get; set; } = 100;
        public double jOffset { get; set; } = 0;
        public UserControl4()
        {
            InitializeComponent();

            var ldline = new Line() { X1 = StartPointX + iOffset, X2 = EndtPointX - jOffset, Y1 = StartPointY, Y2 = EndPointY, Stroke = Brushes.Blue, StrokeThickness = 1 };
            var member = new Line() { X1 = 0, X2 = 1000, Y1 = 450, Y2 = 450, Stroke = Brushes.Black, StrokeThickness = 1 };
            var offset = new Line() { X1 = 0, X2 = iOffset, Y1 = 450, Y2 = 450, Stroke = Brushes.Gray, StrokeThickness = 5 };
            var inode = new Ellipse() { Width = 5, Height = 5, Stroke = Brushes.Black, Fill = Brushes.Black };
            var jnode = new Ellipse() { Width = 5, Height = 5, Stroke = Brushes.Black, Fill = Brushes.Black };

            loadline.Children.Add(ldline);
            loadline.Children.Add(offset);
            loadline.Children.Add(member);
            Canvas.SetTop(inode, 450);
            loadline.Children.Add(inode);
            Canvas.SetTop(jnode, 450);
            Canvas.SetLeft(jnode, 1000);
            loadline.Children.Add(jnode);

            for (int i = 0; i < Math.Ceiling((loadline.Width - iOffset) / 10) ; i++)
            {
                var newx = i * 10;
                var line = new Line() { X1 = iOffset + newx, X2 = iOffset + newx, Y1 = 450, Y2 = Line().Invoke(newx) + StartPointY, Stroke = Brushes.Blue };

                loadline.Children.Add(line);

            }
        }

        private Func<double, double> Line()
        {
            return x => ((x - StartPointX) * (EndPointY - StartPointY)) / (EndtPointX - StartPointX);
        }

    }
}
