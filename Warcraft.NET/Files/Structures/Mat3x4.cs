using System;
using System.Collections.Generic;
using System.Linq;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;
using System.Numerics;

public class Mat3x4 : IFlattenableData<float>
{

    public C3Vector a { get; set; }
    public C3Vector b { get; set; }
    public C3Vector c { get; set; }
    public C3Vector d { get; set; }

    public Mat3x4(byte[] input)
    {
        if (input.Length == 48)
        {
            a = new C3Vector(input.Take(12).ToArray());
            b = new C3Vector(input.Skip(12).Take(12).ToArray());
            c = new C3Vector(input.Skip(24).Take(12).ToArray());
            d = new C3Vector(input.Skip(36).Take(12).ToArray());

        }
    }

    public Mat3x4(float[] input)
    {
        if (input.Length == 12)
        {
            a = new C3Vector(input[0], input[1], input[2]);
            b = new C3Vector(input[3], input[4], input[5]);
            c = new C3Vector(input[6], input[7], input[8]);
            d = new C3Vector(input[9], input[10], input[11]);
        }
    }
    public Mat3x4(C3Vector _a, C3Vector _b, C3Vector _c, C3Vector _d)
    {
        a = _a;
        b = _b;
        c = _c;
        d = _d;
    }

    public byte[] asBytes(){
        return a.asBytes().Concat(b.asBytes()).Concat(c.asBytes()).Concat(d.asBytes()).ToArray();
    }

    public IReadOnlyCollection<float> Flatten()
    {
        return new[] { a.X, a.Y, a.Z, b.X, b.Y, b.Z, c.X, c.Y, c.Z, d.X, d.Y, d.Z };
    }
}