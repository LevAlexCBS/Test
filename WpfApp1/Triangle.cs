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
    public class Triangle : Shape
    {
        protected override Geometry DefiningGeometry => DrawTriangle();

        private Geometry DrawTriangle()
        {
            StreamGeometry geom = new StreamGeometry();
            using (StreamGeometryContext gc = geom.Open())
            {
                // isFilled = false, isClosed = true
                gc.BeginFigure(new Point(), false, false);
                gc.PolyLineTo(new PointCollection()
                    {
                        new Point(0, -25),
                        new Point(15, 4),
                        new Point(30, -25)
                    }, true, true);
                
            }
            return geom;
        }
    }
}
