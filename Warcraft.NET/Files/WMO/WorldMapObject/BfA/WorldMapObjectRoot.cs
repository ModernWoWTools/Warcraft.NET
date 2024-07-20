using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WMO.Chunks;
using Warcraft.NET.Files.WMO.Chunks.BfA;
using Warcraft.NET.Files.WMO.Chunks.Legion;

namespace Warcraft.NET.Files.WMO.WorldMapObject.BfA
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
        /// Gets or sets textures.
        /// Starting with 8.1, MOTX is no longer used.
        /// The texture references in MOMT are file data ids directly.
        /// As of that version, there is a fallback mode though and some files still use MOTX for sake of avoiding re-export.
        /// To check if texture references in MOMT are file data ids, simply check if MOTX exist in file 
        /// </summary>
        [ChunkOrder(3), ChunkOptional]
        public MOTX Textures { get; set; }

        /// <summary>
        /// Gets or sets the materials.
        /// </summary>
        [ChunkOrder(4)]
        public MOMT Materials { get; set; }

        /// <summary>
        /// List of file ids for M2 (mdx) models that appear in this WMO.
        /// </summary>
        [ChunkOrder(5), ChunkOptional]
        public MODI DoodadFileId { get; set; }

        /// <summary>
        /// Scale values for doodad entries.
        /// </summary>
        [ChunkOrder(6), ChunkOptional]
        public MDDI DoodadScale { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Legion.WorldMapObjectRoot"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectRoot(byte[] inData) : base(inData)
        {
        }
    }
}
