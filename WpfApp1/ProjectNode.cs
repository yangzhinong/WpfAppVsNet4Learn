using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    // <summary>
    /// 项目树状节点模型
    /// </summary>
    public class ProjectNode : ViewModelBase
    {
        private string _NodeName;

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName
        {
            get
            {
                return _NodeName;
            }
            set
            {
                if (_NodeName != value)
                {
                    _NodeName = value;
                    OnPropertyChanged("NodeName");
                }
            }
        }

        private string _FilePath;

        /// <summary>
        /// date数据目录
        /// </summary>
        public string FilePath
        {
            get
            {
                return _FilePath;
            }
            set
            {
                if (_FilePath != value)
                {
                    _FilePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }

        private List<string> _LstFilePath;

        /// <summary>
        /// date数据目录下的子目录
        /// </summary>
        public List<string> LstFilePath
        {
            get
            {
                return _LstFilePath;
            }
            set
            {
                if (_LstFilePath != value)
                {
                    _LstFilePath = value;
                    OnPropertyChanged("LstFilePath");
                }
            }
        }

        private ObservableCollection<ProjectNode> _Child = new ObservableCollection<ProjectNode>();

        /// <summary>
        /// daf数据子列表
        /// </summary>
        public ObservableCollection<ProjectNode> Child
        {
            get
            {
                return _Child;
            }
            set
            {
                if (_Child != value)
                {
                    _Child = value;
                    OnPropertyChanged("Child");
                }
            }
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public NodeEnum type { get; set; }

        /// <summary>
        /// 展开节点
        /// </summary>
        private bool isExpanded = true;

        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set
            {
                if (value != isExpanded)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        private bool isSelected = false;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        private string _TreeImage = "/ICTC.RZPC.Common.ControlTheme;component/image/treeFile.svg";

        /// <summary>
        /// 树图片
        /// </summary>
        public string TreeImage
        {
            get
            {
                return _TreeImage;
            }
            set
            {
                if (_TreeImage != value)
                {
                    _TreeImage = value;
                    OnPropertyChanged("TreeImage");
                }
            }
        }
    }

    public enum NodeEnum
    {
        project,
        configFolder,
        dafFolder,

        /// <summary>原始数据</summary>
        primaryDataFolder,

        /// <summary>二次校对数据</summary>
        secondDataFolder,

        /// <summary>上传数据</summary>
        uploadedDataFolder,

        /// <summary>日期文件夹</summary>
        dateFolder,

        /// <summary>节点文件夹</summary>
        nodeFolder,

        /// <summary>占位</summary>
        placeholder,
    }
}