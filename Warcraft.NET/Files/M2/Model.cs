using Warcraft.NET.Attribute;
using Warcraft.NET.Files.M2.Chunks;
using Warcraft.NET.Files.M2.Chunks.BfA;
using Warcraft.NET.Files.M2.Chunks.Legion;

namespace Warcraft.NET.Files.M2
{
    [AutoDocFile("m2")]
    public class Model : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the model information
        /// </summary>
        [ChunkOrder(1)]
        public MD21 ModelInformation { get; set; }

        /// <summary>
        /// Gets or sets the model phys file ids
        /// </summary>
        [ChunkOrder(2), ChunkOptional]
        public PFID PhysFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model skin file ids
        /// </summary>
        [ChunkOrder(3), ChunkOptional]
        public SFID SkinFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model anim file ids
        /// </summary>
        [ChunkOrder(4), ChunkOptional]
        public AFID AnimFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model bone file ids
        /// </summary>
        [ChunkOrder(5), ChunkOptional]
        public BFID BoneFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model recursive particle file ids
        /// </summary>
        [ChunkOrder(6), ChunkOptional]
        public RPID RecursiveParticleFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model geometry particle file ids
        /// </summary>
        [ChunkOrder(7), ChunkOptional]
        public GPID GeometryParticleFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model skeleton file ids
        /// </summary>
        [ChunkOrder(8), ChunkOptional]
        public SKID SkeletonFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model texture file ids
        /// </summary>
        [ChunkOrder(9), ChunkOptional]
        public TXID TextureFileIds { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public Model(byte[] inData) : base(inData)
        {
        }

        public override bool IsReverseSignature()
        {
            return false;
        }
    }
}
