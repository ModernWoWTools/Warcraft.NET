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

<<<<<<< Updated upstream
=======
        /// <summary>
        /// Gets or sets the TXAC
        /// </summary>
        [ChunkOptional]
        public TXAC CParticleEmitter2 { get; set; }

        /// <summary>
        /// Gets or sets the particle extension
        /// </summary>
        [ChunkOptional]
        public EXPT ExtendedParticle { get; set; }

        /// <summary>
        /// Gets or sets the BlacklistAnimData
        /// </summary>
        [ChunkOptional]
        public PABC M2InitBlacklistAnimData { get; set; }

        /// <summary>
        /// Gets or sets the ParentAnimData
        /// </summary>
        [ChunkOptional]
        public PADC M2InitParentAnimData { get; set; }

        /// <summary>
        /// Gets or sets the ParentSequenceBoundsData
        /// </summary>
        [ChunkOptional]
        public PSBC M2InitParentSequenceBoundsData { get; set; }

        /// <summary>
        /// Gets or sets the ParentEventData
        /// </summary>
        [ChunkOptional]
        public PEDC M2InitParentEventData { get; set; }

        /// <summary>
        /// Gets or sets the model skeleton file ids
        /// </summary>
        [ChunkOptional]
        public SKID SkeletonFileIds { get; set; }

        /// <summary>
        /// Gets or sets the texture file ids
        /// </summary>
        [ChunkOptional]
        public TXID TextureFileIds { get; set; }

        /// <summary>
        /// Gets or sets the LodDataVersion1
        /// </summary>
        [ChunkOptional]
        public LDV1 LodDataVersion1 { get; set; }

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        /// Gets or sets the model skeleton file ids
        /// </summary>
        [ChunkOrder(8), ChunkOptional]
        public SKID SkeletonFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model texture file ids
        /// </summary>
        [ChunkOrder(9), ChunkOptional]
        public TXID TextureFileIds { get; set; }
=======
        /// Gets or sets the WaterFallVersion1
        /// </summary>
        [ChunkOptional]
        public WFV1 WaterFallVersion1 { get; set; }

        /// <summary>
        /// Gets or sets the WaterFallVersion2
        /// </summary>
        [ChunkOptional]
        public WFV2 WaterFallVersion2 { get; set; }

        /// <summary>
        /// Gets or sets the ParticleGeosetData
        /// </summary>
        [ChunkOptional]
        public PGD1 ParticleGeosetData { get; set; }

        /// <summary>
        /// Gets or sets the WaterFallVersion3
        /// </summary>
        [ChunkOptional]
        public WFV3 WaterFallVersion3 { get; set; }

        /// <summary>
        /// Gets or sets the Physics of the M2
        /// </summary>
        [ChunkOptional]
        public PFDC ModelPhysics { get; set; }

        /// <summary>
        /// Gets or sets the EdgeFade
        /// </summary>
        [ChunkOptional]
        public EDGF EdgeFade { get; set; }

        /// <summary>
        /// Gets or sets the NERF
        /// </summary>
        [ChunkOptional]
        public NERF NERF { get; set; }

        /// <summary>
        /// Gets or sets the DETL (light related)
        /// </summary>
        [ChunkOptional]
        public DETL DETL { get; set; }

        /// <summary>
        /// Gets or sets the DBOC
        /// </summary>
        [ChunkOptional]
        public DBOC DBOC { get; set; }

        /// <summary>
        /// Gets or sets the AFRA
        /// </summary>
        [ChunkOptional]
        public AFRA AFRA { get; set; }
>>>>>>> Stashed changes

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
