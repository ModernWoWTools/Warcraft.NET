using System;

namespace Warcraft.NET.Files.WDT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MPHDFlags"/>.
    /// </summary>
    [Flags]
    public enum MPHDFlags : ushort
    {
        /// <summary>
        /// Has global wmo information
        /// </summary>
        UseGlobalMapObject = 0x0001,

        /// <summary>
        /// ADTs must have MCCV chunk
        /// </summary>
        HasMCCV = 0x0002,

        /// <summary>
        /// Use big alpha
        /// </summary>
        BigAlpha = 0x0004,

        /// <summary>
        /// If enabled, the ADT's MCRF(m2 only)/MCRD chunks need to be sorted by size category
        /// </summary>
        DoodadRefsSortedBySizeCat = 0x0008,

        /// <summary>
        /// (> Cata) ADTs must have MCLV chunk
        /// </summary>
        HasMCLV = 0x0010,

        /// <summary>
        /// (> Cata) Flips the ground display upside down to create a ceiling
        /// </summary>
        HasUpsideDownGround = 0x0020,

        /// <summary>
        /// (> MoP) Unknow
        /// </summary>
        Unk0x40 = 0x0040,

        /// <summary>
        /// (> MoP) Decides whether to influence alpha maps by _h+MTXP
        /// </summary>
        hasHeightTexturing = 0x0080,

        /// <summary>
        /// (> Legion) Unknow
        /// </summary>
        Unk0x0100 = 0x0100,

        /// <summary>
        /// (> 8.1.0) Client will load ADT using FileDataID instead of filename formatted with "%s\\%s_%d_%d.adt"
        /// </summary>
        HasMAID = 0x0200,

        /// <summary>
        /// Unknow
        /// </summary>
        Unk0x0400 = 0x0400,

        /// <summary>
        /// Unknow
        /// </summary>
        Unk0x0800 = 0x0800,

        /// <summary>
        /// Unknow
        /// </summary>
        Unk0x1000 = 0x1000,

        /// <summary>
        /// Unknow
        /// </summary>
        Unk0x2000 = 0x2000,

        /// <summary>
        /// Unknow
        /// </summary>
        Unk0x4000 = 0x4000,

        /// <summary>
        /// (> Legion) Implicitly set for map ids 0, 1, 571, 870, 1116 (continents). Affects the rendering of _lod.adt
        /// </summary>
        HasLOD = 0x8000,
    }
}
