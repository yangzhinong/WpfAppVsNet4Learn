using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WpfCustomControls
{
    [TemplateVisualState(Name = "Noraml", GroupName = "ViewStates")]
    [TemplateVisualState(Name = "Flipped", GroupName = "ViewStates")]
    [TemplatePart(Name = "FlipButton", Type = typeof(ToggleButton))]
    [TemplatePart(Name = "FlipButtonAlternate", Type = typeof(ToggleButton))]
    public class FlipPanel : Control
    {
        //无外观控件的静态方法
        static FlipPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipPanel), new FrameworkPropertyMetadata(typeof(FlipPanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("FlipButton") is ToggleButton flipButton)
            {
                flipButton.Click += FlipButton_Click;
            }

            if (GetTemplateChild("FlipButtonAlternate") is ToggleButton flipButtonAlternate)
            {
                flipButtonAlternate.Click += FlipButton_Click;
            }

            this.ChangeVisualState(false);
        }

        private void FlipButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsFlipped = !this.IsFlipped;

            ChangeVisualState(true);
        }

        public object FrontContent
        {
            get { return (object)GetValue(FrontContentProperty); }
            set { SetValue(FrontContentProperty, value); }
        }

        public static readonly DependencyProperty FrontContentProperty =
            DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel), new PropertyMetadata(null));

        public object BackContent
        {
            get { return (object)GetValue(BackContentProperty); }
            set { SetValue(BackContentProperty, value); }
        }

        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), new PropertyMetadata(null));

        public bool IsFlipped
        {
            get { return (bool)GetValue(IsFlippedProperty); }
            set
            {
                SetValue(IsFlippedProperty, value);
                ChangeVisualState(true);
            }
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (!IsFlipped)
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Flipped", useTransitions);
            }
        }

        public static readonly DependencyProperty IsFlippedProperty =
            DependencyProperty.Register("IsFlipped", typeof(bool), typeof(FlipPanel), new PropertyMetadata(false));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel), null);
    }
}