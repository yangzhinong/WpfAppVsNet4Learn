using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfAppVsNet4
{
    public class GridLineDivideEquallyHelper
    {
        public static bool GetEnable(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableProperty);
        }

        public static void SetEnable(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableProperty, value);
        }

        // Using a DependencyProperty as the backing store for Enable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableProperty =
            DependencyProperty.RegisterAttached("Enable", typeof(bool), typeof(GridLineDivideEquallyHelper), new PropertyMetadata(false, OnEnableChanged));

        public static int GetRows(DependencyObject obj)
        {
            return (int)obj.GetValue(RowsProperty);
        }

        public static void SetRows(DependencyObject obj, int value)
        {
            obj.SetValue(RowsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached("Rows", typeof(int), typeof(GridLineDivideEquallyHelper), new PropertyMetadata(1));

        public static int GetCols(DependencyObject obj)
        {
            return (int)obj.GetValue(ColsProperty);
        }

        public static void SetCols(DependencyObject obj, int value)
        {
            obj.SetValue(ColsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.RegisterAttached("Cols", typeof(int), typeof(GridLineDivideEquallyHelper), new PropertyMetadata(1));

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Grid grid)
            {
                if ((bool)e.NewValue)
                {
                    grid.Loaded -= OnGridLoaded;
                    grid.Loaded += OnGridLoaded;
                }
            }
        }

        private static void OnGridLoaded(object sender, RoutedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
            {
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();

                var rows = GetRows(grid);
                var cols = GetCols(grid);

                if (rows < 1) rows = 1;
                if (cols < 1) cols = 1;

                for (var i = 0; i < rows; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
                for (var j = 0; j < cols; j++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                for (var i = 0; i < rows; i++)
                {
                    if (i == 0)
                    {
                        var line0 = new Line();
                        Grid.SetRow(line0, i);
                        grid.Children.Add(line0);
                        line0.Style = (Style)grid.FindResource("horizontalLineStyle0");
                    }
                    var line1 = new Line();
                    Grid.SetRow(line1, i);
                    grid.Children.Add(line1);
                    line1.Style = (Style)grid.FindResource("horizontalLineStyle");
                }
                for (var j = 0; j < cols; j++)
                {
                    if (j == 0)
                    {
                        var line0 = new Line();
                        Grid.SetColumn(line0, j);
                        grid.Children.Add(line0);
                        line0.Style = (Style)grid.FindResource("verticalLineStyle0");
                    }
                    var line1 = new Line();
                    Grid.SetColumn(line1, j);
                    grid.Children.Add(line1);
                    line1.Style = (Style)grid.FindResource("verticalLineStyle");
                }
            }
        }
    }
}