using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Phys.Chunks;

namespace Warcraft.NET.Files.Phys
{
    [AutoDocFile("phys")]
    public class Physics : ChunkedFile
    {
        [ChunkOrder(1)]
        public PHYS Version{get; set;}

        [ChunkOptional]
        public PHYT Phyt{get; set; } //unknown what it stands for

        [ChunkOptional]
        public BOXS BoxShapes{get;set;}
      
        [ChunkOptional]
        public SPHS SphereShapes{get;set;}

        [ChunkOptional]
        public CAPS CapsuleShapes{get;set;}  
        
        [ChunkOptional]
        public PLYT PolytopeShapes{get; set;}

        [ChunkOptional]
        public SHP2 Shapes2{get; set;} //SHAP nyi

        [ChunkOptional]
        public BDY4 Rigidbodies4{get; set;} //BODY,BDY2,BDY3 NYI

        [ChunkOptional]
        public SHOJ ShoulderJoints { get; set; } 

        [ChunkOptional]
        public SHJ2 ShoulderJoints2{get; set;}

        [ChunkOptional]
        public WLJ3 WeldJoints3{get; set;} //WELJ,WLJ2 nyi

        [ChunkOptional]
        public SPHJ SphericalJoints{get; set;}

        [ChunkOptional]
        public PRS2 PrismaticJoints2{get; set;} //PRSJ nyi

        [ChunkOptional]
        public REV2 RevoluteJoints2{get; set;} //REVJ nyi

        [ChunkOptional]
        public DSTJ DistanceJoints{get; set;}     
        
        [ChunkOptional]
        public JOIN Joints{get; set;}

        [ChunkOptional]
        public PHYV Phyv { get; set; } //unknown what it stands for

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