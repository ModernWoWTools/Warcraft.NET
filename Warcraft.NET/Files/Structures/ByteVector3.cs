using System.Collections.Generic;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a 3D vector of bytes.
    /// </summary>
    public struct ByteVector3 : IFlattenableData<byte>
    {
        /// <summary>
        /// X coordinate of this vector.
        /// </summary>
        public byte X;

        /// <summary>
        /// Y coordinate of this vector.
        /// </summary>
        public byte Y;

        /// <summary>
        /// Z coordinate of this vector.
        /// </summary>
        public byte Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVector3"/> struct.
        /// </summary>
        /// <param name="inX">X coordinate.</param>
        /// <param name="inY">Y coordinate.</param>
        /// <param name="inZ">Z coordinate.</param>
        public ByteVector3(byte inX, byte inY, byte inZ)
        {
            X = inX;
            Y = inY;
            Z = inZ;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ByteVector3"/> struct.
        /// </summary>
        /// <param name="all">All.</param>
        public ByteVector3(byte all) : this(all, all, all)
        {
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="start">The start vector.</param>
        /// <param name="end">The end vector.</param>
        /// <returns>The dot product of the two vectors.</returns>
        public static byte Dot(ByteVector3 start, ByteVector3 end)
        {
            return (byte)((start.X * end.X) + (start.Y * end.Y) + (start.Z * end.Z));
        }

        /// <summary>
        /// Computes the cross product of two vectors, producing a new vector which
        /// is orthogonal to the two original vectors.
        /// </summary>
        /// <param name="start">The start vector.</param>
        /// <param name="end">The end vector.</param>
        /// <returns>The cross product of the two vectors.</returns>
        public static ByteVector3 Cross(ByteVector3 start, ByteVector3 end)
        {
            var x = (byte)((start.Y * end.Z) - (end.Y * start.Z));
            var y = (byte)(((start.X * end.Z) - (end.X * start.Z)) * -1);
            var z = (byte)((start.X * end.Y) - (end.X * start.Y));

            var rtnvector = new ByteVector3(x, y, z);
            return rtnvector;
        }

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <param name="vect2">The argument vector.</param>
        /// <returns>The two vectors added together.</returns>
        public static ByteVector3 operator +(ByteVector3 vect1, ByteVector3 vect2)
        {
            return new ByteVector3((byte)(vect1.X + vect2.X), (byte)(vect1.Y + vect2.Y), (byte)(vect1.Z + vect2.Z));
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <param name="vect2">The argument vector.</param>
        /// <returns>The two vectors subtracted from each other.</returns>
        public static ByteVector3 operator -(ByteVector3 vect1, ByteVector3 vect2)
        {
            return new ByteVector3((byte)(vect1.X - vect2.X), (byte)(vect1.Y - vect2.Y), (byte)(vect1.Z - vect2.Z));
        }

        /// <summary>
        /// Inverts a vector.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <returns>The initial vector in inverted form..</returns>
        public static ByteVector3 operator -(ByteVector3 vect1)
        {
            return new ByteVector3((byte)-vect1.X, (byte)-vect1.Y, (byte)-vect1.Z);
        }

        /// <summary>
        /// Divides one vector with another on a per-component basis.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <param name="vect2">The argument vector.</param>
        /// <returns>The initial vector, divided by the argument vector.</returns>
        public static ByteVector3 operator /(ByteVector3 vect1, ByteVector3 vect2)
        {
            return new ByteVector3((byte)(vect1.X / vect2.X), (byte)(vect1.Y / vect2.Y), (byte)(vect1.Z / vect2.Z));
        }

        /// <summary>
        /// Creates a new vector from a byte, placing it in every component.
        /// </summary>
        /// <param name="i">The component byte.</param>
        /// <returns>A new vector with the byte as all components.</returns>
        public static implicit operator ByteVector3(byte i)
        {
            return new ByteVector3(i);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        /// <inheritdoc />
        public IReadOnlyCollection<byte> Flatten()
        {
            return new[] { X, Y, Z };
        }
    }
}
