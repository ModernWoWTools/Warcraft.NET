using System;

namespace Warcraft.NET.Files.M2.Flags
{
    /// <summary>
    /// Flags for the <see cref="MD21"/>.
    /// </summary>
    [Flags]
    public enum MD21Flags : uint
    {
        /// <summary>
        /// Tilt X
        /// </summary>
        TiltX = 0x1,

        /// <summary>
        /// Tilt Y
        /// </summary>
        TiltY = 0x2,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown4 = 0x4,

        /// <summary>
        /// Add textureCombinerCombos array to end of data
        /// </summary>
        UseTextureCombinerCombos = 0x8,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown10 = 0x10,

        /// <summary>
        /// Load phys data
        /// </summary>
        LoadPhysData = 0x20,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown40 = 0x40,

        /// <summary>
        /// Unknown Flag: set on all models since cata alpha
        /// </summary>
        Unknown80 = 0x80,

        /// <summary>
        /// Camera related flag
        /// </summary>
        CameraRelated = 0x100,

        /// <summary>
        /// In CATA: new version of ParticleEmitters. By default, length of M2ParticleOld is 476.
        /// </summary>
        NewParticleRecord = 0x200,

        /// <summary>
        /// But if 0x200 is set or if version is bigger than 271, length of M2ParticleOld is 492.
        /// </summary>
        Unknown400 = 0x400,

        /// <summary>
        /// When set, texture transforms are animated using the sequence being played on the bone found by index in tex_unit_lookup_table[textureTransformIndex], instead of using the sequence being played on the model's first bone. Example model: 6DU_HellfireRaid_FelSiege03_Creature
        /// </summary>
        TextureTransUseBoneSeq = 0x800,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown1000 = 0x1000,

        /// <summary>
        /// Unknown Flag: seen in various legion models
        /// </summary>
        Unknown2000 = 0x2000,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown4000 = 0x4000,

        /// <summary>
        /// Unknown Flag: seen in UI_MainMenu_Legion
        /// </summary>
        Unknown8000 = 0x8000,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown10000 = 0x10000,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown20000 = 0x20000,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown40000 = 0x40000,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown80000 = 0x80000,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        Unknown100000 = 0x100000,

        /// <summary>
        /// Unknown Flag: apparently use 24500 upgraded model format: chunked .anim files, change in the exporter reordering sequence+bone blocks before name
        /// </summary>
        Unknown200000 = 0x200000

    }
}
