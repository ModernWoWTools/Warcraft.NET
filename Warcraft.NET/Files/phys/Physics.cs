using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Phys.Chunks;

namespace Warcraft.NET.Files.Phys
{
    [AutoDocFile("phys")]
    public class Physics : ChunkedFile
    {
        /// <summary>
        /// contains the version of the physics
        /// </summary>
        [ChunkOrder(1)]
        public PHYS Version{get; set;}

        /// <summary>
        /// contains an unknown uint
        /// </summary>
        [ChunkOrder(2),ChunkOptional]
        public PHYT Phyt{get; set; }

        /// <summary>
        /// contains the used box shapes
        /// </summary>
        [ChunkOrder(3),ChunkOptional]
        public BOXS BoxShapes{get;set;}
      
        /// <summary>
        /// contains the used sphere shapes
        /// </summary>
        [ChunkOrder(4),ChunkOptional]
        public SPHS SphereShapes{get;set;}

        /// <summary>
        /// contains the used capsule shapes
        /// </summary>

        [ChunkOrder(5),ChunkOptional]
        public CAPS CapsuleShapes{get;set;}  
        
        /// <summary>
        /// contains the used polytope shapes
        /// </summary>
        [ChunkOrder(6),ChunkOptional]
        public PLYT PolytopeShapes{get; set;}

        /// <summary>
        /// contains the used shapes
        /// </summary>
        [ChunkOrder(7),ChunkOptional]
        public SHP2 Shapes2{get; set;} //SHAP nyi

        /// <summary>
        /// contains the used rigidbodies
        /// </summary>
        [ChunkOrder(8),ChunkOptional]
        public BDY4 Rigidbodies4{get; set;} //BODY,BDY2,BDY3 NYI

        /// <summary>
        /// contains the used shoulder joints
        /// </summary>
        [ChunkOrder(9),ChunkOptional]
        public SHOJ ShoulderJoints { get; set; } 

        /// <summary>
        /// contains the used shoulder joints
        /// </summary>
        [ChunkOrder(10),ChunkOptional]
        public SHJ2 ShoulderJoints2{get; set;}

        /// <summary>
        /// contains the used weld joints
        /// </summary>
        [ChunkOrder(11),ChunkOptional]
        public WLJ3 WeldJoints3{get; set;} //WELJ,WLJ2 nyi

        /// <summary>
        /// contains the used spherical joints
        /// </summary>
        [ChunkOrder(12),ChunkOptional]
        public SPHJ SphericalJoints{get; set;}

        /// <summary>
        /// contains the used prismatic joints
        /// </summary>
        [ChunkOrder(13),ChunkOptional]
        public PRS2 PrismaticJoints2{get; set;} //PRSJ nyi

        /// <summary>
        /// contains the used revolute joints
        /// </summary>
        [ChunkOrder(14),ChunkOptional]
        public REV2 RevoluteJoints2{get; set;} //REVJ nyi

        /// <summary>
        /// contains the used distance joints
        /// </summary>
        [ChunkOrder(15),ChunkOptional]
        public DSTJ DistanceJoints{get; set;}

        /// <summary>
        /// contains the used joints
        /// </summary>
        [ChunkOrder(16),ChunkOptional]
        public JOIN Joints{get; set;}

        /// <summary>
        /// contains the used phyv data (unknown)
        /// </summary>
        [ChunkOrder(17),ChunkOptional]
        public PHYV Phyv { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Physics"/> class.
        /// </summary>
        public Physics()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Physics"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public Physics(byte[] inData) : base(inData)
        {

        }
    }
}