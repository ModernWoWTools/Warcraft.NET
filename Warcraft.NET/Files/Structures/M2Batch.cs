namespace Warcraft.NET.Files.Structures
{
    public struct M2Batch
    {
        /// <summary>
        /// Texture Flags </para> 
        /// Usually 16 for static textures, and 0 for animated textures. &0x1: materials invert something; &0x2: transform &0x4: projected texture; &0x10: something batch compatible; &0x20: projected texture?; &0x40: possibly don't multiply transparency by texture weight transparency to get final transparency value(?)
        /// </summary>
        public byte Flags;

        /// <summary>
        /// Priority Plane
        /// </summary>
        public sbyte PriorityPlane;

        /// <summary>
        /// ShaderID </para> 
        /// negative = direct selection </para> 
        /// positive = selection trough a function. See wiki for more information
        /// </summary>
        public ushort ShaderId;

        /// <summary>
        /// A duplicate entry of a submesh from the list above.
        /// </summary>
        public ushort SkinSectionIndex;

        /// <summary>
        /// New name: flags2. 0x2 - projected. 0x8 - EDGF chunk in m2 is mandatory and data from is applied to this mesh
        /// </summary>
        public ushort GeosetIndex;

        /// <summary>
        /// A Color out of the Colors-Block or -1 if none.
        /// </summary>
        public ushort ColorIndex;

        /// <summary>
        /// The renderflags used on this texture-unit. Index into M2 Materials
        /// </summary>
        public ushort MaterialIndex;

        /// <summary>
        /// Capped at 7 (CM2Scene::BeginDraw)
        /// </summary>
        public ushort MaterialLayer;

        /// <summary>
        /// 1 to 4. See below. Also seems to be the number of textures to load, starting at the texture lookup in the next field (0x10).
        /// </summary>
        public ushort TextureCount;

        /// <summary>
        /// Index into Texture lookup table
        /// </summary>
        public ushort TextureComboIndex;

        /// <summary>
        /// Index into the texture mapping lookup table.
        /// </summary>
        public ushort TextureCoordComboIndex;

        /// <summary>
        /// Index into transparency lookup table.
        /// </summary>
        public ushort TextureWeightComboIndex;

        /// <summary>
        /// Index into uvanimation lookup table. 
        /// </summary>
        public ushort TextureTransformComboIndex;
    }
}
