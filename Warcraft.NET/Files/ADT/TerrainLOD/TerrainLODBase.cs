using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Chunks;

namespace Warcraft.NET.Files.ADT.TerrainLOD
{
    public abstract class TerrainLODBase : ChunkedFile
    {
        [ChunkOrder(1)]
        public MVER Version { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainLODBase"/> class.
        /// </summary>
        public TerrainLODBase() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainLODBase"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainLODBase(byte[] inData) : base(inData)
        {
        }

        /// <inheritdoc/>
        public abstract byte[] Serialize();
    }
}
