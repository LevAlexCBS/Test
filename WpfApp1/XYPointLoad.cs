using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class XYPointLoad : Shape
    {
        private double _scale = 0.1;
        public Thickness TextPos => new Thickness(20, _scale, 30, 0);
        public double Scale
        {
            get => _scale;
            set
            {
                if (value < 0.1)
                {
                    _scale = 0.1;
                }
                else if (value >= 1)
                {
                    _scale = 1;
                }
                else
                {
                    _scale = value;
                }

            }
        }
        protected override Geometry DefiningGeometry => DrawXYLoad();

        private Geometry DrawXYLoad()
        {
            StreamGeometry geom = new StreamGeometry();
            using (StreamGeometryContext gc = geom.Open())
            {
                gc.BeginFigure(new Point(0 * Scale, -25* Scale), true, false);
                gc.PolyLineTo(new PointCollection()
                {
                    new Point(15 * Scale, 4*Scale),
                    new Point(15 * Scale, -100*Scale)
                }, true, true);
                gc.PolyLineTo(new PointCollection()
                    {
                        new Point(15*Scale, 4*Scale),
                        new Point(30*Scale, -25*Scale)
                    }, true, true);

            }
            return geom;
        }
    }
}
