using System;
using System.Collections.Generic;
using System.Text;

namespace Warcraft.NET.Files.Structures
{
    public struct M2Batch
    {
        public byte Flags;                        // Usually 16 for static textures, and 0 for animated textures. &0x1: materials invert something; &0x2: transform &0x4: projected texture; &0x10: something batch compatible; &0x20: projected texture?; &0x40: possibly don't multiply transparency by texture weight transparency to get final transparency value(?)
        public sbyte PriorityPlane;
        public ushort ShaderId;                   // See below.
        public ushort SkinSectionIndex;           // A duplicate entry of a submesh from the list above.
        public ushort GeosetIndex;                // See below. New name: flags2. 0x2 - projected. 0x8 - EDGF chunk in m2 is mandatory and data from is applied to this mesh
        public ushort ColorIndex;                 // A Color out of the Colors-Block or -1 if none.
        public ushort MaterialIndex;              // The renderflags used on this texture-unit.
        public ushort MaterialLayer;              // Capped at 7 (see CM2Scene::BeginDraw)
        public ushort TextureCount;               // 1 to 4. See below. Also seems to be the number of textures to load, starting at the texture lookup in the next field (0x10).
        public ushort TextureComboIndex;          // Index into Texture lookup table
        public ushort TextureCoordComboIndex;     // Index into the texture mapping lookup table.
        public ushort TextureWeightComboIndex;    // Index into transparency lookup table.
        public ushort TextureTransformComboIndex; // Index into uvanimation lookup table. 
    }
}
