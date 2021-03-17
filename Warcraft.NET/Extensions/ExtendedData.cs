using SharpDX;
using System.Collections.Generic;

namespace Warcraft.NET.Extensions
{
    /// <summary>
    /// Extension methods used internally in the library for transforming data.
    /// </summary>
    public static class ExtendedData
    {
        /// <summary>
        /// Converts a short-packed quaternion value to a normal float.
        /// </summary>
        /// <param name="inShort">The short.</param>
        /// <returns>The unpacked float.</returns>
        public static float ShortQuatValueToFloat(short inShort)
        {
            return inShort / (float)short.MaxValue;
        }

        /// <summary>
        /// Converts a floating-point value to a packed short.
        /// </summary>
        /// <param name="inFloat">The float.</param>
        /// <returns>The packed short.</returns>
        public static short FloatQuatValueToShort(float inFloat)
        {
            return (short)((inFloat + 1.0f) * short.MaxValue);
        }

        /// <summary>
        /// Deconstructs a key-value pair into a tuple.
        /// </summary>
        /// <param name="keyValuePair">The key-value pair.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <typeparam name="T1">The type of the key.</typeparam>
        /// <typeparam name="T2">The type of the value.</typeparam>
        public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> keyValuePair, out T1 key, out T2 value)
        {
            key = keyValuePair.Key;
            value = keyValuePair.Value;
        }

        public static float Volume(this BoundingBox boundingBox)
        {
            return boundingBox.Height * boundingBox.Width * boundingBox.Depth;
        }

        public static BoundingBox Multiply(this BoundingBox boundingBox, float scale)
        {
            boundingBox.Maximum = Vector3.Multiply(boundingBox.Maximum, scale);
            boundingBox.Minimum = Vector3.Multiply(boundingBox.Minimum, scale);

            return boundingBox;
        }

        public static BoundingBox Transform(this BoundingBox bboundingBoxox, ref Matrix matrix)
        {
            var corners = new Vector3[8];
            bboundingBoxox.GetCorners(corners);
            var newMin = new Vector3(float.MaxValue);
            var newMax = new Vector3(float.MinValue);

            for (var i = 0; i < corners.Length; ++i)
            {
                Vector3 v;
                Vector3.TransformCoordinate(ref corners[i], ref matrix, out v);
                TakeMin(ref newMin, ref v);
                TakeMax(ref newMax, ref v);
            }

            return new BoundingBox(newMin, newMax);
        }

        public static void TakeMin(ref Vector3 v, ref Vector3 other)
        {
            if (v.X > other.X)
                v.X = other.X;
            if (v.Y > other.Y)
                v.Y = other.Y;
            if (v.Z > other.Z)
                v.Z = other.Z;
        }

        public static void TakeMax(ref Vector3 v, ref Vector3 other)
        {
            if (v.X < other.X)
                v.X = other.X;
            if (v.Y < other.Y)
                v.Y = other.Y;
            if (v.Z < other.Z)
                v.Z = other.Z;
        }
    }
}
