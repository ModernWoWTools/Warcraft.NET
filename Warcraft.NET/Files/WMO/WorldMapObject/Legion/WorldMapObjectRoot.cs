using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WMO.Chunks;
using Warcraft.NET.Files.WMO.Chunks.Legion;
using MOHD = Warcraft.NET.Files.WMO.Chunks.Legion.MOHD;
using MOMT = Warcraft.NET.Files.WMO.Chunks.Wotlk.MOMT;

namespace Warcraft.NET.Files.WMO.WorldMapObject.Legion
{
    [AutoDocFile("wmo", "Root WMO")]
    public class WorldMapObjectRoot : WorldMapObjectRootBase
    {
        /// <summary>
        /// Gets or sets the WMO header
        /// </summary>
        [ChunkOrder(2)]
        public MOHD Header { get; set; }

        /// <summary>
        /// Gets or sets WMO textures. 
        /// </summary>
        [ChunkOrder(3)]
        public MOTX Textures { get; set; }

        /// <summary>
        /// Gets or sets the materials.
        /// </summary>
        [ChunkOrder(4)]
        public MOMT Materials { get; set; }

        /// <summary>
        /// List of filenames for M2 (mdx) models that appear in this WMO.
        /// </summary>
        [ChunkOrder(5)]
        public MODN DoodadNames { get; set; }

        /// <summary>
        /// List of file ids for group files
        /// </summary>
        [ChunkOrder(6)]
        public GFID GroupFileFileIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.WorldMapObjectRoot"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectRoot(byte[] inData) : base(inData)
        {
        }
    }
}
