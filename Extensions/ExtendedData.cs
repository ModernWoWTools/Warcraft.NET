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
    }
}
