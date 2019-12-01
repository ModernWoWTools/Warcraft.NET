namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a two-dimensional collection of X and Y coordinates in a UV map.
    /// </summary>
    public struct UVMapEntry
    {
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public ushort X;

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public ushort Y;

        /// <summary>
        /// Initializes a new instance of the <see cref="UVMapEntry"/> struct.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public UVMapEntry(ushort x, ushort y)
        {
            X = x;
            Y = y;
        }

        public UVMapEntry(UVMapEntry inEntry)
            :this(inEntry.X, inEntry.Y)
        {
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}
