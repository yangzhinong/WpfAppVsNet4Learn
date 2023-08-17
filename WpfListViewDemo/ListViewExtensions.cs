using System.Windows;

namespace WpfListViewDemo;

public static class ListViewExtensions
{
    public static string GetGroup(DependencyObject obj)
    {
        return (string)obj.GetValue(GroupProperty);
    }

    public static void SetGroup(DependencyObject obj, string value)
    {
        obj.SetValue(GroupProperty, value);
    }

    // Using a DependencyProperty as the backing store for Group.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GroupProperty =
        DependencyProperty.RegisterAttached("Group", typeof(string), typeof(ListViewExtensions), new PropertyMetadata(""));
}