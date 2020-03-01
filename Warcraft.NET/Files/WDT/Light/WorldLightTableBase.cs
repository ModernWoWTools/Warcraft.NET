using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks;

namespace Warcraft.NET.Files.WDT.Light
{
    public abstract class WorldLightTableBase : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the contains the WDT version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldLightTableBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldLightTableBase(byte[] inData = null) : base(inData)
        {
        }
    }
}
