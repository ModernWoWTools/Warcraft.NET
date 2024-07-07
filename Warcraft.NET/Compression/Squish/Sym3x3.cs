using System.Numerics;

namespace Warcraft.NET.Compression.Squish
{
    public class Sym3x3
    {
        private float[] X { get; set; } = new float[6];

        public float this[int i]
        {
            get => X[i];
            set => X[i] = value;
        }

        public Sym3x3(float s)
        {
            for (int i = 0; i < 6; i++) { X[i] = s; }
        }

        public static Sym3x3 ComputeWeightedCovariance(int n, Vector3[] points, float[] weights)
        {
            // compute the centroid
            float total = 0.0f;
            Vector3 centroid = new Vector3(0.0f);

            for (int i = 0; i < n; ++i)
            {
                total += weights[i];
                centroid += weights[i] * points[i];
            }

            if (total > float.Epsilon) { centroid /= total; }

            // accumulate the covariance matrix
            Sym3x3 covariance = new Sym3x3(0.0f);

            for (int i = 0; i < n; ++i)
            {
                Vector3 a = points[i] - centroid;
                Vector3 b = weights[i] * a;

                covariance[0] += a.X * b.X;
                covariance[1] += a.X * b.Y;
                covariance[2] += a.X * b.Z;
                covariance[3] += a.Y * b.Y;
                covariance[4] += a.Y * b.Z;
                covariance[5] += a.Z * b.Z;
            }

            // return it
            return covariance;
        }

        public static Vector3 ComputePrincipleComponent(Sym3x3 matrix)
        {
            Vector4 row0 = new Vector4(matrix[0], matrix[1], matrix[2], 0.0f);
            Vector4 row1 = new Vector4(matrix[1], matrix[3], matrix[4], 0.0f);
            Vector4 row2 = new Vector4(matrix[2], matrix[4], matrix[5], 0.0f);
            Vector4 v = new Vector4(1.0f);

            for (int i = 0; i < 8; ++i)
            {
                // matrix multiply
                Vector4 w = row0 * v.SplatX();
                w = Helpers.MultiplyAdd(row1, v.SplatY(), w);
                w = Helpers.MultiplyAdd(row2, v.SplatZ(), w);

                // get max component from xyz in all channels
                Vector4 a = Vector4.Max(w.SplatX(), Vector4.Max(w.SplatY(), w.SplatZ()));

                // divide through and advance
                v = w * Helpers.Reciprocal(a);
            }

            return v.ToVector3();
        }
    }
}
