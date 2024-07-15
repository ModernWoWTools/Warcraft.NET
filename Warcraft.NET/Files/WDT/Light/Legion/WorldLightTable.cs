using System;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks.Legion;
using WorldLightTableWoD = Warcraft.NET.Files.WDT.Light.WoD.WorldLightTable;

namespace Warcraft.NET.Files.WDT.Light.Legion
{
    public class WorldLightTable : WorldLightTableWoD
    {
        /// <summary>
        /// Legion Point Light Table
        /// </summary>
        [ChunkOrder(2), ChunkOptional]
        public MPL2 PointLights2 { get; set; }

        /// <summary>
        /// (Obsolete) Legion Point Light Table
        /// </summary>
        [ChunkIgnore, Obsolete("Use PointLights2 instead.")]
        public MPL2 LightTable2 { get { return PointLights2; } set { PointLights2 = value; } }

        /// <summary>
        /// Texture FileDataIDs
        /// </summary>
        [ChunkOrder(4), ChunkOptional]
        public MTEX TextureFileDataIDs { get; set; }

        /// <summary>
        /// Light animations
        /// </summary>
        [ChunkOrder(5), ChunkOptional]
        public MLTA LightAnimations { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldLightTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldLightTable(byte[] inData = null) : base(inData)
        {
        }
    }
}
