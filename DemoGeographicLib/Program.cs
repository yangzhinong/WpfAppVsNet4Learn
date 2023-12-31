﻿namespace DemoGeographicLib;

internal class Program
{
    static void Main(string[] args)
    {
        C拟合圆.Do();
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


