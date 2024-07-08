using System.IO;
using Warcraft.NET.Files.TEX.Flags;
using System.Collections;
using Warcraft.NET.Files.TEX.Chunks;

namespace Warcraft.NET.Files.TEX.Entries
{
    /// <summary>
    /// An entry struct containing information about blob texture.
    /// </summary>
    public class TXBTEntry
    {
        /// <summary>
        /// Gets or sets the texture file id
        /// </summary>
        public uint TextureId { get; set; }

        /// <summary>
        /// Gets or sets the txmd chunk offset
        /// </summary>
        public uint TXMDOffset { get; set; }

        /// <summary>
        /// Gets or sets x size
        /// </summary>
        public byte SizeX { get; set; }

        /// <summary>
        /// Gets or sets y size
        /// </summary>
        public byte SizeY { get; set; }

        /// <summary>
        /// Gets or sets the mipmap level count
        /// </summary>
        public byte MipMapLevelCount { get; set; }

        /// <summary>
        /// Gets or sets loaded flag
        /// </summary>
        public bool Loaded { get; set; } = false;

        /// <summary>
        /// Gets or sets the dxt type (0  = DXT1, 1 = DXT3, 2 = DXT5)
        /// </summary>
        public byte DXTType { get; set; }

        /// <summary>
        /// Gets or sets flags
        /// </summary>
        public TXBTFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets Texture Data
        /// </summary>
        public TXMD TextureData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXBTEntry"/> class.
        /// </summary>
        public TXBTEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXBTEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public TXBTEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                TextureId = br.ReadUInt32();
                TXMDOffset = br.ReadUInt32();
                SizeX = br.ReadByte();
                SizeY = br.ReadByte();
                ReadMipMapLevelCountAndLoaded(br);
                ReadDxtTypeAndFlags(br);
            }
        }

        protected void ReadMipMapLevelCountAndLoaded(BinaryReader br)
        {
            BitArray mipMapLevelBits = new BitArray(7);
            BitArray bits = new BitArray(new byte[] { br.ReadByte() });
            
            for (int i = 0; i < 6; i++)
            {
                mipMapLevelBits[i] = bits[i];
            }

            byte[] byteResult = new byte[1];
            mipMapLevelBits.CopyTo(byteResult, 0);

            MipMapLevelCount = byteResult[0];
            Loaded = (bits[7] == true);
        }

        protected void ReadDxtTypeAndFlags(BinaryReader br)
        {
            BitArray dxtTypeBits = new BitArray(4);
            BitArray flagBits = new BitArray(4);
            BitArray bits = new BitArray(new byte[] { br.ReadByte() });

            for (int i = 0; i < 4; i++)
            {
                dxtTypeBits[i] = bits[i];
            }

            for (int i = 4; i < 8; i++)
            {
                flagBits[i-4] = bits[i];
            }

            byte[] byteResult = new byte[2];
            dxtTypeBits.CopyTo(byteResult, 0);
            flagBits.CopyTo(byteResult, 1);

            DXTType = byteResult[0];
            Flags = (TXBTFlags)byteResult[1];
        }

        protected void WriteMipMapLevelCountAndLoaded(BinaryWriter bw)
        {
            BitArray mipMapLevelBits = new BitArray(new byte[] { MipMapLevelCount });
            mipMapLevelBits.Set(7, Loaded);

            byte[] resultByte = new byte[1];
            mipMapLevelBits.CopyTo(resultByte, 0);
            bw.Write(resultByte);
        }

        protected void WriteDxtTypeAndFlags(BinaryWriter bw)
        {
            BitArray dxtTypeBits = new BitArray(new byte[] { DXTType });
            BitArray flagBits = new BitArray(new byte[] { (byte)Flags });

            BitArray combinedBits = new BitArray(dxtTypeBits);
            for (int i = 4; i < 8; i++)
            {
                combinedBits[i] = flagBits[i - 4];
            }

            byte[] resultByte = new byte[1];
            combinedBits.CopyTo(resultByte, 0);
            bw.Write(resultByte);
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 12;
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
                bw.Write(TextureId);
                bw.Write(TXMDOffset);
                bw.Write(SizeX);
                bw.Write(SizeY);
                WriteMipMapLevelCountAndLoaded(bw);
                WriteDxtTypeAndFlags(bw);
                return ms.ToArray();
            }
        }
    }
}