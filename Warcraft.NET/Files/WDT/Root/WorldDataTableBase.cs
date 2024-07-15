using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks;

namespace Warcraft.NET.Files.WDT.Root
{
    public abstract class WorldDataTableBase : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the contains the WDT version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        /// <summary>
        /// Gets or sets the WDT Header.
        /// ADT.
        /// </summary>
        [ChunkOrder(2)]
        public MPHD Header { get; set; }

        /// <summary>
        /// Gets or sets the WDT Tiles.
        /// ADT.
        /// </summary>
        [ChunkOrder(3)]
        public MAIN Tiles { get; set; }

        /// <summary>
        /// Gets or sets the contains a list of all WMOs referenced by this ADT.
        /// </summary>
        [ChunkOrder(4), ChunkOptional]
        public ADT.Chunks.MWMO WorldModelObjects { get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(5), ChunkOptional]
        public ADT.Chunks.MODF WorldModelObjectPlacementInfo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        public WorldDataTableBase() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldDataTableBase(byte[] inData) : base(inData)
        {
        }
    }
}
