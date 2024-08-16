using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Warcraft.NET.Files.Structures
{
    public struct M2SkinSection
    {
        public ushort SkinSectionId;        // Mesh part ID, see below.
        public ushort Level;                // (level << 16) is added (|ed) to startTriangle and alike to avoid having to increase those fields to uint32s.
        public ushort VertexStart;          // Starting vertex number.
        public ushort VertexCount;          // Number of vertices.
        public ushort IndexStart;           // Starting triangle index (that's 3* the number of triangles drawn so far).
        public ushort IndexCount;           // Number of triangle indices.
        public ushort BoneCount;            // Number of elements in the bone lookup table. Max seems to be 256 in Wrath. Shall be ≠ 0.
        public ushort BoneComboIndex;       // Starting index in the bone lookup table.
        public ushort BoneInfluences;       // <= 4
                                            // from <=BC documentation: Highest number of bones needed at one time in this Submesh --Tinyn (wowdev.org) 
                                            // In 2.x this is the amount of of bones up the parent-chain affecting the submesh --NaK
                                            // Highest number of bones referenced by a vertex of this submesh. 3.3.5a and suspectedly all other client revisions. -- Skarn
        public ushort CenterBoneIndex;
        public Vector3 CenterPosition;     // Average position of all the vertices in the sub mesh.
        //≥BC
        public Vector3 SortCenterPosition; // The center of the box when an axis aligned box is built around the vertices in the submesh.
        public float SortRadius;            // Distance of the vertex farthest from CenterBoundingBox.
    }
}
