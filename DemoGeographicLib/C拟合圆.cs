using MathNet.Numerics.LinearAlgebra.Double;


namespace DemoGeographicLib;
internal class C拟合圆
{

    public static void Do()
    {

        var count = 50;
        var step = 2 * Math.PI / 100;
        var rd = new Random();
        //参照圆
        var x0 = 204.1;
        var y0 = 213.1;
        var r0 = 98.4;
        //噪音绝对差
        var diff = (int)(r0 * 0.1);
        var x = new double[count];
        var y = new double[count];
        //输出点集
        for (int i = 0; i < count; i++)
        {
            //circle
            x[i] = x0 + Math.Cos(i * step) * r0;
            y[i] = y0 + Math.Sin(i * step) * r0;
            //noise
            x[i] += Math.Cos(rd.Next() % 2 * Math.PI) * rd.Next(diff);
            y[i] += Math.Cos(rd.Next() % 2 * Math.PI) * rd.Next(diff);
        }

        var o = new C拟合圆();
        var result1= o.LinearAlgebraFit(x, y);
        var result2= o.LeastSquaresFit(x, y);
        Console.WriteLine(result1);
        Console.WriteLine(result2);
        Console.Read();
    }
    // 定义点的数据结构
    public class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    public class Circle
    {
        /// <summary>
        /// 圆心横坐标
        /// </summary>
        /// <value></value>
        public double X { get; set; }
        /// <summary>
        /// 圆心纵坐标
        /// </summary>
        /// <value></value>
        public double Y { get; set; }
        /// <summary>
        /// 圆半径
        /// </summary>
        /// <value></value>
        public double R { get; set; }

        public override string ToString()
        {
            return $"({X},{X}) R={R}";
        }
    }

    /// <summary>
    /// 拟合圆
    /// </summary>
    /// <param name="X">圆上的坐标x</param>
    /// <param name="Y">圆上的坐标y</param>
    /// <returns></returns>
    public Circle LeastSquaresFit(double[] X, double[] Y)
    {
        if (X.Length < 3)
        {
            return null;
        }
        double cent_x = 0.0,
            cent_y = 0.0,
            radius = 0.0;
        double sum_x = 0.0f, sum_y = 0.0f;
        double sum_x2 = 0.0f, sum_y2 = 0.0f;
        double sum_x3 = 0.0f, sum_y3 = 0.0f;
        double sum_xy = 0.0f, sum_x1y2 = 0.0f, sum_x2y1 = 0.0f;
        int N = X.Length;
        double x, y, x2, y2;
        for (int i = 0; i < N; i++)
        {
            x = X[i];
            y = Y[i];
            x2 = x * x;
            y2 = y * y;
            sum_x += x;
            sum_y += y;
            sum_x2 += x2;
            sum_y2 += y2;
            sum_x3 += x2 * x;
            sum_y3 += y2 * y;
            sum_xy += x * y;
            sum_x1y2 += x * y2;
            sum_x2y1 += x2 * y;
        }
        double C, D, E, G, H;
        double a, b, c;
        C = N * sum_x2 - sum_x * sum_x;
        D = N * sum_xy - sum_x * sum_y;
        E = N * sum_x3 + N * sum_x1y2 - (sum_x2 + sum_y2) * sum_x;
        G = N * sum_y2 - sum_y * sum_y;
        H = N * sum_x2y1 + N * sum_y3 - (sum_x2 + sum_y2) * sum_y;
        a = (H * D - E * G) / (C * G - D * D);
        b = (H * C - E * D) / (D * D - G * C);
        c = -(a * sum_x + b * sum_y + sum_x2 + sum_y2) / N;
        cent_x = a / (-2);
        cent_y = b / (-2);
        radius = Math.Sqrt(a * a + b * b - 4 * c) / 2;
        var result = new Circle();
        result.X = cent_x;
        result.Y = cent_y;
        result.R = radius;
        return result;
    }


    /// <summary>
    /// 线性代数求解 <br/>
    /// 因为(x-c1)^2 + (y-c2)^2= r^2
    /// 所在 x^2+c1^2-2x*c1 + y^2+c2^2-2y*c2= r^2
    /// 整理得 x^2 + y^2= r^2+2x*c1 + 2y*c2 -c1^2-c2^2
    /// 继续 x^2 + y^2 = 2x*c1 + 2y*c2 + (r^2 - c1^2 -c2^2)
    /// 如果把 r^2-c1^2-c2^2看作c3
    /// 即可整理得  2x*c1+2y*c2 + c3 = x^2+y^2
    /// 把x,y数值代入, 就可以看作是求解线性方程c1,c2,c3
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <returns></returns>
    public Circle LinearAlgebraFit(double[] X, double[] Y)
    {
        if (X.Length < 3)
        {
            return null;
        }
        var count = X.Length;
        var a = new double[count, 3];
        var c = new double[count, 1];
        for (int i = 0; i < count; i++)
        {
            //matrix
            a[i, 0] = 2 * X[i];  //对应整理后的代数式2x*c1
            a[i, 1] = 2 * Y[i];  //对应整理后的代数式2y*c2
            a[i, 2] = 1;         //对应整理后的代数式c3
            c[i, 0] = X[i] * X[i] + Y[i] * Y[i];  //对应整理后的代数式右边, x^2+y^2
        }
        var A = DenseMatrix.OfArray(a);
        var C = DenseMatrix.OfArray(c);
        //A*B=C
        var B = A.Solve(C);
        double c1 = B.At(0, 0),
            c2 = B.At(1, 0),
            r = Math.Sqrt(B.At(2, 0) + c1 * c1 + c2 * c2);  //c3= r^2-c1^2-c2^2
        var result = new Circle
        {
            X = c1,
            Y = c2,
            R = r
        };
        return result;
    }


}
