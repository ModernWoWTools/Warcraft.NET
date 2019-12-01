using System;

namespace Warcraft.NET.Files.ADT.Flags
{
    /// <summary>
    /// Flags for the <see cref="MDDFEntry"/>.
    /// </summary>
    [Flags]
    public enum MDDFFlags : ushort
    {
        /// <summary>
        /// Biodome. Perhaps a skybox?
        /// </summary>
        Biodome = 0x1,

        /// <summary>
        /// Possibly used for vegetation and grass.
        /// </summary>
        Shrubbery = 0x2,

        /// <summary>
        /// Unknown Flag (Legion)
        /// </summary>
        Unknown4 = 0x4,

        /// <summary>
        /// Unknown Flag (Legion)
        /// </summary>
        Unknown8 = 0x8,

        /// <summary>
        /// Liquied_Known 
        /// </summary>
        LiquidKnown = 0x20,

        /// <summary>
        /// Flag to skip MMID and MMDX and point directly to CASC Filedata Ids for more performance (Legion)
        /// </summary>
        NameIdIsFiledataId = 0x40,

        /// <summary>
        /// Unknown Flag (Legion)
        /// </summary>
        Unknown100 = 0x100
    }
}
