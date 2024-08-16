using System;
using System.Collections.Generic;
using System.Text;

namespace Warcraft.NET.Files.Phys.Enums
{
    public enum JointType : ushort
    {
        sphericalJoint = 0,
        shoulderJoint = 1,
        weldJoint = 2,
        revoluteJoint = 3,
        prismaticJoint = 4,
        distanceJoint = 5,
    }
}
