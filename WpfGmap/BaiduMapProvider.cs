using GMap.NET;
using System;

namespace WpfGmap
{
    /// <summary>
    /// Gmap组件需要的百度地图提供器
    /// </summary>
    public class BaiduMapProvider : MapProviderBase
    {
        static BaiduMapProvider()
        {
            Instance = new BaiduMapProvider();
        }

        public BaiduMapProvider() : base("http://map.baidu.com")
        {
        }

        public static readonly BaiduMapProvider Instance;
        private readonly Guid id = new Guid("47C1561B-C785-4EBF-9EC3-2CF0E416E219");

        public override GMap.NET.PureImage GetTileImage(GMap.NET.GPoint pos, int zoom)
        {
            try
            {
                string url = MakeTileImageUrl(pos, zoom, LanguageStr);
                return GetTileImageUsingHttp(url);
            }
            catch
            {
                return null;
            }
        }

        private string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            zoom = zoom - 1;
            var offsetX = Math.Pow(2, zoom);
            var offsetY = offsetX - 1;
            var numX = pos.X - offsetX;
            var numY = -pos.Y + offsetY;
            zoom = zoom + 1;
            var num = (pos.X + pos.Y) % 8 + 1;
            var x = numX.ToString().Replace("-", "M");
            var y = numY.ToString().Replace("-", "M");
            //原来：http://q3.baidu.com/it/u=x=721;y=209;z=12;v=014;type=web&fm=44
            //更新：http://online1.map.bdimg.com/tile/?qt=tile&x=23144&y=6686&z=17&styles=pl
            //string url = string.Format(UrlFormat, num, x, y, zoom, "014", "web", "44");
            string url = string.Format(UrlFormat, x, y, zoom);
            return url;
        }

        //static readonly string UrlFormat = "http://q{0}.baidu.com/it/u=x={1};y={2};z={3};v={4};type={5}&fm={6}";
        private static readonly string UrlFormat = "http://online1.map.bdimg.com/tile/?qt=tile&x={0}&y={1}&z={2}&styles=pl";

        public override Guid Id
        {
            get { return id; }
        }

        private readonly string name = "BaiduMap";

        public override string Name
        {
            get
            {
                return name;
            }
        }
    }
}