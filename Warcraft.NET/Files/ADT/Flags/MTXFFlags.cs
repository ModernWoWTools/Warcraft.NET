using System;

namespace Warcraft.NET.Files.ADT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MTXFFlags"/>.
    /// </summary>
    [Flags]
    public enum MTXFFlags : ushort
    {
        /// <summary>
        /// The texture is unshaded
        /// </summary>
        FlatShading = 0x1,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown3 = 0x3,

        /// <summary>
        /// The texture has a scaling factor (MoP)
        /// </summary>
        ScaledTexture = 0x4,

        /// <summary>.
        /// Unknown Flag (MoP)
        /// </summary>
        Unknown24 = 0x24,

        /// <summary>.
        /// Unknown Flag (MoP)
        /// </summary>
        Unknown28 = 0x28
    }
}
