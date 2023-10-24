using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfCustomControls
{
    public class CustomDrawnElement : FrameworkElement
    {
        static CustomDrawnElement()
        {
            var meta = new FrameworkPropertyMetadata(Colors.Yellow)
            {
                AffectsRender = true
            };
            BackgroundColorProperty =
           DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(CustomDrawnElement), meta);
        }

        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty BackgroundColorProperty;

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.InvalidateVisual();
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            Rect bounds = new Rect(0, 0, base.ActualWidth, base.ActualHeight);

            dc.DrawRectangle(GetForegroundBrush(), null, bounds);
        }

        private Brush GetForegroundBrush()
        {
            if (!IsMouseOver)
            {
                return new SolidColorBrush(BackgroundColor);
            }
            else
            {
                RadialGradientBrush brush = new RadialGradientBrush(Colors.White, BackgroundColor);

                Point absoluteGradientOrigin = Mouse.GetPosition(this);

                Point relativeGradientOrigin = new Point(
                    absoluteGradientOrigin.X / base.ActualWidth,
                    absoluteGradientOrigin.Y / base.ActualHeight);

                brush.GradientOrigin = relativeGradientOrigin;
                brush.Center = relativeGradientOrigin;

                return brush;
            }
        }
    }
}