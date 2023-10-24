using System.Windows;

namespace WpfCustomControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowBreakPanel : Window
    {
        public MainWindowBreakPanel()
        {
            InitializeComponent();
        }

        private void cmdFlipClick(object sender, RoutedEventArgs e)
        {
            //panel.IsFlipped = !panel.IsFlipped;
        }
    }
}