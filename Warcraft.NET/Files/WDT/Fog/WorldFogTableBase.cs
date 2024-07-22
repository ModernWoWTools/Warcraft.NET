using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDT.Chunks;

namespace Warcraft.NET.Files.WDT.Fog
{
    public abstract class WorldFogTableBase : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the WDT version. 1 for Legion, 2 for TWW.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="WorldFogTableBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldFogTableBase(byte[] inData = null) : base(inData)
        {
        }
    }
}
