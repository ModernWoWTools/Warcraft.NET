using System;
using System.Globalization;
using System.Numerics;

namespace Warcraft.NET.Files.Structures;

public struct BoundingBox : IEquatable<BoundingBox>, IFormattable
{
    /// <summary>
    /// The minimum point of the box
    /// </summary>
    public Vector3 Minimum;

    /// <summary>
    /// The maximum point of the box
    /// </summary>
    public Vector3 Maximum;
    
    /// <summary>
    /// Returns the width of the bounding box
    /// </summary>
    public float Width => Maximum.X - Minimum.X;
    
    /// <summary>
    /// Returns the height of the bounding box
    /// </summary>
    public float Height => Maximum.Y - Minimum.Y;

    /// <summary>
    /// Returns the height of the bounding box
    /// </summary>
    public float Depth => Maximum.Z - Minimum.Z;

    /// <summary>
    /// Initializes a new instance of the <see cref="BoundingBox"/> struct.
    /// </summary>
    /// <param name="minimum">The minimum vertex of the bounding box.</param>
    /// <param name="maximum">The maximum vertex of the bounding box.</param>
    public BoundingBox(Vector3 minimum, Vector3 maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }
    
    /// <summary>
    /// Retrieves the corners of the bounding box
    /// </summary>
    /// <returns>An array of points representing the corners of the bounding box</returns>
    public void GetCorners(Vector3[] corners)
    {
        corners[0] = new Vector3(Minimum.X, Maximum.Y, Maximum.Z);
        corners[1] = new Vector3(Maximum.X, Maximum.Y, Maximum.Z);
        corners[2] = new Vector3(Maximum.X, Minimum.Y, Maximum.Z);
        corners[3] = new Vector3(Minimum.X, Minimum.Y, Maximum.Z);
        corners[4] = new Vector3(Minimum.X, Maximum.Y, Minimum.Z);
        corners[5] = new Vector3(Maximum.X, Maximum.Y, Minimum.Z);
        corners[6] = new Vector3(Maximum.X, Minimum.Y, Minimum.Z);
        corners[7] = new Vector3(Minimum.X, Minimum.Y, Minimum.Z);
    }
    
    public bool Equals(BoundingBox other)
    {
        return Minimum == other.Minimum && Maximum == other.Maximum;
    }

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return string.Format(CultureInfo.CurrentCulture, "Minimum:{0} Maximum:{1}", Minimum.ToString(), Maximum.ToString());
    }
}