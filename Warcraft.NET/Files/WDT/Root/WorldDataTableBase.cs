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
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldDataTableBase(byte[] inData) : base(inData)
        {
        }
    }
}
