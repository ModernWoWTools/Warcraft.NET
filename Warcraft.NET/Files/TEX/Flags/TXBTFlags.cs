using System;

namespace Warcraft.NET.Files.TEX.Flags
{
    /// <summary>
    /// Flags for the <see cref="TXBTEntry"/>.
    /// </summary>
    [Flags]
    public enum TXBTFlags : byte
    {
        /// <summary>
        /// DXT1 with alpha (for dxt_type = DXT1, prefer argb1555 over rgb565, when DXT1 unavailable)
        /// </summary>
        AlphaDxt1 = 0x1,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown1 = 0x2,

        /// <summary>
        /// Unknown Flag DXT3
        /// </summary>
        Unknown2 = 0x4,

        /// <summary>
        /// Unknown Flag DXT3
        /// </summary>
        Unknown3 = 0x8,
    }
}
