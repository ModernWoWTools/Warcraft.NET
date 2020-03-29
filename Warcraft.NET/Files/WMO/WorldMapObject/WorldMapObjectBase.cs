using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WMO.Chunks;

namespace Warcraft.NET.Files.WMO.WorldMapObject
{
    public abstract class WorldMapObjectBase : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the contains the WMO version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectBase"/> class.
        /// </summary>
        public WorldMapObjectBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectBase(byte[] inData)
        {
            LoadBinaryData(inData);
        }
    }
}
