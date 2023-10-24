using System.Windows;
using System.Windows.Controls;

namespace WpfCustomControls
{
    public class CanvasClone : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = new Size(double.PositiveInfinity, double.PositiveInfinity);

            foreach (UIElement element in base.InternalChildren)
            {
                element.Measure(size);
            }
            return new Size();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement element in base.InternalChildren)
            {
                double x = 0, y = 0, left = Canvas.GetLeft(element);
                if (!double.IsNaN(left))
                {
                    x = left;
                }

                double top = Canvas.GetTop(element);
                if (!double.IsNaN(top))
                {
                    y = top;
                }
                element.Arrange(new Rect(new Point(x, y), element.DesiredSize));
            }
            return finalSize;
        }
    }
}