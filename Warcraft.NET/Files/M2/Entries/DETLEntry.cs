using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class DETLEntry
    {
        /// <summary>
        /// unknown Flags 
        /// </summary>
        public ushort Flags;

        /// <summary>
        /// multiplier for M2Light.diffuse_color
        /// </summary>
        public ushort PackedFloat0;

        /// <summary>
        /// multiplier for M2Light.diffuse_color
        /// </summary>
        public ushort PackedFloat1;

        /// <summary>
        /// unknown field
        /// </summary>
        public ushort Unk0;

        /// <summary>
        /// unknown field
        /// </summary>
        public uint Unk1;

        /// <summary>
        /// Initializes a new instance of the <see cref="DETLEntry"/> class.
        /// </summary>
        public DETLEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DETLEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public DETLEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                Flags = br.ReadUInt16();
                PackedFloat0 = br.ReadUInt16();
                PackedFloat1 = br.ReadUInt16();
                Unk0 = br.ReadUInt16();
                Unk1 = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of a AFRA Entry
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 12;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(Flags);
                    bw.Write(PackedFloat0);
                    bw.Write(PackedFloat1);
                    bw.Write(Unk0);
                    bw.Write(Unk1);
                }
                return ms.ToArray();
            }
        }
    }
}