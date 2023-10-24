using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class MainVm : ViewModelBase
    {
        private ObservableCollection<ProjectNode> _ProjectList = new ObservableCollection<ProjectNode>();

        /// <summary>
        /// 项目集合
        /// </summary>
        public ObservableCollection<ProjectNode> ProjectList
        {
            get
            {
                return _ProjectList;
            }
            set
            {
                if (value != _ProjectList)
                {
                    _ProjectList = value;
                    OnPropertyChanged("ProjectList");
                }
            }
        }
    }
}