using System.Windows;

namespace DemoStylet
{
    public class MainShellViewModel: Stylet.Screen
    {
        public int ProgressValue { get; set; } = 20;

        
        public bool IsProgressShow { get; set; } = true;

        public Visibility ProgressVisibility=> IsProgressShow? Visibility.Visible : Visibility.Collapsed;

        public MainShellViewModel()
        {
        }
    }
}
