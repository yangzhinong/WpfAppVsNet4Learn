using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace CustomBehaviorsLibrary
{
    //拖动行为类
    // xmlns:i="clr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity"
    /* <Ellipse Canvas.Left="10" Canvas.Top="70" Fill="Blue"
                         Width="80" Height="60">
            <i:Interaction.Behaviors>
                <custom:DragInCanvasBehavior />
            </i:Interaction.Behaviors>
       </Ellipse>
    */

    public class DragInCanvasBehavior : Behavior<UIElement>
    {
        private bool _isDragging = false;
        private Point _mouseOffset;
        private Canvas _canvas;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseRightButtonUp;
        }

        private void AssociatedObject_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                AssociatedObject.ReleaseMouseCapture();
                _isDragging = false;
                if (AssociatedObject is FrameworkElement fe)
                {
                    fe.Cursor = Cursors.Arrow;
                }
            }
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point point = e.GetPosition(_canvas);
                AssociatedObject.SetValue(Canvas.TopProperty, point.Y - _mouseOffset.Y);
                AssociatedObject.SetValue(Canvas.LeftProperty, point.X - _mouseOffset.X);
            }
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_canvas == null)
            {
                _canvas = (Canvas)VisualTreeHelper.GetParent(this.AssociatedObject);
            }

            _isDragging = true;
            _mouseOffset = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            if (AssociatedObject is FrameworkElement fe)
            {
                fe.Cursor = Cursors.ScrollAll;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }
}