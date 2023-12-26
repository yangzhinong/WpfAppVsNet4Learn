namespace DemoGeographicLib;
internal class C中值滤波实现
{
    public static void Do()
    {
        // 示例数据
        double[] noisySignal = { 1, 2, 3, 10, 5, 6, 7, 8, 9, 4 };

        // 中值滤波窗口大小
        int windowSize = 3;

        // 中值滤波处理
        double[] filteredSignal = MedianFilter(noisySignal, windowSize);

        // 打印结果
        Console.WriteLine("原始信号: " + string.Join(", ", noisySignal));
        Console.WriteLine("中值滤波后的信号: " + string.Join(", ", filteredSignal));
        Console.Read();
    }

    // 中值滤波的实现
    static double[] MedianFilter(double[] signal, int windowSize)
    {
        int halfWindowSize = windowSize / 2;
        double[] result = new double[signal.Length];

        for (int i = 0; i < signal.Length; i++)
        {
            double[] window = new double[windowSize];
            for (int j = 0; j < windowSize; j++)
            {
                int index = i - halfWindowSize + j;
                if (index < 0)
                {
                    index = 0;
                }
                else if (index >= signal.Length)
                {
                    index = signal.Length - 1;
                }

                window[j] = signal[index];
            }

            Array.Sort(window);
            result[i] = window[halfWindowSize];
        }

        return result;
    }
}
