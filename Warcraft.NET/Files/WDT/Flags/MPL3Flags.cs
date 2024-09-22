using System;

namespace Warcraft.NET.Files.WDT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MPHDFlags"/>.
    /// </summary>
    [Flags]
    public enum MPL3Flags : ushort
    {
        /// <summary>
        /// Enable raytracing for this light. (Only visible with enabled D3D12 + min shadowRt level 2)
        /// </summary>
        Raytraced = 0x1,
    }
}
