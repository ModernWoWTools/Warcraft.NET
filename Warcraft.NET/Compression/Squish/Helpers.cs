using System;
using System.Numerics;

namespace Warcraft.NET.Compression.Squish
{
    public static class Helpers
    {
        public static Vector4 MultiplyAdd(Vector4 a, Vector4 b, Vector4 c)
        {
            return a * b + c;
        }

        public static Vector4 NegativeMultiplySubtract(Vector4 a, Vector4 b, Vector4 c)
        {
            return c - a * b;
        }

        public static Vector4 Reciprocal(Vector4 v)
        {
            return new Vector4(
                    1.0f / v.X,
                    1.0f / v.Y,
                    1.0f / v.Z,
                    1.0f / v.W
            );
        }

        public static Vector4 Truncate(Vector4 v)
        {
            return new Vector4(
                (float)(v.X > 0.0f ? Math.Floor(v.X) : Math.Ceiling(v.X)),
                (float)(v.Y > 0.0f ? Math.Floor(v.Y) : Math.Ceiling(v.Y)),
                (float)(v.Z > 0.0f ? Math.Floor(v.Z) : Math.Ceiling(v.Z)),
                (float)(v.W > 0.0f ? Math.Floor(v.W) : Math.Ceiling(v.W))
            );
        }

        public static Vector3 Truncate(Vector3 v)
        {
            return new Vector3(
                (float)(v.X > 0.0f ? Math.Floor(v.X) : Math.Ceiling(v.X)),
                (float)(v.Y > 0.0f ? Math.Floor(v.Y) : Math.Ceiling(v.Y)),
                (float)(v.Z > 0.0f ? Math.Floor(v.Z) : Math.Ceiling(v.Z))
            );
        }

        public static bool CompareAnyLessThan(Vector4 left, Vector4 right)
        {
            return left.X < right.X ||
                    left.Y < right.Y ||
                    left.Z < right.Z ||
                    left.W < right.W;
        }

        public static Vector4 SplatX(this Vector4 v) { return new Vector4(v.X); }

        public static Vector4 SplatY(this Vector4 v) { return new Vector4(v.Y); }

        public static Vector4 SplatZ(this Vector4 v) { return new Vector4(v.Z); }

        public static Vector4 SplatW(this Vector4 v) { return new Vector4(v.W); }


        public static Vector3 ToVector3(this Vector4 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }
    }
}
