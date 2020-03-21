using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Files.ADT.Terrain.BfA
{
    public class Terrain : TerrainBase
    {
        /// <summary>
        /// Gets or sets the water informations in this ADT.
        /// </summary>
        [ChunkOrder(3)]
        public MH2O Water { get; set; }

        /// <summary>
        /// Gets or sets the contains an array of offsets where MCNKs are in the file.
        /// </summary>
        [ChunkOrder(4), ChunkArray(256)]
        public MCNK[] Chunks { get; set; }

        /// <summary>
        /// Gets or Sets a array of flags for entries in MTEX. Always same number of entries as MTEX.
        /// </summary>
        [ChunkOrder(100), ChunkOptional]
        public MTXF TextureFlags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Terrain"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public Terrain(byte[] inData) : base(inData)
        {
        }
    }
}
