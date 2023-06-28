namespace Warcraft.NET.Files.BLP
{
    /// <summary>
    /// The compression type used in the <see cref="BLP"/> image.
    /// </summary>
    public enum TextureCompressionType : uint
    {
        /// <summary>
        /// The image is using JPEG compression.
        /// </summary>
        JPEG = 0,

        /// <summary>
        /// The image is using a colour palette.
        /// </summary>
        Palettized = 1,

        /// <summary>
        /// The image is compressed using the DXT algorithm.
        /// </summary>
        DXTC = 2,

        /// <summary>
        /// The image is not compressed.
        /// </summary>
        Uncompressed = 3,

        /// <summary>
        /// TODO: Unknown behaviour
        /// The image is not compressed.
        /// </summary>
        UncompressedAlternate = 4
    }
}
