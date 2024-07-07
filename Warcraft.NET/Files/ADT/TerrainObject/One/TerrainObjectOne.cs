using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks.Legion;

namespace Warcraft.NET.Files.ADT.TerrainObject.One
{
    public class TerrainObjectOne : TerrainObjectBase
    {
        /// <summary>
        /// Level of detail offset information
        /// </summary>
        [ChunkOrder(6), ChunkOptional]
        public MLFD LevelForDetail { get; set; }

        /// <summary>
        /// Contains position information for all M2 models in this ADT.
        /// </summary>
        [ChunkOrder(7)]
        public MLDD LevelDoodadDetail { get; set; }

        /// <summary>
        /// Contains M2 model bounding information. Same count as <see cref="MLDD"/>
        /// </summary>
        [ChunkOrder(8)]
        public MLDX LevelDoodadExtent { get; set; }

        /// <summary>
        /// Contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(9)]
        public MLMD LevelWorldObjectDetail { get; set; }

        /// <summary>
        /// Contains WMO model bounding information. Same count as <see cref="MLMD"/>
        /// </summary>
        [ChunkOrder(10)]
        public MLMX LevelWorldObjectExtent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainObjectOne"/> class.
        /// </summary>
        public TerrainObjectOne() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainObjectOne"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainObjectOne(byte[] inData) : base(inData) 
        {
        }
    }
}
