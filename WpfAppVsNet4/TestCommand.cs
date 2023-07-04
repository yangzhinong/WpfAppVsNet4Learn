using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfAppVsNet4
{
    public class TestCommand : ICommand
    {
        private Random _random = new Random();

        public bool CanExecute(object parameter)
        {
            return (_random.NextDouble() < 0.5);
        }

        public void Execute(object parameter)
        {
            Debug.WriteLine("TestCommand Executed");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}