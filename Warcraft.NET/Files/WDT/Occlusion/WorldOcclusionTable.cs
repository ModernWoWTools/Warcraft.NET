using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks;
using Warcraft.NET.Files.WDT.Chunks.WoD;

namespace Warcraft.NET.Files.WDT.Occlusion
{
    public class WorldOcclusionTable : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the contains the WDT version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; } = new MVER(18);

        /// <summary>
        /// Gets or sets the WDT Header.
        /// </summary>
        [ChunkOrder(2)]
        public MAOI MapAreaOcclusionIndex { get; set; }

        /// <summary>
        /// Gets or sets the WDT Tiles.
        /// </summary>
        [ChunkOrder(3)]
        public MAOH MapAreaOcclusionHeight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldOcclusionTable"/> class.
        /// </summary>
        public WorldOcclusionTable()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldOcclusionTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldOcclusionTable(byte[] inData) : base(inData)
        {
        }
    }
}
