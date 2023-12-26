using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DemoPropertyChanged
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Read();
        }
    }

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PrsonNew3 : BaseViewModel
    {
        public string Name { get; set; }
    }

    public class PrsonNew4
    {
        private string _name;
        public string Name { get=> _name; set {

                if (string.Compare(_name,value?.Trim(), StringComparison.OrdinalIgnoreCase) != 0 )
                {
                    _name = value?.Trim();
                }
            } }
    }

    public class PrsonNew5
    {
        private string _name;
        public string Name
        {
            get => _name; set
            {
                var valTrim= value?.Trim();
                if (string.Compare(_name, valTrim, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    _name = valTrim;
                }
            }
        }
    }
}
