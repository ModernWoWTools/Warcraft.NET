using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks.WoD;

namespace Warcraft.NET.Files.WDT.Light.WoD
{
    [AutoDocFile("wdt", "_lgt WDT")]
    public class WorldLightTable : WorldLightTableBase
    {
        /// <summary>
        /// WoD Point Light Table
        /// </summary>
        [ChunkOrder(2), ChunkOptional]
        public MPLT PointLights { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldLightTable"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldLightTable(byte[] inData = null) : base(inData)
        {
        }
    }
}
