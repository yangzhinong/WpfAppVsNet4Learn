using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfCustomControls
{
    [TemplatePart(Name = "PART_RedSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_GreenSlider", Type = typeof(RangeBase))]
    [TemplatePart(Name = "PART_BlueSlider", Type = typeof(RangeBase))]
    public class ColorPicker : System.Windows.Controls.Control
    {
        private Color? _previousColor;

        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                            typeof(ColorPicker),
                 new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }

        public ColorPicker()
        {
            SetUpCommands();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PART_RedSlider") is RangeBase redSlider)
            {
                Binding binding = new Binding("Red")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                };
                redSlider.SetBinding(RangeBase.ValueProperty, binding);
            }

            if (GetTemplateChild("PART_GreenSlider") is RangeBase greenSlider)
            {
                Binding binding = new Binding("Green")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                };
                greenSlider.SetBinding(RangeBase.ValueProperty, binding);
            }

            if (GetTemplateChild("PART_BlueSlider") is RangeBase blueSlider)
            {
                Binding binding = new Binding("Blue")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                };
                blueSlider.SetBinding(RangeBase.ValueProperty, binding);
            }

            if (GetTemplateChild("PART_PreviewBrush") is SolidColorBrush brush)
            {
                Binding binding = new Binding("Color")
                {
                    Source = brush,
                    Mode = BindingMode.OneWayToSource
                };
                this.SetBinding(ColorPicker.ColorProperty, binding);
            }
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Black, OnColorChanged));

        public int Red
        {
            get { return (int)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Red.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RedProperty =
            DependencyProperty.Register("Red", typeof(int), typeof(ColorPicker), new PropertyMetadata(0, OnRgbColorChanged));

        public int Blue
        {
            get { return (int)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Blue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueProperty =
            DependencyProperty.Register("Blue", typeof(int), typeof(ColorPicker), new PropertyMetadata(0, OnRgbColorChanged));

        public int Green
        {
            get { return (int)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Green.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GreenProperty =
            DependencyProperty.Register("Green", typeof(int), typeof(ColorPicker), new PropertyMetadata(0, OnRgbColorChanged));

        private void SetUpCommands()
        {
            CommandBinding binding = new CommandBinding(ApplicationCommands.Undo);
            binding.Executed += UndoCommmandExecuted;
            binding.CanExecute += UndoCommandCanExecute;
            this.CommandBindings.Add(binding);
        }

        private void UndoCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _previousColor.HasValue;
        }

        private void UndoCommmandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Color = _previousColor.Value;
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            Color oldColor = (Color)e.OldValue;

            ColorPicker colorPicker = (ColorPicker)d;
            colorPicker._previousColor = oldColor;
            colorPicker.Red = newColor.R;
            colorPicker.Green = newColor.G;
            colorPicker.Blue = newColor.B;

            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(
                    oldColor, newColor, ColorChangedEvent);
            colorPicker.RaiseEvent(args); //3.触发路由事件
        }

        private static void OnRgbColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)d;
            Color color = colorPicker.Color;

            if (e.Property == RedProperty)
            {
                color.R = byte.Parse($"{e.NewValue:d0}");
            }
            else if (e.Property == GreenProperty)
            {
                color.G = byte.Parse($"{e.NewValue:d0}");
            }
            else if (e.Property == BlueProperty)
            {
                color.B = byte.Parse($"{e.NewValue:d0}");
            }

            colorPicker.Color = color;
        }

        /// <summary>
        /// 1.定义路由事件
        /// </summary>
        public static readonly RoutedEvent ColorChangedEvent =
                            EventManager.RegisterRoutedEvent("ColorChanged",
                                        RoutingStrategy.Bubble, //冒泡方式
                                        typeof(RoutedPropertyChangedEventHandler<Color>),  //RoutedEventHandler
                                        typeof(ColorPicker));

        /// <summary>
        /// 2创建标准的.net事件封装器
        /// </summary>
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        /// 3.触发路由事件, 比如依赖属性的Change回调方法 colorPicker.RaiseEvent(RoutedPropertyChangedEventArgs<Color> arg);
        /*
           private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
           {
                Color newColor = (Color)e.NewValue;
                Color oldColor = (Color)e.OldValue;
                ColorPicker colorPicker = (ColorPicker)d;
                RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(
                                                                    oldColor, newColor, ColorChangedEvent);
                colorPicker.RaiseEvent(args); //3.触发路由事件
           }
        */
    }
}