using System;
namespace Warcraft.NET.Files.WMO.Flags
{
    /// <summary>
    /// A set of flags which can affect the way a doodad instance is rendered.
    /// </summary>
    [Flags]
    public enum MODDFlags : byte
    {
        /// <summary>
        /// Accepts a projected texture.
        /// </summary>
        AcceptProjectedTexture = 0x1,

        /// <summary>
        /// Unknown flag - Related to lighting.
        /// </summary>
        Unk_0x2 = 0x2,

        /// <summary>
        /// Unknown flag 0x4
        /// </summary>
        Unk_0x4 = 0x4,

        /// <summary>
        /// Unknown flag 0x8
        /// </summary>
        Unk_0x8 = 0x8,

        /// <summary>
        /// Unknown flag 0x16
        /// </summary>
        Unk_0x16 = 0x16,
    }
}
