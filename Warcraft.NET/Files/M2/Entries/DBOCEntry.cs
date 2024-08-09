using System;
using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class DBOCEntry
    {
        public float unk_float1;
        public float unk_float2;
        public UInt32 unk_int1;
        public UInt32 unk_int2;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBOCEntry"/> class.
        /// </summary>
        public DBOCEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DBOCEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public DBOCEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                unk_float1 = br.ReadSingle();
                unk_float2 = br.ReadSingle();
                unk_int1 = br.ReadUInt32();
                unk_int2 = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of a DBOC Entry
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 16;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(unk_float1);
                    bw.Write(unk_float2);
                    bw.Write(unk_int1);
                    bw.Write(unk_int2);
                }

                return ms.ToArray();
            }
        }
    }
}
