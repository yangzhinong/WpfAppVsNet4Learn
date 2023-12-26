using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfRxDemo
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
        Polyline line = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var mouseDonws = Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseDown));

            var mouseUp = Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseUp));

            var movements = Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseMove));

            movements.SkipUntil(mouseDonws.Do( _ =>
                                  {
                                      line = new Polyline() { Stroke = Brushes.Black, StrokeThickness = 3 };
                                      canvas.Children.Add(line);
                                  }))
                      .TakeUntil(mouseUp)
                      .Repeat()
                      .Select(x => x.EventArgs.GetPosition(this))
                      .Subscribe(pos => line.Points.Add(pos));

            

        }
    }
}
