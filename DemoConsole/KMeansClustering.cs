using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoConsole
{
    public class KMeansClustering
    {
        static List<List<double>> KMeans(List<double> data, int k, int maxIterations = 100)
        {
            Random random = new Random();
            List<double> centroids = new List<double>();

            // 随机选择k个初始聚类中心
            for (int i = 0; i < k; i++)
            {
                centroids.Add(data[random.Next(data.Count)]);
            }

            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                // 分配数据点到最近的聚类中心
                List<List<double>> clusters = new List<List<double>>(k);
                for (int i = 0; i < k; i++)
                {
                    clusters.Add(new List<double>());
                }

                foreach (double point in data)
                {
                    int closestCentroidIndex = FindClosestCentroid(point, centroids);
                    clusters[closestCentroidIndex].Add(point);
                }

                // 更新聚类中心
                for (int i = 0; i < k; i++)
                {
                    if (clusters[i].Count > 0)
                    {
                        centroids[i] = clusters[i].Average();
                    }
                }
            }

            return centroids.Select(centroid => new List<double> { centroid }).ToList();
        }

        static int FindClosestCentroid(double point, List<double> centroids)
        {
            int closestIndex = 0;
            double minDistance = Math.Abs(point - centroids[0]);

            for (int i = 1; i < centroids.Count; i++)
            {
                double distance = Math.Abs(point - centroids[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        public static void Do()
        {
            List<double> inputData = new List<double> { 28, 116, 117, 109, 118, 119, 29, 110, 120, 121, 30, 111, 122, 123, 31, 124, 125, 112, 32, 126 };
            int k = 2;

            List<List<double>> clusters = KMeans(inputData, k);

            // 打印聚类结果
            for (int i = 0; i < clusters.Count; i++)
            {
                Console.WriteLine($"Cluster {i + 1}: [{string.Join(", ", clusters[i])}]");
            }
        }
    }
}
