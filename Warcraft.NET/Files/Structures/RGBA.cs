namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a RGBA color.
    /// </summary>
    public struct RGBA
    {
        /// <summary>
        /// Gets or sets the red value.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Gets or sets the green value.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// Gets or sets the blue value.
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Gets or sets the alpha value.
        /// </summary>
        public byte A { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> struct.
        /// </summary>
        /// <param name="R">Red</param>
        /// <param name="G">Green</param>
        /// <param name="B">Blue</param>
        /// <param name="A">Alpha</param>
        public RGBA(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGBA"/> struct.
        /// </summary>
        /// <param name="color">In color.</param>
        public RGBA(RGBA color)
            : this(color.R, color.G, color.B, color.A)
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"R: {R}, G: {G}, B: {B}, A:{A}";
        }
    }
}
