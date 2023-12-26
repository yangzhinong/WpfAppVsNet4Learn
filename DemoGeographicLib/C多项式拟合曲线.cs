using MathNet.Numerics;

namespace DemoGeographicLib;
internal class C多项式拟合曲线
{
    public static void Do()
    {
        // 示例数据
        double[] xValues = { 1, 2, 3, 4, 5 };
        double[] yValues = { 2.5, 3.7, 4.2, 5.0, 5.8 };

        // 使用 MathNet.Numerics.Fitting.Fit.Polynomial 进行多项式拟合
        int degree = 2; // 多项式的次数
        var polynomial = Fit.Polynomial(xValues, yValues, degree);
        
        

        // 打印拟合的多项式系数
        Console.WriteLine("拟合的多项式系数:");
        for (int i = 0; i <= degree; i++)
        {
            Console.WriteLine($"Coeff[{i}] = {polynomial[i]}");
        }

        // 打印拟合的多项式表达式
        Console.WriteLine($"拟合的多项式表达式: {GetPolynomialExpression(polynomial)}");
        Console.Read();
    }

    // 辅助方法：获取多项式表达式字符串
    static string GetPolynomialExpression(double[] coefficients)
    {
        int degree = coefficients.Length - 1;
        string expression = $"";

        for (int i = degree; i >= 0; i--)
        {
            if (coefficients[i] != 0)
            {
                string term = (i > 0) ? $" + {coefficients[i]}x^{i}" : $" + {coefficients[i]}";
                expression += term;
            }
        }

        return expression;
    }
}
