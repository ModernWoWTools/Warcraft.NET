using System.IO;
using System.Linq;
using System.Reflection;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.WMO.Chunks;
using MOHD = Warcraft.NET.Files.WMO.Chunks.Legion.MOHD;
using MOMT = Warcraft.NET.Files.WMO.Chunks.Wotlk.MOMT;

namespace Warcraft.NET.Files.WMO.WorldMapObject.Legion
{
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
        /// Initializes a new instance of the <see cref="Legion.WorldMapObjectRoot"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldMapObjectRoot(byte[] inData) : base(inData)
        {
        }
    }
}
