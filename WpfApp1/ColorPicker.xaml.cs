using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        private Color? _previousColor;

        public ColorPicker()
        {
            InitializeComponent();
            SetUpCommands();
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