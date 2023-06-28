namespace Warcraft.NET.Files.BLP
{
    /// <summary>
    /// A structure representing a graphical resolution, consisting of two uint values.
    /// </summary>
    public struct Resolution
    {
        /// <summary>
        /// The horizontal resolution (or X resolution).
        /// </summary>
        public uint X;

        /// <summary>
        /// The vertical resolution (or Y resolution).
        /// </summary>
        public uint Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolution"/> struct from a height and a width.
        /// </summary>
        /// <param name="inX">The input width component.</param>
        /// <param name="inY">The input height component.</param>
        public Resolution(uint inX, uint inY)
        {
            X = inX;
            Y = inY;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resolution"/> struct from a single input uint, filling all components.
        /// </summary>
        /// <param name="all">The input component.</param>
        public Resolution(uint all)
            : this(all, all)
        {
        }

        /// <summary>
        /// Creates a string representation of the current object.
        /// </summary>
        /// <returns>A string representation of the current object.</returns>
        public override readonly string ToString()
        {
            return $"{X}x{Y}";
        }
    }
}
