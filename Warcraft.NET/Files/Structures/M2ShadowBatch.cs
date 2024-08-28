namespace Warcraft.NET.Files.Structures
{
    public struct M2ShadowBatch
    {        
        /// <summary>
        /// if auto-generated: M2Batch.Flags & 0xFF
        /// </summary>
        public byte Flags;

        /// <summary>
        /// gets auto generated if certain flags in TextureUnit are set
        /// See wiki for more information
        /// </summary>
        public byte Flags2;             

        /// <summary>
        /// unknown field
        /// </summary>
        public ushort Unk1;

        /// <summary>
        /// same as TextureUnit
        /// </summary>
        public ushort SubmeshId;

        /// <summary>
        /// same as TextureUnit
        /// </summary>
        public ushort TextureId;        // already looked-up

        /// <summary>
        /// same as TextureUnit
        /// </summary>
        public ushort ColorId;

        /// <summary>
        /// same as TextureUnit
        /// </summary>
        public ushort TransparencyId;   // already looked-up
    }
}
