using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;
using Warcraft.NET.Files.ADT.Chunks.Wotlk;

namespace Warcraft.NET.Files.ADT.Terrain.Wotlk
{
    public class Terrain : TerrainBase
    {
        /// <summary>
        /// Gets or sets the contains a list of all textures referenced by this ADT.
        /// </summary>
        [ChunkOrder(3)]
        public MCIN MapChunkOffsets { get; set; }

        /// <summary>
        /// Gets or sets the contains a list of all textures referenced by this ADT.
        /// </summary>
        [ChunkOrder(4)]
        public MTEX Textures { get; set; }

        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(5)]
        public MMDX Models { get; set; }

        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(6)]
        public MMID ModelIndices { get; set; }

        /// <summary>
        /// Gets or sets the contains a list of all WMOs referenced by this ADT.
        /// </summary>
        [ChunkOrder(7)]
        public MWMO WorldModelObjects { get; set; }

        /// <summary>
        /// Gets or sets the contains WMO indexes for the list in ADTWMOs (MWMO chunk).
        /// </summary>
        [ChunkOrder(8)]
        public MWID WorldModelObjectIndices { get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all M2 models in this ADT.
        /// </summary>
        [ChunkOrder(8)]
        public MDDF ModelPlacementInfo { get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(9)]
        public MODF WorldModelObjectPlacementInfo { get; set; }

        /// <summary>
        /// Gets or sets the water informations in this ADT.
        /// </summary>
        [ChunkOrder(10), ChunkOptional]
        public MH2O Water { get; set; }

        /// <summary>
        /// Gets or sets the contains an array of offsets where MCNKs are in the file.
        /// </summary>
        [ChunkOrder(11), ChunkArray(256)]
        public MCNK[] Chunks { get; set; }

        /// <summary>
        /// Gets or Sets a array of flags for entries in MTEX. Always same number of entries as MTEX.
        /// </summary>
        [ChunkOrder(100), ChunkOptional]
        public MTXF TextureFlags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wotlk.Terrain"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public Terrain(byte[] inData) : base(inData)
        {
        }
    }
}
