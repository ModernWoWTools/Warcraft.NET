using System;

namespace Warcraft.NET.Extensions
{
    /// <summary>
    /// Extension methods used internally in the library for mathematical computations.
    /// </summary>
    public static class ExtendedMath
    {
        /// <summary>
        /// Clamps a value between a minimum and maximum point.
        /// </summary>
        /// <param name="val">The value to compare.</param>
        /// <param name="min">The minimum allowed value.</param>
        /// <param name="max">The maximum allowed value.</param>
        /// <typeparam name="T">The type of value to compare.</typeparam>
        /// <returns>The value, if it falls into the range, otherwise the upper or lower bound, whichever was closest. </returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }
            else if (val.CompareTo(max) > 0)
            {
                return max;
            }
            else
            {
                return val;
            }
        }

        /// <summary>
        /// Maps a value in a specified range to a new value in a new range.
        /// Taken from the Arduino reference (https://www.arduino.cc/en/Reference/Map).
        /// </summary>
        /// <param name="val">The input value.</param>
        /// <param name="inMin">The original mininum value.</param>
        /// <param name="inMax">The original maximum value.</param>
        /// <param name="outMin">The new minimum value.</param>
        /// <param name="outMax">The new maxiumum value.</param>
        /// <returns>The value, mapped to the new range.</returns>
        public static int Map(int val, int inMin, int inMax, int outMin, int outMax)
        {
            return ((val - inMin) * (outMax - outMin) / (inMax - inMin)) + outMin;
        }
    }
}
