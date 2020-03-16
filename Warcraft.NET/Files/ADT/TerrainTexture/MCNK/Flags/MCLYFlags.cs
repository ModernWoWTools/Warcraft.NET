using System;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Flags
{
    /// <summary>
    /// Flags for the <see cref="MCLYEntry"/>.
    /// </summary>
    [Flags]
    public enum MCLYFlags : uint
    {
        /// <summary>
        /// The texture rotates 45 degrees per tick.
        /// </summary>
        Animated45RotationPerTick = 0x001,

        /// <summary>
        /// The texture rotates 90 degrees per tick.
        /// </summary>
        Animated90RotationPerTick = 0x002,

        /// <summary>
        /// The texture rotates 180 degrees per tick.
        /// </summary>
        Animated180RotationPerTick = 0x004,

        /// <summary>
        /// The texture has an animation speed of 1.
        /// </summary>
        AnimSpeed1 = 0x008,

        /// <summary>
        /// The texture has an animation speed of 2.
        /// </summary>
        AnimSpeed2 = 0x010,

        /// <summary>
        /// The texture has an animation speed of 3.
        /// </summary>
        AnimSpeed3 = 0x020,

        /// <summary>
        /// The texture's animation is enabled.
        /// </summary>
        AnimationEnabled = 0x040,

        /// <summary>
        /// The texture is emissive.
        /// </summary>
        EmissiveLayer = 0x080,

        /// <summary>
        /// The texture uses the alpha channel.
        /// </summary>
        UseAlpha = 0x100,

        /// <summary>
        /// The texture's alpha channel is compressed.
        /// </summary>
        CompressedAlpha = 0x200,

        /// <summary>
        /// The texture uses cube mapped reflection.
        /// </summary>
        UseCubeMappedReflection = 0x400,
    }
}
