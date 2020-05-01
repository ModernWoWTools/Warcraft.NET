using System;
using System.Collections;
using System.IO;

namespace Warcraft.NET.Files.ADT.Entrys.MoP
{
    /// <summary>
    /// An entry struct containing information about the WMO.
    /// </summary>
    public class MTXPEntry
    {
        /// <summary>
        /// Disable specular or height texture loading
        /// </summary>
        public bool DontLoadSpecularOrHeightTexture { get; set; } = false;

        /// <summary>
        /// Unknown Value 1
        /// </summary>
        public byte Unknown1 { get; set; } = 0;

        /// <summary>
        /// Texture scale max is 15, default 1
        /// </summary>
        public byte TextureScale { get; set; } = 1;

        /// <summary>
        /// Unknown Value 2
        /// </summary>
        public uint Unknown2 { get; set; } = 0;

        /// <summary>
        /// The _h texture values are scaled to [0, value) to determine actual "height". This determines if textures overlap or not (e.g. roots on top of roads). 
        /// </summary>
        public float HeightScale { get; set; } = 0;

        /// <summary>
        /// Note that _h based chunks are still influenced by MCAL (blendTex below)
        /// </summary>
        public float HeightOffset { get; set; } = 1;

        /// <summary>
        /// Padding
        /// </summary>
        public uint Padding { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MTXPEntry"/> class.
        /// </summary>
        public MTXPEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MTXPEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MTXPEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                ReadMTXPFlags(br);
                HeightScale = br.ReadSingle();
                HeightOffset = br.ReadSingle();
                Padding = br.ReadUInt32();
            }
        }

        private BitArray bits;

        protected void ReadMTXPFlags(BinaryReader br)
        {
            BitArray unknown1Bits = new BitArray(3);
            BitArray textureScaleBits = new BitArray(4);
            BitArray unknown2Bits = new BitArray(24);
            bits = new BitArray(BitConverter.GetBytes(br.ReadUInt32()));

            DontLoadSpecularOrHeightTexture = (bits[0] == true);

            #region Unknown1
            unknown1Bits.Set(0, bits[1]);
            unknown1Bits.Set(1, bits[2]);
            unknown1Bits.Set(2, bits[3]);
            byte[] unk1Bytes = new byte[1];
            unknown1Bits.CopyTo(unk1Bytes, 0);
            Unknown1 = unk1Bytes[0];
            #endregion

            #region TextureScale
            textureScaleBits.Set(0, bits[4]);
            textureScaleBits.Set(1, bits[5]);
            textureScaleBits.Set(2, bits[6]);
            textureScaleBits.Set(3, bits[7]);
            byte[] textureScaleBytes = new byte[1];
            textureScaleBits.CopyTo(textureScaleBytes, 0);
            TextureScale = textureScaleBytes[0];
            #endregion

            #region Unknown2
            for (int i = 0; i < unknown2Bits.Length; i++)
            {
                unknown2Bits[i] = bits[i + 8];
            }
            uint[] unk2Uints = new uint[1];
            unknown2Bits.CopyTo(unk2Uints, 0);
            Unknown2 = unk2Uints[0];
            #endregion
        }

        /// <summary>
        /// Gets the size of a placement entry.
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
                    WriteMTXPFlags(bw);
                    bw.Write(HeightScale);
                    bw.Write(HeightOffset);
                    bw.Write(Padding);
                }

                return ms.ToArray();
            }
        }

        protected void WriteMTXPFlags(BinaryWriter bw)
        {
            BitArray flagBits = new BitArray(32);

            // DontLoadSpecularOrHeightTexture
            flagBits.Set(0, DontLoadSpecularOrHeightTexture);

            #region Unknown1
            BitArray unknown1Bits = new BitArray(BitConverter.GetBytes(Unknown1));
            flagBits.Set(1, unknown1Bits[0]);
            flagBits.Set(2, unknown1Bits[1]);
            flagBits.Set(3, unknown1Bits[2]);
            #endregion

            #region TextureScale
            BitArray textureScaleBits = new BitArray(BitConverter.GetBytes(TextureScale));
            flagBits.Set(4, textureScaleBits[0]);
            flagBits.Set(5, textureScaleBits[1]);
            flagBits.Set(6, textureScaleBits[2]);
            flagBits.Set(7, textureScaleBits[3]);
            #endregion

            #region Unknown2
            BitArray unknown2Bits = new BitArray(BitConverter.GetBytes(Unknown2));
            for (int i = 0; i < 24; i++)
            {
                flagBits.Set(i + 8, unknown2Bits[i]);
            }
            #endregion

            // Write
            uint[] flagUint = new uint[1];
            flagBits.CopyTo(flagUint, 0);

            bw.Write(flagUint[0]);
        }
    }
}