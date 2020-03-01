using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks.Legion;
using WorldLightTableWoD = Warcraft.NET.Files.WDT.Light.WoD.WorldLightTable;

namespace Warcraft.NET.Files.WDT.Light.Legion
{
    public class WorldLightTable : WorldLightTableWoD
    {
        /// <summary>
        /// WoD Light Table
        /// </summary>
        [ChunkOrder(3), ChunkOptional]
        public MPL2 LightTable2 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldLightTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldLightTable(byte[] inData = null) : base(inData)
        {
        }
    }
}
