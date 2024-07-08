using System;
using System.Collections.Generic;

namespace DemoConsole
{
    internal class LongestIncreasingSubsequence
    {
        static List<int> FindLIS(List<int> sequence)
        {
            int n = sequence.Count;
            int[] lisLength = new int[n];
            int[] previousIndex = new int[n];

            for (int i = 0; i < n; i++)
            {
                lisLength[i] = 1;
                previousIndex[i] = -1;

                for (int j = 0; j < i; j++)
                {
                    if (sequence[i] > sequence[j] && lisLength[i] < lisLength[j] + 1)
                    {
                        lisLength[i] = lisLength[j] + 1;
                        previousIndex[i] = j;
                    }
                }
            }

            // 寻找最长递增子序列的长度和结束索引
            int maxLength = 0;
            int endIndex = 0;
            for (int i = 0; i < n; i++)
            {
                if (lisLength[i] > maxLength)
                {
                    maxLength = lisLength[i];
                    endIndex = i;
                }
            }

            // 构造最长递增子序列
            List<int> lis = new List<int>();
            while (endIndex != -1)
            {
                lis.Insert(0, sequence[endIndex]);
                endIndex = previousIndex[endIndex];
            }

            return lis;
        }

        public static void Do()
        {
            List<int> inputData = new List<int> { 28, 116, 117, 109, 118, 119, 29, 110, 120, 121, 30, 111, 122, 123, 31, 124, 125, 112, 32, 126 };
            List<int> lis = FindLIS(inputData);

            Console.WriteLine("Longest Increasing Subsequence: [" + string.Join(", ", lis) + "]");
        }
    }
}
