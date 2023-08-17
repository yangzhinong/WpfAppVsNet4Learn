using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace WpfAppVsNet4;

public class SectorVisual : DrawingVisual
{
    public SectorVisual()
    {
        StreamGeometry geometry = new();
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

        using DrawingContext context = RenderOpen();
        Pen pen = new(Brushes.Black, 1);
        context.DrawGeometry(Brushes.CornflowerBlue, pen, geometry);
        context.DrawText(new FormattedText("Hello Wpf!",
            CultureInfo.GetCultureInfo("en-us"),
            FlowDirection.LeftToRight,
            new Typeface("Sego UI"),
            36, Brushes.Black), new Point(10, 10));
    }
}