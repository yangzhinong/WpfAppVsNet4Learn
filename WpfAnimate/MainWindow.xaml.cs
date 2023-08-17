using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfAnimate
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

        private void cmdGrow_MouseEnter(object sender, MouseEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation()
            {
                To = this.Width - 30,
                Duration = TimeSpan.FromSeconds(1),
                FillBehavior = FillBehavior.Stop
            };
            SetValue(Button.WidthProperty, widthAnimation);

            cmdGrow.BeginAnimation(Button.WidthProperty, widthAnimation);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(widthAnimation);
        }

        private void cmdGrow_MouseLeave(object sender, MouseEventArgs e)
        {
            //DoubleAnimation widthAnimation = new DoubleAnimation()
            //{
            //    Duration = TimeSpan.FromSeconds(0.1)
            //};
            //cmdGrow.BeginAnimation(Button.WidthProperty, widthAnimation);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation()
            {
                To = this.Width - 30,
                Duration = TimeSpan.FromSeconds(1),
            };
            widthAnimation.SetValue(Button.WidthProperty, widthAnimation);
            widthAnimation.SetValue(Storyboard.TargetNameProperty, new PropertyPath("Width"));

            //cmdGrow.BeginAnimation(Button.WidthProperty, widthAnimation);

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(widthAnimation);
        }
    }
}