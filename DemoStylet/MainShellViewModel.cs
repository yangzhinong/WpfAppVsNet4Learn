using PropertyChanged;
using System.Diagnostics;
using System.Windows;

namespace DemoStylet
{
    [SuppressPropertyChangedWarnings]
    public class MainShellViewModel: Stylet.Screen
    {
        public int ProgressValue { get; set; } = 20;

        public bool IsProgressShow { get; set; } = true;

        [OnChangedMethod(nameof(OnMyNameChanged))]
        public string Name { get; set; }

        private void OnMyNameChanged()
        {
            Debug.WriteLine("Name changed");
        }

        
        public Visibility ProgressVisibility=> IsProgressShow? Visibility.Visible : Visibility.Collapsed;

        public MainShellViewModel()
        {
        }
    }
}
