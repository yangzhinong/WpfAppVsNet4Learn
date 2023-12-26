using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfGmap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ucMap.MapProvider = GMapProviders.BingMap;
            //ucMap.MapProvider = BaiduMapProvider.Instance;
            ucMap.MapProvider = BaiduMapProvider.Instance;
            //ucMap.Zoom = 15;
            ucMap.Zoom = 5;
            ucMap.ShowCenter = false;
            ucMap.DragButton = MouseButton.Left;
            ucMap.Position = new PointLatLng(32.064, 150.704);
            //ucMap.Position = new GMap.NET.PointLatLng(21.5053134635657, 67.8822576999664);
            ucMap.OnMapTypeChanged += UcMap_OnMapTypeChanged;
            ucMap.MouseLeftButtonDown += UcMap_MouseLeftButtonDown;
        }

        private void UcMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(ucMap);
            PointLatLng point = ucMap.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
            GMapMarker marker = new GMapMarker(point);
            var rect = new Rectangle();
            rect.Width = 10;
            rect.Height = 20;
            rect.Fill = Brushes.Red;
            //var brush = new ImageBrush();
            //brush.ImageSource= new Uri("")
            var dd = ucMap.FindResource("car");
            rect.Fill = ucMap.FindResource("car") as ImageBrush;
            rect.Stroke = Brushes.Transparent;
            rect.StrokeThickness = 0;
            rect.RenderTransform = new RotateTransform(90, 0.5, 0.5);
            rect.ToolTip = "hello";
            marker.Shape = rect;
            ucMap.Markers.Add(marker);
        }

        private void UcMap_OnMapTypeChanged(GMapProvider type)
        {
            
        }

        private void TextBox_PreviewKeyDown()
        {

        }
    }
}