using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfCustomControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBlock _selTxt;
        public MainWindow()
        {
            InitializeComponent();
            _selTxt = txtSecond;
        }

        private void cmdFlipClick(object sender, RoutedEventArgs e)
        {
            //panel.IsFlipped = !panel.IsFlipped;
        }

        private void ButtonSpinner_Spin(object sender, SpinEventArgs e)
        {
            if (e.Direction == SpinDirection.Increase)
            {
                var i = int.Parse(_selTxt.Text);
                i++;
                if (i == 60 && _selTxt.Name!="txtHour" || i==24 && _selTxt.Name =="txtHour")
                {
                    i = 0;
                }
                _selTxt.Text = i.ToString("00");
            } else if (e.Direction == SpinDirection.Decrease)
            {
                var i = int.Parse(_selTxt.Text);
                i--;
                if (i == -1)
                {
                    if (_selTxt.Name== "txtHour")
                    {
                        i = 23;
                    } else
                    {
                        i = 59;
                    }
                }
                _selTxt.Text = i.ToString("00");
            }
        }

       
        private void MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TextBlock txts)
            {
                _selTxt = txts;
            }

            if ( spin.Content is StackPanel st)
            {
                foreach(TextBlock txt in st.Children)
                {
                    txt.Foreground = Brushes.Black;
                    txt.Background = Brushes.White;
                }
                //_selTxt.Foreground = Brushes.White;
                //_selTxt.Background = Brushes.Black;
            }
            
        }
    }
}