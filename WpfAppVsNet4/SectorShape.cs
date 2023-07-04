using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfAppVsNet4
{
    public class SectorShape : Shape
    {
        protected override Geometry DefiningGeometry { get => GetSectorGeometry(); }

        private Geometry GetSectorGeometry()
        {
            StreamGeometry geometry = new StreamGeometry();
            using (StreamGeometryContext c = geometry.Open())
            {
                c.BeginFigure(new Point(200, 200), true, true);
                c.LineTo(new Point(175, 50), true, true);
                c.ArcTo(new Point(50, 150),
                           new Size(200, 200),
                           0, true,
                           SweepDirection.Counterclockwise,
                           true,
                           true
                            );
                c.LineTo(new Point(200, 200), true, true);
            }
            return geometry;
        }
    }

    public class 正三角Shape : Shape
    {
        protected override Geometry DefiningGeometry { get => GetSectorGeometry(); }

        private Geometry GetSectorGeometry()
        {
            StreamGeometry geometry = new StreamGeometry();
            using (StreamGeometryContext c = geometry.Open())
            {
                c.BeginFigure(new Point(0, 100), true, true);
                c.LineTo(new Point(50, 100 - 100 * Math.Sin(Math.PI / 3)), true, true);
                c.LineTo(new Point(100, 100), true, true);
            }
            return geometry;
        }
    }
}