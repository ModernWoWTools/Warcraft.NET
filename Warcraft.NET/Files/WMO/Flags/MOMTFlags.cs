using System;
namespace Warcraft.NET.Files.WMO.Flags
{
    /// <summary>
    /// Flags for <see cref="MOMT"/>
    /// </summary>
    [Flags]
    public enum MOMTFlags
    {
        /// <summary>
        /// Disable lighting logic in shader (but can still use vertex colors)
        /// </summary>
        F_UNLIT = 1,

        /// <summary>
        /// Disable fog shading (rarely used)
        /// </summary>
        F_UNFOGGED = 2,

        /// <summary>
        /// Two-sided
        /// </summary>
        F_UNCULLED = 4,

        /// <summary>
        /// Darkened, the intern face of windows are flagged 0x08
        /// </summary>
        F_EXTLIGHT = 8,

        /// <summary>
        /// Bright at night, unshaded. Used on windows and lamps in Stormwind, for example (see emissive color)
        /// </summary>
        F_SIDN = 16,

        /// <summary>
        /// Lighting related (flag checked in CMapObj::UpdateSceneMaterials)
        /// </summary>
        F_WINDOW = 32,

        /// <summary>
        /// Texture clamp S (force this material's textures to use clamp s addressing)
        /// </summary>
        F_CLAMP_S = 64,

        /// <summary>
        /// Texture clamp T (force this material's textures to use clamp t addressing)
        /// </summary>
        F_CLAMP_T = 128,

        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 256
    }
}
