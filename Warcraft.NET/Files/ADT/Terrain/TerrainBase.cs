using Warcraft.NET.Attribute;
using Warcraft.NET.Exceptions;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.ADT.Chunks;
using Warcraft.NET.Files.Interfaces;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Warcraft.NET.Files.ADT.Terrain
{
    public abstract class TerrainBase : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the contains the ADT version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; }

        /// <summary>
        /// Gets or sets the contains the ADT Header with offsets. The header has offsets to the other chunks in the
        /// ADT.
        /// </summary>
        [ChunkOrder(2)]
        public MHDR Header { get; set; }

        /// <summary>
        /// Gets or sets the contains an array of offsets where MCNKs are in the file.
        /// </summary>
        //public MCNK Chunk { get; set; }

        /// <summary>
        /// Gets or sets the contains the ADT bounding box
        /// ADT.
        /// </summary>
        [ChunkOrder(99), ChunkOptional]
        public MFBO BoundingBox { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainBase(byte[] inData) : base(inData) 
        {
        }
    }
}
