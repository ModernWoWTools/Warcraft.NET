using System;

namespace Warcraft.NET.Files.ADT.Terrain.MCNK.Flags
{
    /// <summary>
    /// Flags for the <see cref="Header"/>.
    /// </summary>
    [Flags]
    public enum MCNKFlags : uint
    {
        /// <summary>
        /// Flags the MCNK as containing a static shadow map
        /// </summary>
        HasBakedShadows = 1,

        /// <summary>
        /// Flags the MCNK as impassible
        /// </summary>
        Impassible = 2,

        /// <summary>
        /// Flags the MCNK as a river
        /// </summary>
        IsRiver = 4,

        /// <summary>
        /// Flags the MCNK as an ocean
        /// </summary>
        IsOcean = 8,

        /// <summary>
        /// Flags the MCNK as magma
        /// </summary>
        IsMagma = 16,

        /// <summary>
        /// Flags the MCNK as slime
        /// </summary>
        IsSlime = 32,

        /// <summary>
        /// Flags the MCNK as containing an MCCV chunk
        /// </summary>
        HasVertexShading = 64,

        /// <summary>
        /// Unknown flag, but occasionally set.
        /// </summary>
        Unknown = 128,

        /// <summary>
        /// Disables repair of the alpha maps in this chunk.
        /// </summary>
        DoNotRepairAlphaMaps = 32768,

        /// <summary>
        /// Flags the MCNK for high-resolution holes. Introduced in WoW 5.3
        /// </summary>
        UsesHighResHoles = 65536,
    }
}
