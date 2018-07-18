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
    /// Interaction logic for UserControl3.xaml
    /// </summary>
    public partial class UserControl3
    {
        public UserControl3()
        {
            InitializeComponent();
            UIElement[] _ploads = new UIElement[6];
            for (int i = 0; i < 3; i++)
            {
                var arrow = new Polygon()
                {
                    Fill = Brushes.Blue,
                    Points = new PointCollection()
                    {
                        new Point(0, -25),
                        new Point(15, 4),
                        new Point(30, -25)
                    }
                };
                var line = new Line()
                {
                    Stroke = Brushes.Blue,
                    X1 = 15,
                    Y1 = 0,
                    X2 = 15,
                    Y2 = -100
                };
                var text = new TextBlock(){Text = "-15k"};
                Canvas.SetLeft(arrow, i * 100);
                Canvas.SetLeft(line, i * 100);
                canvas.Children.Add(arrow);
                canvas.Children.Add(line);
                Canvas.SetLeft(text, (i * 100) - 20);
                Canvas.SetBottom(text, 100);
                canvas.Children.Add(text);
            }


        }
    }
}
