using System;

namespace Warcraft.NET.Files.WDT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MAINFlags"/>.
    /// </summary>
    [Flags]
    public enum MAINFlags : ushort
    {
        /// <summary>
        /// Tile has ADT
        /// </summary>
        HasAdt = 0x0001,

        /// <summary>
        /// Tile has water at 0 point
        /// </summary>
        HasWater = 0x0002,

        /// <summary>
        /// Use big alpha
        /// </summary>
        IsLoaded = 0x0004,
    }
}
