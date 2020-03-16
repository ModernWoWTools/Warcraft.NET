using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Files.ADT.TerrainObject
{
    public abstract class TerrainObjectBase : ChunkedFile
    {
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(2), ChunkOptional]
        public MMDX Models { get; set; }

        /// <summary>
        /// Gets or sets the contains M2 model indexes for the list in ADTModels (MMDX chunk).
        /// </summary>
        [ChunkOrder(3), ChunkOptional]
        public MMID ModelIndices { get; set; }

        /// <summary>
        /// Gets or sets the contains a list of all WMOs referenced by this ADT.
        /// </summary>
        [ChunkOrder(4), ChunkOptional]
        public MWMO WorldModelObjects { get; set; }

        /// <summary>
        /// Gets or sets the contains WMO indexes for the list in ADTWMOs (MWMO chunk).
        /// </summary>
        [ChunkOrder(5), ChunkOptional]
        public MWID WorldModelObjectIndices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainObjectBase"/> class.
        /// </summary>
        public TerrainObjectBase() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainObjectBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObjectBase(byte[] inData) : base(inData) 
        {
        }
    }
}
