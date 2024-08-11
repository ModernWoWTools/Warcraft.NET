using System;
using System.Collections.Generic;
using System.Text;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.SKIN
{
    public struct M2SkinSection
    {
        public UInt16 skinSectionId;        // Mesh part ID, see below.
        public UInt16 Level;                // (level << 16) is added (|ed) to startTriangle and alike to avoid having to increase those fields to uint32s.
        public UInt16 vertexStart;          // Starting vertex number.
        public UInt16 vertexCount;          // Number of vertices.
        public UInt16 indexStart;           // Starting triangle index (that's 3* the number of triangles drawn so far).
        public UInt16 indexCount;           // Number of triangle indices.
        public UInt16 boneCount;            // Number of elements in the bone lookup table. Max seems to be 256 in Wrath. Shall be ≠ 0.
        public UInt16 boneComboIndex;       // Starting index in the bone lookup table.
        public UInt16 boneInfluences;       // <= 4
                                            // from <=BC documentation: Highest number of bones needed at one time in this Submesh --Tinyn (wowdev.org) 
                                            // In 2.x this is the amount of of bones up the parent-chain affecting the submesh --NaK
                                            // Highest number of bones referenced by a vertex of this submesh. 3.3.5a and suspectedly all other client revisions. -- Skarn
        public UInt16 centerBoneIndex;
        public C3Vector centerPosition;     // Average position of all the vertices in the sub mesh.
        //≥BC
        public C3Vector sortCenterPosition; // The center of the box when an axis aligned box is built around the vertices in the submesh.
        public float sortRadius;            // Distance of the vertex farthest from CenterBoundingBox.
    }

    public struct BoneStruct
    {
        public byte b1;
        public byte b2;
        public byte b3;
        public byte b4;
    }

    public struct M2Batch
    {
        public byte flags;                       // Usually 16 for static textures, and 0 for animated textures. &0x1: materials invert something; &0x2: transform &0x4: projected texture; &0x10: something batch compatible; &0x20: projected texture?; &0x40: possibly don't multiply transparency by texture weight transparency to get final transparency value(?)
        public sbyte priorityPlane;
        public UInt16 shader_id;                  // See below.
        public UInt16 skinSectionIndex;           // A duplicate entry of a submesh from the list above.
        public UInt16 geosetIndex;                // See below. New name: flags2. 0x2 - projected. 0x8 - EDGF chunk in m2 is mandatory and data from is applied to this mesh
        public UInt16 colorIndex;                 // A Color out of the Colors-Block or -1 if none.
        public UInt16 materialIndex;              // The renderflags used on this texture-unit.
        public UInt16 materialLayer;              // Capped at 7 (see CM2Scene::BeginDraw)
        public UInt16 textureCount;               // 1 to 4. See below. Also seems to be the number of textures to load, starting at the texture lookup in the next field (0x10).
        public UInt16 textureComboIndex;          // Index into Texture lookup table
        public UInt16 textureCoordComboIndex;     // Index into the texture mapping lookup table.
        public UInt16 textureWeightComboIndex;    // Index into transparency lookup table.
        public UInt16 textureTransformComboIndex; // Index into uvanimation lookup table. 
    }

    public struct M2ShadowBatch
    {
        byte flags;              // if auto-generated: M2Batch.flags & 0xFF
        byte flags2;             // if auto-generated: (renderFlag[i].flags & 0x04 ? 0x01 : 0x00)
                                 //                  | (!renderFlag[i].blendingmode ? 0x02 : 0x00)
                                 //                  | (renderFlag[i].flags & 0x80 ? 0x04 : 0x00)
                                 //                  | (renderFlag[i].flags & 0x400 ? 0x06 : 0x00)
        public UInt16 _unknown1;
        public UInt16 submesh_id;
        public UInt16 texture_id;        // already looked-up
        public UInt16 color_id;
        public UInt16 transparency_id;   // already looked-up
    }

    public struct M2Triangle //Left handed
    {
        public ushort V1;
        public ushort V3;
        public ushort V2;
    }
}
