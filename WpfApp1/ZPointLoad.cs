using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class ZPointLoad : Shape
    {
        private double maxDiameter = 35;
        private double _scale = 0.5;
        private double Diameter => maxDiameter * Scale;
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
        protected override Geometry DefiningGeometry => DrawPointLoad();

        private Geometry DrawPointLoad()
        {
            StreamGeometry geom = new StreamGeometry();
            using (StreamGeometryContext ctx = geom.Open())
            {

                ctx.DrawGeometry(new EllipseGeometry(new Point(0, 0), Diameter, Diameter), false);
                ctx.DrawGeometry(new EllipseGeometry(new Point(0, 0), Diameter / 5, Diameter / 5), true);
            }

            return geom;
        }
    }

}
