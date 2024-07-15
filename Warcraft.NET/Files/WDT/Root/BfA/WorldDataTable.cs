using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks.BfA;

namespace Warcraft.NET.Files.WDT.Root.BfA
{
    public class WorldDataTable : WorldDataTableBase
    {
        /// <summary>
        /// Gets or sets the contains the WDT version.
        /// </summary>
        [ChunkOrder(6), ChunkOptional]
        public MAID Ids { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        public WorldDataTable() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldDataTable(byte[] inData) : base(inData)
        {
        }
    }
}
