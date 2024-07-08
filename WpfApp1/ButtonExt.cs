using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1
{
    public class ButtonExt
    {


        public static int GetRound(DependencyObject obj)
        {
            return (int)obj.GetValue(RoundProperty);
        }

        public static void SetRound(DependencyObject obj, int value)
        {
            obj.SetValue(RoundProperty, value);
        }

        // Using a DependencyProperty as the backing store for Round.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoundProperty =
            DependencyProperty.RegisterAttached("Round", typeof(int), typeof(ButtonExt), new PropertyMetadata(0, RoundChanedCallback));

        private static void RoundChanedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            for (var i= 0; i< VisualTreeHelper.GetChildrenCount(d); i++)
            {
                var child = VisualTreeHelper.GetChild(d, i);
                if (child is Border)
                {
                   child.SetValue(  Border.CornerRadiusProperty, e.NewValue );
                    break;
                }
            }
        }
    }
}
