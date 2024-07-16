using System;

namespace Warcraft.NET.Files.ADT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MLLDFlags"/>.
    /// </summary>
    [Flags]
    public enum MLLDFlags : uint
    {
        /// <summary>
        /// Has Tile Data
        /// </summary>
        HasTileData = 0x1,

        /// <summary>
        /// Depth (first) chunk is compressed.
        /// </summary>
        DepthChunkCompressed = 0x2,

        /// <summary>
        /// Alpha (second) chunk is compressed.
        /// </summary>
        AlphaChunkCompressed = 0x4,
    }
}
