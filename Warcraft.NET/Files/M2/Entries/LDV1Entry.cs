using System;
using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class LDV1Entry
    {
        public UInt16 unk0;
        public UInt16 lodCount; //maxLod = lodCount-1;  
        float unk2_f;
        public byte[] particleBoneLod; //lod serves as indes into this array
        public UInt32 unk4;


        /// <summary>
        /// Initializes a new instance of the <see cref="LDV1Entry"/> class.
        /// </summary>
        public LDV1Entry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LDV1Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public LDV1Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                unk0 = br.ReadUInt16();
                lodCount = br.ReadUInt16();
                unk2_f = br.ReadSingle();
                particleBoneLod = br.ReadBytes(4);
                unk4 = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of a LDV1 Entry
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
                    bw.Write(unk0);
                    bw.Write(lodCount);
                    bw.Write(unk2_f);
                    bw.Write(particleBoneLod);
                    bw.Write(unk4);
                }

                return ms.ToArray();
            }
        }
    }
}
