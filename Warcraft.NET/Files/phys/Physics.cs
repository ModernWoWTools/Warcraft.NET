using Warcraft.NET.Attribute;
using Warcraft.NET.Files.phys.Chunks;

namespace Warcraft.NET.Files.phys
{
    [AutoDocFile("phys")]
    public class Physics : ChunkedFile
    {
        [ChunkOrder(1)]
        public PHYS phys{get; set;}

        [ChunkOrder(2)]
        public PHYT phyt{get; set;}

       [ChunkOptional]
        public BOXS boxs{get;set;}
      
        [ChunkOptional]
        public SPHS sphs{get;set;}

        [ChunkOptional]
        public CAPS caps{get;set;}  
        
        [ChunkOptional]
        public PLYT plyt{get; set;}

        [ChunkOptional]
        public SHP2 shp2{get; set;}

        [ChunkOptional]
        public BDY4 bdy4{get; set;}



 


        [ChunkOptional]
        public SHOJ shoj { get; set; }

        [ChunkOptional]
        public SHJ2 shj2{get; set;}



        [ChunkOptional]
        public WLJ3 wlj3{get; set;}

        [ChunkOptional]
        public SPHJ sphj{get; set;}




        [ChunkOptional]
        public PRS2 prs2{get; set;}

        [ChunkOptional]
        public REV2 rev2{get; set;}

        [ChunkOptional]
        public DSTJ dstj{get; set;}        
        
        [ChunkOptional]
        public JOIN join{get; set;}

        [ChunkOptional]
        public PHYV phyv { get; set; }

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