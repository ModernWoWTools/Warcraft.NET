using Warcraft.NET.Attribute;
using Warcraft.NET.Files.M2.Chunks;
using Warcraft.NET.Files.M2.Chunks.BfA;
using Warcraft.NET.Files.M2.Chunks.DF;
using Warcraft.NET.Files.M2.Chunks.Legion;
using Warcraft.NET.Files.M2.Chunks.SL;

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
        [ChunkOptional]
        public PFID PhysFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model skin file ids
        /// </summary>
        [ChunkOptional]
        public SFID SkinFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model anim file ids
        /// </summary>
        [ChunkOptional]
        public AFID AnimFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model bone file ids
        /// </summary>
        [ChunkOptional]
        public BFID BoneFileIds { get; set; }

        [ChunkOptional]
        public TXAC CParticleEmitter2 { get; set; }
        
        [ChunkOptional]
        public EXPT ExtendedParticle { get; set; }

        //[ChunkOptional]
        //public EXP2 ExtendedParticle2 { get; set; }

        [ChunkOptional]
        public PABC M2InitBlacklistAnimData { get; set; }//Unfinished
        [ChunkOptional]
        public PADC M2InitParentAnimData { get; set; } //Unfinished
        [ChunkOptional]
        public PSBC M2InitParentSequencyBoundsData { get; set; }//Unfinished
        [ChunkOptional]
        public PEDC M2InitParentEventData { get; set; }//Unfinished

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

        [ChunkOptional]
        public LDV1 LodDataVersion1 { get; set; }

        /// <summary>
        /// Gets or sets the model recursive particle file ids
        /// </summary>
        [ChunkOptional]
        public RPID RecursiveParticleFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model geometry particle file ids
        /// </summary>
        [ChunkOptional]
        public GPID GeometryParticleFileIds { get; set; }

        [ChunkOptional]
        public WFV1 WaterFallVersion1 { get; set; }

        [ChunkOptional]
        public WFV2 WaterFallVersion2 { get; set; }

        [ChunkOptional]
        public PGD1 ParticleGeosetData { get; set; }

        [ChunkOptional]
        public WFV3 WaterFallVersion3 { get; set; }

        /// <summary>
        /// Gets or sets the model physics
        /// </summary>
        [ChunkOptional]
        public PFDC ModelPhysics { get; set; }

        [ChunkOptional]
        public EDGF EdgeFade { get; set; }

        [ChunkOptional]
        public NERF NERF { get; set; }

        [ChunkOptional]
        public DETL DETL { get; set; }

        [ChunkOptional]
        public DBOC DBOC { get; set; }

        [ChunkOptional]
        public AFRA AFRA { get; set; }















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
