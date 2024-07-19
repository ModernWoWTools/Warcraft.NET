using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Files.ADT.TerrainObject.Zero
{
    [AutoDocFile("adt", "Obj0 ADT")]
    public class TerrainObjectZero : TerrainObjectBase
    {
        /// <summary>
        /// Gets or sets the contains position information for all M2 models in this ADT.
        /// </summary>
        [ChunkOrder(6)]
        public MDDF ModelPlacementInfo { get; set; }

        /// <summary>
        /// Gets or sets the contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(7)]
        public MODF WorldModelObjectPlacementInfo { get; set; }

        /// <summary>
        /// Gets or sets the array of object MCNKs
        /// </summary>
        [ChunkOrder(8), ChunkArray(256)]
        public MCNK[] Chunks { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainObjectZero"/> class.
        /// </summary>
        public TerrainObjectZero() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainObjectZero"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObjectZero(byte[] inData) : base(inData) 
        {
        }
    }
}
