using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //CultureInfo.CurrentCulture = CultureInfo.
            InitializeComponent();
        }

        private void ColorPicker_ColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color> e)
        {
            txt.Text = "Color is " + e.NewValue.ToString();
        }
    }
}