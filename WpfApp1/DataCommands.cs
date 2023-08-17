using System.Windows.Input;

namespace WpfApp1
{
    public class DataCommands
    {
        private static RoutedUICommand _requery;

        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control));
            _requery = new RoutedUICommand("Requery", "Requery",
                                                typeof(DataCommands), inputs);
        }

        public static RoutedUICommand Requery => _requery;
    }
}