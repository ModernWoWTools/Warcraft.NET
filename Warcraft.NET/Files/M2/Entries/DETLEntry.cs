using System;
using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class DETLEntry
    {
        public UInt16 flags;
        public UInt16 packedFloat0;
        public UInt16 packedFloat1; // multiplier for M2Light.diffuse_color
        public UInt16 unk0;
        public UInt32 unk1;

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
                flags = br.ReadUInt16();
                packedFloat0 = br.ReadUInt16();
                packedFloat1 = br.ReadUInt16();
                unk0 = br.ReadUInt16();
                unk1 = br.ReadUInt32();
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
                    bw.Write(flags);
                    bw.Write(packedFloat0);
                    bw.Write(packedFloat1);
                    bw.Write(unk0);
                    bw.Write(unk1);
                }

                return ms.ToArray();
            }
        }
    }
}
