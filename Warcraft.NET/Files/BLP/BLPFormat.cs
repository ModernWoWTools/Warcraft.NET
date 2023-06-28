namespace Warcraft.NET.Files.BLP
{
    /// <summary>
    /// The format of a <see cref="BLP"/> image file.
    /// </summary>
    public enum BLPFormat
    {
        /// <summary>
        /// BLP version 0. Can contain JPEG data.
        /// </summary>
        BLP0,

        /// <summary>
        /// BLP version 1. Usually palettized.
        /// </summary>
        BLP1,

        /// <summary>
        /// BLP version 2. Usually stores DXT compressed data.calls
        /// </summary>
        BLP2
    }
}
