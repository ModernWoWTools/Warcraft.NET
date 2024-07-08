using System.IO;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MapChunk.Entrys
{
    /// <summary>
    /// Texture layer entry, representing a ground texture in the chunk.
    /// </summary>
    public class MCLYEntry
    {
        /// <summary>
        /// Gets or sets index of the texture in the MTEX chunk.
        /// </summary>
        public uint TextureID { get; set; }

        /// <summary>
        /// Gets or sets flags for the texture. Used for animation data.
        /// </summary>
        public Flags.MCLYFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the offset into MCAL where the alpha map begins.
        /// </summary>
        public uint AlphaMapOffset { get; set; }

        /// <summary>
        /// Gets or sets the ground effect ID. This is a foreign key entry into GroundEffectTexture::ID.
        /// </summary>
        public uint EffectID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCLYEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MCLYEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    TextureID = br.ReadUInt32();
                    Flags = (Flags.MCLYFlags)br.ReadUInt32();
                    AlphaMapOffset = br.ReadUInt32();
                    EffectID = br.ReadUInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 16;
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(TextureID);
                bw.Write((uint)Flags);
                bw.Write(AlphaMapOffset);
                bw.Write(EffectID);

                return ms.ToArray();
            }
        }
    }
}
