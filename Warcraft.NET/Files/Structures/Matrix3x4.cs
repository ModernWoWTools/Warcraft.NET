using System.Collections.Generic;
using Warcraft.NET.Files.Interfaces;
using System.Numerics;

public class Matrix3x4 : IFlattenableData<float>
{
    /// <summary>
    /// The directional vector 'right'
    /// </summary>
    public Vector3 RotationX { get; set; }

    /// <summary>
    /// The directional vector 'up'
    /// </summary>
    public Vector3 RotationY { get; set; }

    /// <summary>
    /// The directional vector 'forward'
    /// </summary>
    public Vector3 RotationZ { get; set; }

    /// <summary>
    /// The scale of the matrix
    /// </summary>
    public Vector3 Scale { get; set; }

    /// <summary>
    /// The position of the matrix
    /// </summary>
    public Vector3 Position { get; set; }

    public Matrix3x4 (Vector3 column1, Vector3 column2, Vector3 column3, Vector3 column4)
    {
        float scaleX = column1.Length();
        float scaleY = column2.Length();
        float scaleZ = column3.Length();

        RotationX = column1 / scaleX;
        RotationY = column2 / scaleY;
        RotationZ = column3 / scaleZ;

        Scale = new Vector3(scaleX, scaleY, scaleZ);
        Position = column4;
    }

    public Vector3 GetForward()
    {
        return Vector3.Normalize(RotationZ);
    }

    public Vector3 GetUp()
    {
        return Vector3.Normalize(RotationY);
    }

    public Vector3 GetRight()
    {
        return Vector3.Normalize(RotationX);
    }

    public IReadOnlyCollection<float> Flatten()
    {
        return new[] {
            GetForward().X,GetForward().Y,GetForward().Z,
            Scale.X, Scale.Y, Scale.Z,
            Position.X, Position.Y, Position.Z };
    }
}