namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure representing a RGB color.
    /// </summary>
    public struct RGB
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
        /// Initializes a new instance of the <see cref="RGB"/> struct.
        /// </summary>
        /// <param name="R">Red</param>
        /// <param name="G">Green</param>
        /// <param name="B">Blue</param>
        public RGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RGB"/> struct.
        /// </summary>
        /// <param name="color">In color.</param>
        public RGB(RGB color) : this(color.R, color.G, color.B)
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"R: {R}, G: {G}, B: {B}";
        }
    }
}