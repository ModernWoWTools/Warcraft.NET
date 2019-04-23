using System.Collections.Generic;
using Warcraft.NET.Files.Interfaces;
using SharpDX;

namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a three-dimensional collection of euler angles.
    /// </summary>
    public struct Rotator : IFlattenableData<float>
    {
        private Vector3 _values;

        /// <summary>
        /// Gets or sets the pitch of the rotator.
        /// </summary>
        public float Pitch
        {
            get => _values.X;
            set => _values.X = value;
        }

        /// <summary>
        /// Gets or sets the yaw of the rotator.
        /// </summary>
        public float Yaw
        {
            get => _values.Y;
            set => _values.Y = value;
        }

        /// <summary>
        /// Gets or sets the roll of the rotator.
        /// </summary>
        public float Roll
        {
            get => _values.Z;
            set => _values.Z = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rotator"/> struct.
        /// </summary>
        /// <param name="inPitch">The pitch.</param>
        /// <param name="inYaw">The yaw.</param>
        /// <param name="inRoll">The roll.</param>
        public Rotator(float inPitch, float inYaw, float inRoll)
        {
            _values = new Vector3(inPitch, inYaw, inRoll);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rotator"/> struct.
        /// </summary>
        /// <param name="inVector">In vector.</param>
        public Rotator(Vector3 inVector)
            : this(inVector.X, inVector.Y, inVector.Z)
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Pitch: {Pitch}, Yaw: {Yaw}, Roll: {Roll}";
        }

        /// <inheritdoc />
        public IReadOnlyCollection<float> Flatten()
        {
            return _values.ToArray();
        }
    }
}
