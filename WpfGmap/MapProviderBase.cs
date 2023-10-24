using GMap.NET.MapProviders;
using GMap.NET.Projections;
using System;

namespace WpfGmap
{
    public abstract class MapProviderBase : GMapProvider
    {
        public MapProviderBase(string refererUrl)
        {
            MaxZoom = null;
            RefererUrl = refererUrl;
            Copyright = string.Format("©{0} Baidu Corporation, ©{0} NAVTEQ, ©{0} Image courtesy of NASA", DateTime.Today.Year);
        }

        public override GMap.NET.PureProjection Projection
        {
            get { return MercatorProjection.Instance; }
        }

        private GMapProvider[] overlays;

        public override GMapProvider[] Overlays
        {
            get
            {
                if (overlays == null)
                {
                    overlays = new GMapProvider[] { this };
                }
                return overlays;
            }
        }
    }
}