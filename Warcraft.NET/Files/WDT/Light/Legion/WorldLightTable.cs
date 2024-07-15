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
