using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfAppVsNet4
{
    public class VisulaContainer : FrameworkElement
    {
        private SectorVisual _visual = new SectorVisual();

        protected override Visual GetVisualChild(int index)
        {
            return _visual;
        }

        protected override int VisualChildrenCount => 1;
    }
}