using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks;

namespace Warcraft.NET.Files.WDT.Fog.BfA
{
    public class WorldFogTable : WorldFogTableBase
    {
        /// <summary>
        /// Gets or sets the volume fog entries
        /// </summary>
        [ChunkOrder(2)]
        public VFOG VolumeFogs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        public WorldFogTable() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldFogTable(byte[] inData) : base(inData)
        {
        }
    }
}
