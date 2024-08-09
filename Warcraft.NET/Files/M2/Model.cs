using Warcraft.NET.Attribute;
using Warcraft.NET.Files.M2.Chunks;
using Warcraft.NET.Files.M2.Chunks.BfA;
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
        
        [ChunkOptional]
        public TXAC TXAC { get; set; }
        
        //[ChunkOptional]  
        //public PEDC PEDC { get; set; }
        
        
        [ChunkOptional]
        public EXPT EXP2 { get; set; }

        [ChunkOptional]
        public PGD1 PGD1 { get; set; } 
        
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
        public LDV1 LDV1 { get; set; }

        [ChunkOptional]
        public EDGF EDGF { get; set; }

        /// <summary>
        /// Gets or sets the model physics
        /// </summary>
        [ChunkOptional]
        public PFDC ModelPhysics { get; set; }

        [ChunkOptional]
        public DBOC DBOC { get; set; }

        /// <summary>
        /// Gets or sets the model skeleton file ids
        /// </summary>
        [ChunkOptional]
        public SKID SkeletonFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model skin file ids
        /// </summary>
        [ChunkOptional]
        public SFID SkinFileIds { get; set; }

        /// <summary>
        /// Gets or sets the model phys file ids
        /// </summary>
        [ChunkOptional]
        public PFID PhysFileIds { get; set; }

        /// <summary>
        /// Gets or sets the texture file ids
        /// </summary>
        [ChunkOptional]
        public TXID TextureFileIds { get; set; }


        //[ChunkOptional]
        //public PSBC PSBC { get; set; }



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
