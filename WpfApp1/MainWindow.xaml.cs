using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainVm vm;

        public MainWindow()
        {
            //CultureInfo.CurrentCulture = CultureInfo.
            InitializeComponent();
            vm = new MainVm();
            vm.ProjectList.Add(new ProjectNode()
            {
                type = NodeEnum.project,
                NodeName = "Test",
                Child = new ObservableCollection<ProjectNode>()
                {
                    new ProjectNode()
                    {
                        NodeName="placeholder",
                        type= NodeEnum.placeholder
                    }
                }
            });
            DataContext = vm;
        }
    }
}