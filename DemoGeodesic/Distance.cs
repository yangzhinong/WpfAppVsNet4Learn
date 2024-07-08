using GeographicLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoGeodesic
{
    public class Distance
    {
        /// <summary>
        /// 地球半径(千米)
        /// </summary>
        private const double R = 6371;

        private const double PI = Math.PI;

        private static Func<double, double> rad = Radians;
        private static Func<double, double> sin = Math.Sin;
        private static Func<double, double> cos = Math.Cos;
        private static Func<double, double> sqrt = Math.Sqrt;
        private static Func<double, double, double> atan2 = Math.Atan2;

        /// <summary>
        /// Convert degrees to Radians
        /// </summary>
        /// <param name="x">Degrees</param>
        /// <returns>The equivalent in radians</returns>
        public static double Radians(double x)
        {
            return x * PI / 180;
        }

        /// <summary>
        /// Calculate the Great-circle Distance between two points using Haversine formula.
        /// 结果为千米(距离较为正确)
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns>The distance in kilometers.</returns>
        public static double DistanceComplex(double lat1, double lon1, double lat2, double lon2)
        {
            var havLat = sin(rad(lat1 - lat2) / 2);
            var havLon = sin(rad(lon1 - lon2) / 2);

            var a = havLat * havLat + cos(rad(lat1)) * cos(rad(lat2)) * havLon * havLon;

            return 2 * R * atan2(sqrt(a), sqrt(1 - a));
        }

        /// <summary>
        /// Calculate the Great-circle Distance between two points using Haversine formula.
        /// 结果为米(距离最准, 因为它考虑了地球是一个椭园球)
        /// https://blog.csdn.net/qq_27361945/article/details/79555778
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns>The distance in kilometers.</returns>
        public static double DistanceUseGeoLib(double lat1, double lon1, double lat2, double lon2)
        {
            return Geodesic.WGS84.Inverse(lat1, lon1, lat2, lon2, GeodesicFlags.Distance).Distance;
        }

        /// <summary>
        /// Calculate the distance between two points using simplified model. <br/>
        /// 误差最大(最简算法)
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns>The distance in kilometers.</returns>
        public static double Simplified(double lat1, double lon1, double lat2, double lon2)
        {
            var avgLat = rad(lat1 + lat2) / 2;
            var disLat = R * cos(avgLat) * rad(lon1 - lon2);
            var disLon = R * rad(lat1 - lat2);

            return sqrt(disLat * disLat + disLon * disLon);
        }

        /// <summary>
        /// 地图两点距离(大圆距离公式, 但是这个计算公式有个比较大的问题
        /// 当两点相距较近时, /Cos(latA-latB)误差比较大.
        /// </summary>
        public static double DistanceAppointment(double LatA, double LonA, double LatB, double LonB)
        {
            double x = Math.Cos(LatA * PI / 180.0) * Math.Cos(LatB * PI / 180.0) * Math.Cos((LonA - LonB) * PI / 180);
            double y = Math.Sin(LatA * PI / 180.0) * Math.Sin(LatB * PI / 180.0);
            double s = x + y;
            if (s > 1) s = 1;
            if (s < -1) s = -1;

            return Math.Acos(s) * R;
        }

        /// <summary>
        /// 返回两点的方向角(度)
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public static double CalculateHeading(double lat1, double lon1, double lat2, double lon2)
        {
            var ret = Geodesic.WGS84.Inverse(lat1, lon1, lat2, lon2, GeodesicFlags.Azimuth);
            return ret.Azimuth2;
        }

        public static double calculateHeading(double lat1, double lon1, double lat2, double lon2)
        {
            double fromLat = (double)lat1 * Math.PI / 180.0;
            double fromLng = (double)lon1 * Math.PI / 180.0;
            double toLat = (double)lat2 * Math.PI / 180.0;
            double toLng = (double)lon2 * Math.PI / 180.0;

            double dLng = toLng - fromLng;
            double heading = Math.Atan2(Math.Sin(dLng) * Math.Cos(toLat), Math.Cos(fromLat) * Math.Sin(toLat) - Math.Sin(fromLat) * Math.Cos(toLat) * Math.Cos(dLng));

            return heading * 180.0 / Math.PI;
        }

        /// <summary>
        /// 按某个方向移动点到
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1">原始经度</param>
        /// <param name="azimuth">方向(度)</param>
        /// <param name="distance">距离</param>
        /// <param name="lat2">输出新点纬度</param>
        /// <param name="lon2">输出新点经度</param>
        public static void Move(double lat1, double lon1, double azimuth, double distance, out double lat2, out double lon2)
        {
            var result = Geodesic.WGS84.Direct(lat1, lon1, azimuth, distance, GeodesicFlags.Longitude | GeodesicFlags.Latitude);
            lat2 = result.Latitude;
            lon2 = result.Longitude;
        }

        public static double GetPointToLineDistance(double lat1, double lon1, double lat2, double lon2, double latPoint, double lonPoint)
        {

            Geodesic geod = Geodesic.WGS84;
            var lineX = geod.InverseLine(lat1, lon1, lat2, lon2);
            var lineY = geod.Line(latPoint, lonPoint, lineX.Azimuth + 90);
            Intersect inter = new Intersect(geod);
            var point = inter.Closest(lineX, lineY);
            return Math.Abs(point.Y);
        }

        public static double GetPointToLineDistance2(double lat1, double lon1, double lat2, double lon2, double latPoint, double lonPoint)
        {
            Geodesic geod = Geodesic.WGS84;
            var lineX = geod.InverseLine(lat1, lon1, lat2,lon2);

            var p = new PolygonArea(Geodesic.WGS84, false);
            p.AddPoint(lat1, lon1);
            p.AddPoint(lat2, lon2);
            p.AddPoint(latPoint, lonPoint);
            var r= p.Compute(false, false);
            var result = r.area / lineX.Distance;
            return result;
        }

        

        public static double GetArea(double[,] corrds)
        {
            double result = 0f;
            try
            {
                var p = new PolygonArea(Geodesic.WGS84, false);
                var size = corrds.Length / corrds.Rank;
                for (int i=0;i<size; i++)
                {

                    p.AddPoint(corrds[i,0], corrds[i,1]);
                }
                var r = p.Compute(false,false);
                result = r.area;

            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }
    }
}
