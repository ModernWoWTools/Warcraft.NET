using System;

namespace Warcraft.NET.Files.ADT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MHDRFlags"/>.
    /// </summary>
    [Flags]
    public enum MHDRFlags : uint
    {
        /// <summary>
        /// Map contains MFBO chuck
        /// </summary>
        MFBO = 0x1,

        /// <summary>
        /// ADT is northend Map
        /// </summary>
        Northrend = 0x2
    }
}
