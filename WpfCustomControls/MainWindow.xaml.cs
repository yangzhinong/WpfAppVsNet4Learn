using System.Windows;

namespace WpfCustomControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmdFlipClick(object sender, RoutedEventArgs e)
        {
            panel.IsFlipped = !panel.IsFlipped;
        }
    }
}