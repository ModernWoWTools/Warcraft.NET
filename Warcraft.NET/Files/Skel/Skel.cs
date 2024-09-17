using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Skel.Chunks;
using Warcraft.NET.Files.M2.Chunks.Legion;

namespace Warcraft.NET.Files.Skel
{
    public class Skel : ChunkedFile
    {
        /// <summary>
        /// contains the name and unknown data of the skeleton
        /// </summary>
        [ChunkOrder(1)]
        public SKL1 Skeleton { get; set; }

        /// <summary>
        /// contains the animations of the skeleton
        /// </summary>
        [ChunkOptional]
        public SKS1 Animations { get; set; }

        /// <summary>
        /// contains the attachments of the skeleton
        /// </summary>
        [ChunkOptional]
        public SKA1 Attachments { get; set; }

        /// <summary>
        /// contains the bones of the skeleton
        /// </summary>
        [ChunkOptional]
        public SKB1 Bones { get; set; }
        
        /// <summary>
        /// contains the animation file ids of the skeleton
        /// </summary>
        [ChunkOptional]
        public AFID AnimationFileIDs { get; set; }

        /// <summary>
        /// contains the animations of the skeleton
        /// </summary>
        [ChunkOptional]
        public SKPD ParentSkelFileID { get; set; }

        /// <summary>
        /// contains the bone file ids of the skeleton
        /// </summary>
        [ChunkOptional]
        public BFID BoneFileIDs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Skel"/> class.
        /// </summary>
        public Skel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Skel"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public Skel(byte[] inData) : base(inData)
        {

        }
        public override bool IsReverseSignature()
        {
            return false;
        }

    }
}


