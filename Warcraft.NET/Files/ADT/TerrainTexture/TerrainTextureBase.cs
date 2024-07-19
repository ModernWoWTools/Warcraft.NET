using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;
using Warcraft.NET.Files.ADT.Chunks.Cata;
using Warcraft.NET.Files.ADT.Chunks.MoP;

namespace Warcraft.NET.Files.ADT.TerrainTexture
{
    public abstract class TerrainTextureBase : ChunkedFile
    {
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        [ChunkOrder(2)]
        public MAMP Fred { get; set; }

        [ChunkOrder(3), ChunkOptional]
        public MTEX Textures { get; set; }

        [ChunkOrder(6), ChunkArray(256)]
        public MCNK[] Chunks { get; set; }

        [ChunkOrder(7), ChunkOptional]
        public MTXP TextureParameters { get; set; }

        [ChunkOrder(8), ChunkOptional]
        public MTXF TextureFlags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainTexture"/> class.
        /// </summary>
        public TerrainTextureBase() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainTextureBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainTextureBase(byte[] inData) : base(inData) 
        {
        }
    }
}
