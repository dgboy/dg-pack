using UnityEngine;

public class Matrix3x3 {
    private readonly double[,] m_val = new double[3, 3];

    public double[,] Data => m_val;

    public double[] GetValues() {
        var k = 3;
        var data = new double[Data.Length - 1];
        for (int i = 0; i < Data.Length / k; i++) {
            data[i * k] = Data[i, 0];
            data[i * k + 1] = Data[i, 1];
            if (i + 1 < k) data[i * k + 2] = Data[i, 2];
        }

        return data;
    }

    public Matrix3x3() {
    }

    public Matrix3x3(Vector2 p1, Vector2 p2, Vector2 p3) {
        m_val = new double[3, 3];
        m_val[0, 0] = p1.x;
        m_val[0, 1] = p2.x;
        m_val[0, 2] = p3.x;

        m_val[1, 0] = p1.y;
        m_val[1, 1] = p2.y;
        m_val[1, 2] = p3.y;

        m_val[2, 0] = 1.0;
        m_val[2, 1] = 1.0;
        m_val[2, 2] = 1.0;
    }

    public double Determinant() {
        double result = 0;
        result =
            m_val[0, 0] *
            (m_val[1, 1] * m_val[2, 2] - m_val[1, 2] * m_val[2, 1]);
        result -=
            m_val[0, 1] *
            (m_val[1, 0] * m_val[2, 2] - m_val[1, 2] * m_val[2, 0]);
        result +=
            m_val[0, 2] *
            (m_val[1, 0] * m_val[2, 1] - m_val[1, 1] * m_val[2, 0]);

        return result;
    }

    public double a(int i, int j) {
        return m_val[i - 1, j - 1];
    }

    public void Inverse() {
        double a11 = a(2, 2) * a(3, 3) - a(2, 3) * a(3, 2);
        double a12 = a(2, 1) * a(3, 3) - a(2, 3) * a(3, 1);
        double a13 = a(2, 1) * a(3, 2) - a(2, 2) * a(3, 1);

        double a21 = a(1, 2) * a(3, 3) - a(1, 3) * a(3, 2);
        double a22 = a(1, 1) * a(3, 3) - a(1, 3) * a(3, 1);
        double a23 = a(1, 1) * a(3, 2) - a(1, 2) * a(3, 1);

        double a31 = a(1, 2) * a(2, 3) - a(1, 3) * a(2, 2);
        double a32 = a(1, 1) * a(2, 3) - a(1, 3) * a(2, 1);
        double a33 = a(1, 1) * a(2, 2) - a(1, 2) * a(2, 1);

        double od = 1.0 / Determinant();

        m_val[0, 0] = od * a11;
        m_val[0, 1] = -od * a21;
        m_val[0, 2] = od * a31;

        m_val[1, 0] = -od * a12;
        m_val[1, 1] = od * a22;
        m_val[1, 2] = -od * a32;

        m_val[2, 0] = od * a13;
        m_val[2, 1] = -od * a23;
        m_val[2, 2] = od * a33;
        //Display("verse");
    }

    public void MultByVec(double[] vector) {
        for (int row = 0; row < 3; ++row) {
            m_val[row, 0] *= vector[0];
            m_val[row, 1] *= vector[1];
            m_val[row, 2] *= vector[2];
        }
    }

    public Vector2 Dehomogenize(Vector2 vector) {
        double x = a(1, 1) * vector.x + a(1, 2) * vector.y + a(1, 3) * 1.0;
        double y = a(2, 1) * vector.x + a(2, 2) * vector.y + a(2, 3) * 1.0;
        double z = a(3, 1) * vector.x + a(3, 2) * vector.y + a(3, 3) * 1.0;

        var result = new Vector2((float)(x / z), (float)(y / z));
        return result;
    }

    public Matrix3x3 MultMat(Matrix3x3 B) {
        Matrix3x3 result = new Matrix3x3();

        result.m_val[0, 0] =
            a(1, 1) * B.a(1, 1) + a(1, 2) * B.a(2, 1) + a(1, 3) * B.a(3, 1);
        result.m_val[0, 1] =
            a(1, 1) * B.a(1, 2) + a(1, 2) * B.a(2, 2) + a(1, 3) * B.a(3, 2);
        result.m_val[0, 2] =
            a(1, 1) * B.a(1, 3) + a(1, 2) * B.a(2, 3) + a(1, 3) * B.a(3, 3);

        result.m_val[1, 0] =
            a(2, 1) * B.a(1, 1) + a(2, 2) * B.a(2, 1) + a(2, 3) * B.a(3, 1);
        result.m_val[1, 1] =
            a(2, 1) * B.a(1, 2) + a(2, 2) * B.a(2, 2) + a(2, 3) * B.a(3, 2);
        result.m_val[1, 2] =
            a(2, 1) * B.a(1, 3) + a(2, 2) * B.a(2, 3) + a(2, 3) * B.a(3, 3);

        result.m_val[2, 0] =
            a(3, 1) * B.a(1, 1) + a(3, 2) * B.a(2, 1) + a(3, 3) * B.a(3, 1);
        result.m_val[2, 1] =
            a(3, 1) * B.a(1, 2) + a(3, 2) * B.a(2, 2) + a(3, 3) * B.a(3, 2);
        result.m_val[2, 2] =
            a(3, 1) * B.a(1, 3) + a(3, 2) * B.a(2, 3) + a(3, 3) * B.a(3, 3);

        return result;
    }

    public void Display(string prefix) {
        string msg = $"[{prefix}] ";
        for (int i = 0; i < m_val.GetLength(0); i++) {
            for (int j = 0; j < m_val.GetLength(1); j++) {
                msg += $"{m_val[i, j]:f2}; ";
            }
        }

        Debug.Log(msg);
    }

    public static Matrix3x3 Homogenous(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4) {
        Matrix3x3 Major = new Matrix3x3(p1, p2, p3);
        Matrix3x3 Minor1 = new Matrix3x3(p4, p2, p3);
        Matrix3x3 Minor2 = new Matrix3x3(p1, p4, p3);
        Matrix3x3 Minor3 = new Matrix3x3(p1, p2, p4);
        double MajorD = Major.Determinant();
        double Minor1D = Minor1.Determinant();
        double Minor2D = Minor2.Determinant();
        double Minor3D = Minor3.Determinant();

        double[] coeff = new double[3];
        coeff[0] = Minor1D / MajorD;
        coeff[1] = Minor2D / MajorD;
        coeff[2] = Minor3D / MajorD;
        Major.MultByVec(coeff);

        //Major.Display("init");
        return Major;
    }
    public static Matrix3x3 Homogenous(Vector2[] ps) {
        var Major  = new Matrix3x3(ps[0], ps[1], ps[2]);
        var Minor1 = new Matrix3x3(ps[3], ps[1], ps[2]);
        var Minor2 = new Matrix3x3(ps[0], ps[3], ps[2]);
        var Minor3 = new Matrix3x3(ps[0], ps[1], ps[3]);
        double MajorD = Major.Determinant();
        double Minor1D = Minor1.Determinant();
        double Minor2D = Minor2.Determinant();
        double Minor3D = Minor3.Determinant();

        double[] coeff = new double[3];
        coeff[0] = Minor1D / MajorD;
        coeff[1] = Minor2D / MajorD;
        coeff[2] = Minor3D / MajorD;
        Major.MultByVec(coeff);

        //Major.Display("init");
        return Major;
    }
}
