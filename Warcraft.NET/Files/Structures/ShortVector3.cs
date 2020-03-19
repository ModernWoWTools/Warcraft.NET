using System.Collections.Generic;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a 3D vector of shorts.
    /// </summary>
    public struct ShortVector3 : IFlattenableData<short>
    {
        /// <summary>
        /// X coordinate of this vector.
        /// </summary>
        public short X;

        /// <summary>
        /// Y coordinate of this vector.
        /// </summary>
        public short Y;

        /// <summary>
        /// Z coordinate of this vector.
        /// </summary>
        public short Z;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVector3"/> struct.
        /// </summary>
        /// <param name="inX">X coordinate.</param>
        /// <param name="inY">Y coordinate.</param>
        /// <param name="inZ">Z coordinate.</param>
        public ShortVector3(short inX, short inY, short inZ)
        {
            X = inX;
            Y = inY;
            Z = inZ;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVector3"/> struct using
        /// normalized signed bytes instead of straight short values.
        /// </summary>
        /// <param name="inX">X.</param>
        /// <param name="inY">Y.</param>
        /// <param name="inZ">Z.</param>
        public ShortVector3(sbyte inX, sbyte inY, sbyte inZ)
        {
            X = (short)(127 / inX);
            Y = (short)(127 / inY);
            Z = (short)(127 / inZ);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortVector3"/> struct.
        /// </summary>
        /// <param name="all">All.</param>
        public ShortVector3(short all) : this(all, all, all)
        {
        }

        /// <summary>
        /// Computes the dot product of two vectors.
        /// </summary>
        /// <param name="start">The start vector.</param>
        /// <param name="end">The end vector.</param>
        /// <returns>The dot product of the two vectors.</returns>
        public static short Dot(ShortVector3 start, ShortVector3 end)
        {
            return (short)((start.X * end.X) + (start.Y * end.Y) + (start.Z * end.Z));
        }

        /// <summary>
        /// Computes the cross product of two vectors, producing a new vector which
        /// is orthogonal to the two original vectors.
        /// </summary>
        /// <param name="start">The start vector.</param>
        /// <param name="end">The end vector.</param>
        /// <returns>The cross product of the two vectors.</returns>
        public static ShortVector3 Cross(ShortVector3 start, ShortVector3 end)
        {
            var x = (short)((start.Y * end.Z) - (end.Y * start.Z));
            var y = (short)(((start.X * end.Z) - (end.X * start.Z)) * -1);
            var z = (short)((start.X * end.Y) - (end.X * start.Y));

            var rtnvector = new ShortVector3(x, y, z);
            return rtnvector;
        }

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <param name="vect2">The argument vector.</param>
        /// <returns>The two vectors added together.</returns>
        public static ShortVector3 operator +(ShortVector3 vect1, ShortVector3 vect2)
        {
            return new ShortVector3((short)(vect1.X + vect2.X), (short)(vect1.Y + vect2.Y), (short)(vect1.Z + vect2.Z));
        }

        /// <summary>
        /// Subtracts two vectors.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <param name="vect2">The argument vector.</param>
        /// <returns>The two vectors subtracted from each other.</returns>
        public static ShortVector3 operator -(ShortVector3 vect1, ShortVector3 vect2)
        {
            return new ShortVector3((short)(vect1.X - vect2.X), (short)(vect1.Y - vect2.Y), (short)(vect1.Z - vect2.Z));
        }

        /// <summary>
        /// Inverts a vector.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <returns>The initial vector in inverted form..</returns>
        public static ShortVector3 operator -(ShortVector3 vect1)
        {
            return new ShortVector3((short)-vect1.X, (short)-vect1.Y, (short)-vect1.Z);
        }

        /// <summary>
        /// Divides one vector with another on a per-component basis.
        /// </summary>
        /// <param name="vect1">The initial vector.</param>
        /// <param name="vect2">The argument vector.</param>
        /// <returns>The initial vector, divided by the argument vector.</returns>
        public static ShortVector3 operator /(ShortVector3 vect1, ShortVector3 vect2)
        {
            return new ShortVector3((short)(vect1.X / vect2.X), (short)(vect1.Y / vect2.Y), (short)(vect1.Z / vect2.Z));
        }

        /// <summary>
        /// Creates a new vector from a short, placing it in every component.
        /// </summary>
        /// <param name="i">The component short.</param>
        /// <returns>A new vector with the short as all components.</returns>
        public static implicit operator ShortVector3(short i)
        {
            return new ShortVector3(i);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        /// <inheritdoc />
        public IReadOnlyCollection<short> Flatten()
        {
            return new[] { X, Y, Z };
        }
    }
}
