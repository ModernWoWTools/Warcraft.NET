using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks.BfA;

namespace Warcraft.NET.Files.ADT.TerrainTexture.BfA
{
    [AutoDocFile("adt", "_tex0 ADT")]
    public class TerrainTexture : TerrainTextureBase
    {
        [ChunkOrder(4), ChunkOptional]
        public MDID TextureDiffuseIds { get; set; }

        [ChunkOrder(5), ChunkOptional]
        public MHID TextureHeightIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainTexture"/> class.
        /// </summary>
        public TerrainTexture() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainTexture"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainTexture(byte[] inData) : base(inData)
        {
        }
    }
}
