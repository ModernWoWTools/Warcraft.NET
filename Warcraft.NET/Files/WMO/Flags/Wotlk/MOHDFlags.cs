using System;
using Warcraft.NET.Files.WMO.Chunks.Wotlk;

namespace Warcraft.NET.Files.WMO.Flags.Wotlk
{
    /// <summary>
    /// Flags for <see cref="MOHD"/>
    /// </summary>
    [Flags]
    public enum MOHDFlags : ushort
    {
        /// <summary>
        /// Do not attenuate vertices based on distance to portal
        /// </summary>
        DoNotAttenuateVerticesBasedOnDistanceToPortal = 1,

        /// <summary>
        /// In 3.3.5a this flag switches between classic render path (MOHD color is baked into MCV values,
        /// all three batch types have their own rendering logic) and unified (MOHD color is added to 
        /// lighting at runtime, int. and ex. batches share the same rendering logic).
        /// See https://wowdev.wiki/WMO/Rendering for more details.
        /// </summary>
       UseUnifiedRenderPath = 2,

        /// <summary>
        /// Use real liquid type ID from DBCs instead of a local one. See <see cref="MLIQ"/> for further reference.
        /// </summary>
        UseLiquidTypeDbcId = 4,

        /// <summary>
        /// In 3.3.5a (and probably before) it prevents CMapObjGroup::FixColorVertexAlpha function to be executed.
        /// Alternatively, for the wotlk version of it, the function can be called with MOCV.a being set to 64,
        /// which will produce the same effect for easier implementation.
        /// For Wotlk+ rendering, it alters the behavior of the said function instead.
        /// See https://wowdev.wiki/WMO/Rendering for more details.
        /// </summary>
        DoNotFixVertexColorAlpha = 8,
    }
}
