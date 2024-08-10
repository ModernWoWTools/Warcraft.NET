using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.Structures
{
    public struct CImVector : IFlattenableData<byte>
    {
        public byte r, g, b, a;

        public CImVector(byte _r, byte _g, byte _b, byte _a)
        {
            r = _r;
            g = _g;
            b = _b;
            a = _a;
        }

        public byte[] toBytes()
        {
            byte[] arr = new byte[4] { r, g, b, a };
            return arr;
        }

        public CImVector(byte[] input)
        {
            if (input.Length == 4)
            {
                r = input[0];
                g = input[1];
                b = input[2];
                a = input[3];
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{r}, {g}, {b}, {a}";
        }

        public IReadOnlyCollection<byte> Flatten()
        {
            return new[] { r,g,b,a };
        }
    }
}
