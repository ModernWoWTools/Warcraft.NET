using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.M2.Entries
{
    public class WFV3Entry
    {
        public float bumpScale;  //Passed to vertex shader
        public float value0_x;
        public float value0_y;
        public float value0_z;
        public float value1_w;
        public float value0_w;
        public float value1_x;
        public float value1_y;
        public float value2_w;
        public float value3_y;
        public float value3_x;
        public CImVector baseColor; // in rgba (not bgra)
        public UInt16 flags;
        public UInt16 unk0;
        public float value3_w;
        public float value3_z;
        public float value4_y;
        public float unk1;
        public float unk2;
        public float unk3;
        public float unk4;

        /// <summary>
        /// Initializes a new instance of the <see cref="WFV3Entry"/> class.
        /// </summary>
        public WFV3Entry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WFV3Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public WFV3Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                bumpScale = br.ReadSingle();

                value0_x = br.ReadSingle();
                value0_y = br.ReadSingle();
                value0_z = br.ReadSingle();
                value1_w = br.ReadSingle();

                value0_w = br.ReadSingle();
                value1_x = br.ReadSingle();
                value1_y = br.ReadSingle();
                value2_w = br.ReadSingle();

                value3_y = br.ReadSingle();
                value3_x = br.ReadSingle();

                baseColor = new CImVector(br.ReadBytes(4));

                flags = br.ReadUInt16();
                unk0 = br.ReadUInt16();

                value3_w = br.ReadSingle();
                value3_z = br.ReadSingle();
                value4_y = br.ReadSingle();

                unk1 = br.ReadSingle();
                unk2 = br.ReadSingle();
                unk3 = br.ReadSingle();
                unk4 = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a WFV3 Entry
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 80;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                { 
                    bw.Write(bumpScale);

                bw.Write(value0_x);
                bw.Write(value0_y);
                bw.Write(value0_z);
                bw.Write(value1_w);

                bw.Write(value0_w);
                bw.Write(value1_x);
                bw.Write(value1_y);
                bw.Write(value2_w);

                bw.Write(value3_y);
                bw.Write(value3_x);

                bw.Write(baseColor.toBytes());

                bw.Write(flags);
                bw.Write(unk0);

                bw.Write(value3_w);
                bw.Write(value3_z);
                bw.Write(value4_y);

                bw.Write(unk1);
                bw.Write(unk2);
                bw.Write(unk3);
                bw.Write(unk4);
            }

                return ms.ToArray();
            }
        }
    }
}
