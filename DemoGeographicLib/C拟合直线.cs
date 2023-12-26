namespace DemoGeographicLib;
internal class C拟合直线
{
    public static void Do()
    {
        //其实可用用mathNet的  Fit.Line(xValues,yValues)拟合
        // y= mx+c
        // m = [avg(x*y)-avgX*avgY] / [avg(x^2)- (avgX)^2]
        // 输入数据，假设这是你的10个测量点
        double[] xValues = { 2, 6, 9, 13 };
        double[] yValues = { 4, 8, 12, 21 };

        double sumXY = 0;
        double sumX2 = 0;
        double sumX = 0;
        double sumY = 0;
        for (var i = 0; i < xValues.Length; i++)
        {
            sumX += xValues[i];
            sumXY += xValues[i] * yValues[i];
            sumX2 += xValues[i] * xValues[i];
            sumY += yValues[i];
        }

        double avgXY = sumXY / xValues.Length;
        double avgX2 = sumX2 / xValues.Length;
        double avgX = sumX / xValues.Length;
        double avgY = sumY / xValues.Length;

        //这就是最小2乘法求解公式
        double m = (avgXY - avgX * avgY) / (avgX2 - avgX * avgX);
        double c = avgY - m * avgX;

        Console.WriteLine($"y= mx + c  ==> y= {m}x + {c}");
        Console.Read();
    }
}
