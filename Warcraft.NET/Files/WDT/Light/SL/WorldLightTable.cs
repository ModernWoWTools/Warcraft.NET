using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks.SL;
using WorldLightTableLegion = Warcraft.NET.Files.WDT.Light.Legion.WorldLightTable;

namespace Warcraft.NET.Files.WDT.Light.SL
{
    [AutoDocFile("wdt", "_lgt WDT")]
    public class WorldLightTable : WorldLightTableLegion
    {
        /// <summary>
        /// SL Point Light Table
        /// </summary>
        [ChunkOrder(3), ChunkOptional]
        public MPL3 PointLights3 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldLightTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldLightTable(byte[] inData = null) : base(inData)
        {
        }
    }
}
