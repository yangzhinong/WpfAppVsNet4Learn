using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfAppVsNet4
{
    /// <summary>
    /// Interaction logic for Msg.xaml
    /// </summary>
    public partial class Msg : UserControl
    {
        public Msg(VM_Msg vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void bdMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VisualStateManager.GoToState(this, "HasRead", false);

            var bindExp = BindingOperations.GetBindingExpression(txt, TextBlock.TextProperty);
            var s = bindExp.ToString();
            Console.WriteLine("");
        }
    }

    public class VM_Msg
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}