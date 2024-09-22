using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Warcraft.NET.Utils;

namespace Warcraft.NET.Types
{
    /// <summary>
    /// A half precision (16 bit) floating point value.
    /// Code copy from https://github.com/sharpdx/SharpDX/blob/master/Source/SharpDX.Mathematics/Half.cs by SharpDX
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HalfFloat
    {
        private ushort value;

        /// <summary>
        /// Number of decimal digits of precision.
        /// </summary>
        public const int PrecisionDigits = 3;

        /// <summary>
        /// Number of bits in the mantissa.
        /// </summary>
        public const int MantissaBits = 11;

        /// <summary>
        /// Maximum decimal exponent.
        /// </summary>
        public const int MaximumDecimalExponent = 4;

        /// <summary>
        /// Maximum binary exponent.
        /// </summary>
        public const int MaximumBinaryExponent = 15;

        /// <summary>
        /// Minimum decimal exponent.
        /// </summary>
        public const int MinimumDecimalExponent = -4;

        /// <summary>
        /// Minimum binary exponent.
        /// </summary>
        public const int MinimumBinaryExponent = -14;

        /// <summary>
        /// Exponent radix.
        /// </summary>
        public const int ExponentRadix = 2;

        /// <summary>
        /// Additional rounding.
        /// </summary>
        public const int AdditionRounding = 1;

        /// <summary>
        /// Smallest such that 1.0 + epsilon != 1.0
        /// </summary>
        public static readonly float Epsilon;

        /// <summary>
        /// Maximum value of the number.
        /// </summary>
        public static readonly float MaxValue;

        /// <summary>
        /// Minimum value of the number.
        /// </summary>
        public static readonly float MinValue;

        /// <summary>
        /// Initializes a new instance of the <see cref = "HalfFloat" /> structure.
        /// </summary>
        /// <param name = "value">The floating point value that should be stored in 16 bit format.</param>
        public HalfFloat(float value)
        {
            this.value = HalfUtils.Pack(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "HalfFloat" /> structure.
        /// </summary>
        /// <param name = "rawvalue">The floating point value that should be stored in 16 bit format.</param>
        public HalfFloat(ushort rawvalue)
        {
            this.value = rawvalue;
        }

        /// <summary>
        /// Gets or sets the raw 16 bit value used to back this half-float.
        /// </summary>
        public ushort RawValue
        {
            get { return value; }
            set { this.value = value; }
        }

        /// <summary>
        /// Converts an array of half precision values into full precision values.
        /// </summary>
        /// <param name = "values">The values to be converted.</param>
        /// <returns>An array of converted values.</returns>
        public static float[] ConvertToFloat(HalfFloat[] values)
        {
            float[] results = new float[values.Length];
            for (int i = 0; i < results.Length; i++)
                results[i] = HalfUtils.Unpack(values[i].RawValue);
            return results;
        }

        /// <summary>
        /// Converts an array of full precision values into half precision values.
        /// </summary>
        /// <param name = "values">The values to be converted.</param>
        /// <returns>An array of converted values.</returns>
        public static HalfFloat[] ConvertToHalf(float[] values)
        {
            HalfFloat[] results = new HalfFloat[values.Length];
            for (int i = 0; i < results.Length; i++)
                results[i] = new HalfFloat(values[i]);
            return results;
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref = "Single" /> to <see cref = "HalfFloat" />.
        /// </summary>
        /// <param name = "value">The value to be converted.</param>
        /// <returns>The converted value.</returns>
        public static implicit operator HalfFloat(float value)
        {
            return new HalfFloat(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref = "HalfFloat" /> to <see cref = "Single" />.
        /// </summary>
        /// <param name = "value">The value to be converted.</param>
        /// <returns>The converted value.</returns>
        public static implicit operator float(HalfFloat value)
        {
            return HalfUtils.Unpack(value.value);
        }

        /// <summary>
        /// Tests for equality between two objects.
        /// </summary>
        /// <param name = "left">The first value to compare.</param>
        /// <param name = "right">The second value to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name = "left" /> has the same value as <paramref name = "right" />; otherwise, <c>false</c>.</returns>
        public static bool operator ==(HalfFloat left, HalfFloat right)
        {
            return left.value == right.value;
        }

        /// <summary>
        /// Tests for inequality between two objects.
        /// </summary>
        /// <param name = "left">The first value to compare.</param>
        /// <param name = "right">The second value to compare.</param>
        /// <returns>
        /// <c>true</c> if <paramref name = "left" /> has a different value than <paramref name = "right" />; otherwise, <c>false</c>.</returns>
        public static bool operator !=(HalfFloat left, HalfFloat right)
        {
            return left.value != right.value;
        }

        /// <summary>
        /// Converts the value of the object to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            float num = this;
            return num.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            ushort num = value;
            return (((num * 3) / 2) ^ num);
        }

        /// <summary>
        /// Determines whether the specified object instances are considered equal.
        /// </summary>
        /// <param name = "value1" />
        /// <param name = "value2" />
        /// <returns>
        /// <c>true</c> if <paramref name = "value1" /> is the same instance as <paramref name = "value2" /> or 
        /// if both are <c>null</c> references or if <c>value1.Equals(value2)</c> returns <c>true</c>; otherwise, <c>false</c>.</returns>
        [MethodImpl((MethodImplOptions)0x100)] // MethodImplOptions.AggressiveInlining
        public static bool Equals(ref HalfFloat value1, ref HalfFloat value2)
        {
            return value1.value == value2.value;
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance is equal to the specified object.
        /// </summary>
        /// <param name = "other">Object to make the comparison with.</param>
        /// <returns>
        /// <c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        public bool Equals(HalfFloat other)
        {
            return other.value == value;
        }

        /// <summary>
        /// Returns a value that indicates whether the current instance is equal to a specified object.
        /// </summary>
        /// <param name = "obj">Object to make the comparison with.</param>
        /// <returns>
        /// <c>true</c> if the current instance is equal to the specified object; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is HalfFloat))
                return false;

            return Equals((HalfFloat)obj);
        }

        static HalfFloat()
        {
            Epsilon = 0.0004887581f;
            MaxValue = 65504f;
            MinValue = 6.103516E-05f;
        }
    }
}
