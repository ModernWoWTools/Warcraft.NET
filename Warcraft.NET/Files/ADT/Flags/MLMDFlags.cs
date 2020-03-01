using System;

namespace Warcraft.NET.Files.ADT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MODFFlags"/>.
    /// </summary>
    [Flags]
    public enum MODFFlags : ushort
    {
        /// <summary>
        /// Set for destroyable buildings like the tower in DeathknightStart. This makes it a server-controllable game object.
        /// </summary>
        Destroyable = 0x1,

        /// <summary>
        /// Load _lod1.WMO for use dependent on distance (WoD)
        /// </summary>
        UseLod = 0x2,

        /// <summary>
        /// Use scale. Otherwise scale is 1.0 (Legion)
        /// </summary>
        HasScale = 0x4,

        /// <summary>
        /// Flag to skip MWID and MODF and point directly to CASC Filedata Ids for more performance (Legion)
        /// </summary>
        NameIdIsFiledataId = 0x8,
    }
}
