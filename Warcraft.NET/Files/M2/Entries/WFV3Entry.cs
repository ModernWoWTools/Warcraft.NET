using System;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.M2.Entries
{
    public class WFV3Entry
    {
        /// <summary>
        /// BumpScale -> Passed to vertex shader
        /// </summary>
        public float BumpScale;

        /// <summary>
        /// value 0 x
        /// </summary>
        public float Value0X;

        /// <summary>
        /// value 0 y
        /// </summary>
        public float Value0Y;

        /// <summary>
        /// value 0 z
        /// </summary>
        public float Value0Z;

        /// <summary>
        /// value 1 W
        /// </summary>
        public float Value1W;

        /// <summary>
        /// value 0 W
        /// </summary>
        public float Value0W;

        /// <summary>
        /// value 1 x
        /// </summary>
        public float Value1X;

        /// <summary>
        /// value 1 y
        /// </summary>
        public float Value1Y;

        /// <summary>
        /// value 2 w
        /// </summary>
        public float Value2W;

        /// <summary>
        /// value 3 y
        /// </summary>
        public float Value3Y;

        /// <summary>
        /// value 3 x
        /// </summary>
        public float Value3X;

        /// <summary>
        /// BaseColor in rgba (not bgra)
        /// </summary>
        public RGBA BaseColor;

        /// <summary>
        /// unknown Flags
        /// </summary>
        public ushort Flags;

        /// <summary>
        /// unknown field
        /// </summary>
        public ushort Unk0;

        /// <summary>
        /// value 3 W
        /// </summary>
        public float Value3W;

        /// <summary>
        /// value 3 Z
        /// </summary>
        public float Value3Z;

        /// <summary>
        /// value 4 y
        /// </summary>
        public float Value4Y;

        /// <summary>
        /// unknown field
        /// </summary>
        public float Unk1;

        /// <summary>
        /// unknown field
        /// </summary>
        public float Unk2;

        /// <summary>
        /// unknown field
        /// </summary>
        public float Unk3;

        /// <summary>
        /// unknown field
        /// </summary>
        public float Unk4;

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
                BumpScale = br.ReadSingle();
                Value0X = br.ReadSingle();
                Value0Y = br.ReadSingle();
                Value0Z = br.ReadSingle();
                Value1W = br.ReadSingle();
                Value0W = br.ReadSingle();
                Value1X = br.ReadSingle();
                Value1Y = br.ReadSingle();
                Value2W = br.ReadSingle();
                Value3Y = br.ReadSingle();
                Value3X = br.ReadSingle();
                BaseColor = br.ReadRGBA();
                Flags = br.ReadUInt16();
                Unk0 = br.ReadUInt16();
                Value3W = br.ReadSingle();
                Value3Z = br.ReadSingle();
                Value4Y = br.ReadSingle();
                Unk1 = br.ReadSingle();
                Unk2 = br.ReadSingle();
                Unk3 = br.ReadSingle();
                Unk4 = br.ReadSingle();
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
                    bw.Write(BumpScale);
                    bw.Write(Value0X);
                    bw.Write(Value0Y);
                    bw.Write(Value0Z);
                    bw.Write(Value1W);
                    bw.Write(Value0W);
                    bw.Write(Value1X);
                    bw.Write(Value1Y);
                    bw.Write(Value2W);
                    bw.Write(Value3Y);
                    bw.Write(Value3X);
                    bw.WriteRGBA(BaseColor);
                    bw.Write(Flags);
                    bw.Write(Unk0);
                    bw.Write(Value3W);
                    bw.Write(Value3Z);
                    bw.Write(Value4Y);
                    bw.Write(Unk1);
                    bw.Write(Unk2);
                    bw.Write(Unk3);
                    bw.Write(Unk4);
                }
                return ms.ToArray();
            }
        }
    }
}