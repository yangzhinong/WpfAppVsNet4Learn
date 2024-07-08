using System;
using System.Collections.Generic;

namespace DemoConsole
{
    class MinimizeAdjacentDifference
    {
        static List<List<int>> MinimizeDifference(List<int> sequence, int numGroups)
        {
            int n = sequence.Count;
            int[,] dp = new int[n + 1, numGroups + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= numGroups; j++)
                {
                    dp[i, j] = int.MaxValue;
                }
            }

            dp[0, 0] = 0;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= numGroups; j++)
                {
                    for (int k = 0; k < i; k++)
                    {
                        int currentDifference = CalculateDifference(sequence, k, i - 1);
                        dp[i, j] = Math.Min(dp[i, j], dp[k, j - 1] + currentDifference);
                    }
                }
            }

            // 构造分组结果
            List<List<int>> result = new List<List<int>>();
            int endIndex = n;
            for (int j = numGroups; j > 0; j--)
            {
                int startIndex = 0;
                while (startIndex < endIndex)
                {
                    int minDifference = int.MaxValue;
                    int minIndex = -1;

                    for (int k = startIndex + 1; k <= endIndex; k++)
                    {
                        int currentDifference = CalculateDifference(sequence, startIndex, k - 1);
                        int totalDifference = dp[k, j - 1] + currentDifference;

                        if (totalDifference < minDifference)
                        {
                            minDifference = totalDifference;
                            minIndex = k;
                        }
                    }

                    result.Add(sequence.GetRange(startIndex, minIndex - startIndex));
                    startIndex = minIndex;
                }

                endIndex = startIndex;
            }

            return result;
        }

        static int CalculateDifference(List<int> sequence, int start, int end)
        {
            int difference = 0;
            for (int i = start + 1; i <= end; i++)
            {
                difference += Math.Abs(sequence[i] - sequence[i - 1]);
            }
            return difference;
        }

        public static void Do()
        {
             List<int> inputData = new List<int> { 28, 116, 117, 109, 118, 119, 29, 110, 120, 121, 30, 111, 122, 123, 31, 124, 125, 112, 32, 126 };
            //List<int> inputData = new List<int> { 1, 2, 6, 9, 10, 11, 12 };
            int numGroups = 3;

            List<List<int>> result = MinimizeDifference(inputData, numGroups);

            // 打印分组结果
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine($"Group {i + 1}: [{string.Join(", ", result[i])}]");
            }
        }
    }
}
