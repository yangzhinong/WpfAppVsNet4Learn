using MathNet.Numerics.LinearAlgebra;

namespace DemoGeographicLib;
internal class C解方程
{
    public static void Do()
    {
        //5 * x + 2 * y - 4 * z = -7

        //3 * x - 7 * y + 6 * z = 38

        //4 * x + 1 * y + 5 * z = 43          

        //创建方程向量的集合
        var vector1 = CreateVector.Dense(new double[] { 5.00, 2.00, -4.00 });
        var vector2 = CreateVector.Dense(new double[] { 3.00, -7.00, 6.00 });
        var vector3 = CreateVector.Dense(new double[] { 4.00, 1.00, 5.00 });
        var vectors = new List<Vector<double>> { vector1, vector2, vector3 };
        //方程等号左侧的已知数矩阵
        var matrixA = CreateMatrix.DenseOfRowVectors(vectors);
        foreach (var rows in matrixA.EnumerateRows())
        {
            Console.WriteLine(string.Join(" , ", rows));
        }
        //方程等号右侧的已知数向量
        var vectorB = CreateVector.Dense(new[] { -7.0, 38.0, 43.0 });
        //求解
        var resultX = matrixA.Solve(vectorB);

        

        Console.WriteLine("结果：");
        resultX.ToList().ForEach(p => Console.WriteLine(Math.Round(p, 2)));
        Console.Read();
    }
}
