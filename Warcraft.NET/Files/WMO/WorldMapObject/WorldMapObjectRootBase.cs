using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WMO.Chunks;

namespace Warcraft.NET.Files.WMO.WorldMapObject
{
    public abstract class WorldMapObjectRootBase : WorldMapObjectBase
    {
        /// <summary>
        /// This chunk defines doodad sets.
        /// </summary>
        [ChunkOptional]
        public MODS DoodadSets { get; set; } = new();

        /// <summary>
        /// This chunk defines doodad sets.
        /// </summary>
        [ChunkOptional]
        public MODD Doodads { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectRootBase"/> class.
        /// </summary>
        public WorldMapObjectRootBase() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldMapObjectRootBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectRootBase(byte[] inData) : base(inData)
        {
        }
    }
}
