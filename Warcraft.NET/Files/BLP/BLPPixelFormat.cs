namespace Warcraft.NET.Files.BLP
{
    /// <summary>
    /// The format of the pixels stored in a <see cref="BLP"/> file.
    /// </summary>
    public enum BLPPixelFormat : uint
    {
        /// <summary>
        /// DXT1 compressed pixels.
        /// </summary>
        DXT1 = 0,

        /// <summary>
        /// DXT3 compressed pixels.
        /// </summary>
        DXT3 = 1,

        /// <summary>
        /// ARGB8888 formatted pixels.
        /// </summary>
        ARGB8888 = 2,

        /// <summary>
        /// PAL ARGB1555 formatted pixels.
        /// </summary>
        PalARGB1555DitherFloydSteinberg = 3,

        /// <summary>
        /// PAL ARGB4444 formatted pixels.
        /// </summary>
        PalARGB4444DitherFloydSteinberg = 4,

        /// <summary>
        /// PAL ARGB565 formatted pixels.
        /// </summary>
        PalARGB565DitherFloydSteinberg = 5,

        /// <summary>
        /// DXT5 compressed pixels.
        /// </summary>
        DXT5 = 7,

        /// <summary>
        /// Palettized pixels, that is, the pixels are indices into the stored colour palette.
        /// </summary>
        Palettized = 8,

        /// <summary>
        /// PAL ARGB2565 formatted pixels.
        /// </summary>
        PalARGB2565DitherFloydSteinberg = 9
    }
}
