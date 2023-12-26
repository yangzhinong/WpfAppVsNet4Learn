using MathNet.Numerics.LinearAlgebra;

namespace DemoGeographicLib;

class KalmanFilter
{
    // 状态向量 x，包含系统的状态信息
    private Matrix<double> x;

    // 状态协方差矩阵 P，表示状态估计的不确定性
    private Matrix<double> P;

    // 系统的动态模型矩阵 A
    private Matrix<double> A;

    // 系统的动态模型噪声协方差矩阵 Q
    private Matrix<double> Q;

    // 观测模型矩阵 H
    private Matrix<double> H;

    // 观测模型噪声协方差矩阵 R
    private Matrix<double> R;

    public KalmanFilter(Matrix<double> initialX, Matrix<double> initialP, Matrix<double> A, Matrix<double> Q, Matrix<double> H, Matrix<double> R)
    {
        this.x = initialX.Clone();
        this.P = initialP.Clone();
        this.A = A.Clone();
        this.Q = Q.Clone();
        this.H = H.Clone();
        this.R = R.Clone();
    }

    public Matrix<double> Predict()
    {
        // 预测步骤
        x = A * x;
        P = A * P * A.Transpose() + Q;
        return x;
    }

    public Matrix<double> Update(Matrix<double> measurement)
    {
        // 更新步骤
        var K = P * H.Transpose() * (H * P * H.Transpose() + R).Inverse();
        x = x + K * (measurement - H * x);
        P = (Matrix<double>.Build.DenseIdentity(x.RowCount) - K * H) * P;

        return x;
    }
}

