using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Warcraft.NET.Files.Structures
{
    public struct M2SkinSection
    {
        /// <summary>
        /// Mesh part ID, see wiki for exact calculation
        /// </summary>
        public ushort SkinSectionId;

        /// <summary>
        /// (level << 16) is added (|ed) to startTriangle and alike to avoid having to increase those fields to uint32s.
        /// </summary>
        public ushort Level;

        /// <summary>
        /// Starting Vertex. Index into local vertex list
        /// </summary>
        public ushort VertexStart;

        /// <summary>
        /// Number of Vertices taken from local vertex list
        /// </summary>
        public ushort VertexCount;

        /// <summary>
        /// Starting triangle index.
        /// </summary>
        public ushort IndexStart;

        /// <summary>
        /// Number of triangle indices.        
        /// </summary>
        public ushort IndexCount;

        /// <summary>
        /// Number of elements in the bone lookup table. Max seems to be 256 in Wrath. Shall be ≠ 0.    
        /// </summary>
        public ushort BoneCount;

        /// <summary>
        /// Starting index in the bone lookup table.
        /// </summary>
        public ushort BoneComboIndex;

        /// <summary>
        /// <= 4
        /// from <=BC documentation: Highest number of bones needed at one time in this Submesh --Tinyn (wowdev.org)
        /// In 2.x this is the amount of of bones up the parent-chain affecting the submesh --NaK
        /// Highest number of bones referenced by a vertex of this submesh. 3.3.5a and suspectedly all other client revisions. -- Skarn
        /// </summary>
        public ushort BoneInfluences;

        /// <summary>
        /// Index of the center bone
        /// </summary>
        public ushort CenterBoneIndex;

        /// <summary>
        /// Average position of all the vertices in the sub mesh.
        /// </summary>
        public Vector3 CenterPosition;

        //≥BC
        /// <summary>
        /// The center of the box when an axis aligned box is built around the vertices in the submesh.
        /// </summary>
        public Vector3 SortCenterPosition;

        /// <summary>
        /// Distance of the vertex farthest from CenterBoundingBox.
        /// </summary>
        public float SortRadius;
    }
}
