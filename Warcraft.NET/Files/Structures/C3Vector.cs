using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a 3D vector of floats.
    /// </summary>
    public struct C3Vector : IFlattenableData<float>
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public C3Vector(float inX, float inY, float inZ)
        {
            X = inX;
            Y = inY;
            Z = inZ;
        }

        public Vector3 asVec3(){
            return new Vector3(X,Y,Z);
        }

        public byte[] asBytes(){
            return BitConverter.GetBytes(X).Concat(BitConverter.GetBytes(Y)).Concat(BitConverter.GetBytes(Z)).ToArray();
        }

        public C3Vector(byte[] input)
        {
            if (input.Length == 12)
            {
                X = BitConverter.ToSingle(input,0);
                Y = BitConverter.ToSingle(input,4);
                Z = BitConverter.ToSingle(input,8);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }

        public IReadOnlyCollection<float> Flatten()
        {
            return new[] { X, Y, Z };
        }
    }
}