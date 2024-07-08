using System;
using System.Collections.Generic;

namespace DemoConsole
{
    internal class DataGroup
    {
        static List<List<int>> SeparateGroups(int[] data)
        {
            List<List<int>> result = new List<List<int>>();
            List<int> currentGroup = new List<int>();

            for (int i = 0; i < data.Length; i++)
            {
                // 当前组为空或当前数字递增时，添加到当前组
                if (currentGroup.Count == 0 || data[i] > currentGroup[currentGroup.Count - 1])
                {
                    currentGroup.Add(data[i]);
                }
                else
                {
                    // 当前数字不再递增，表示当前组结束，开始新的组
                    result.Add(currentGroup);
                    currentGroup = new List<int> { data[i] };
                }
            }

            // 添加最后一组
            if (currentGroup.Count > 0)
            {
                result.Add(currentGroup);
            }

            return result;

        }

        public static void Do()
        {
            int[] inputData = { 28, 116, 117, 109, 118, 119, 29, 110, 120, 121, 30, 111, 122, 123, 31, 124, 125, 112, 32, 126 };

            List<List<int>> groups = SeparateGroups(inputData);

            // 打印分组结果
            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine($"Group {i + 1}: [{string.Join(", ", groups[i])}]");
            }
        }
    }
}
