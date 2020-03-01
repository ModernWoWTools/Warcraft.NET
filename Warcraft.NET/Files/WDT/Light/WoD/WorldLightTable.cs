using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks.WoD;

namespace Warcraft.NET.Files.WDT.Light.WoD
{
    public class WorldLightTable : WorldLightTableBase
    {
        /// <summary>
        /// WoD Light Table
        /// </summary>
        [ChunkOrder(2), ChunkOptional]
        public MPLT LightTable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldLightTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldLightTable(byte[] inData = null) : base(inData)
        {
        }
    }
}
