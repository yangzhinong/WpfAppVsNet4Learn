using System.Windows;

namespace WpfCustomControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowCopy : Window
    {
        public MainWindowCopy()
        {
            InitializeComponent();
        }

        private void cmdFlipClick(object sender, RoutedEventArgs e)
        {
            panel.IsFlipped = !panel.IsFlipped;
        }
    }
}