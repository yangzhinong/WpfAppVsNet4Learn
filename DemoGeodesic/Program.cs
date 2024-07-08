using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoGeodesic
{
    internal class Program
    {
        private static double[,] coords =   { 
            { 22.656304210485004, 114.06466523520893 },
            { 22.651868529042016, 114.06466523520893 },
            { 22.651868529042016, 114.06605998390202 },
            { 22.656304210485004, 114.06605998390202 } 
        };
        static void Main(string[] args)
        {
            var d1= Distance.GetPointToLineDistance(29.6904621,106.4867361,29.6908013, 106.4865471,29.690616 , 106.486689 );
          var d2 = Distance.GetPointToLineDistance2(29.6904621,106.4867361,29.6908013, 106.4865471,29.690616, 106.486689);

            var area = Distance.GetArea(coords);
            var d3 = Distance.DistanceUseGeoLib(29.690616, 106.486689, 29.6906303, 106.4866506);
            Console.ReadLine();
        }
    }
}
