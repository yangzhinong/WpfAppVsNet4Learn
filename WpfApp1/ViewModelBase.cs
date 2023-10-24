using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructor

        public ViewModelBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Constructor

        #region Private Methods

        #endregion Private Methods

        //Protected & Public Methods
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            //OnPropertyChanging(propertyName);
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}