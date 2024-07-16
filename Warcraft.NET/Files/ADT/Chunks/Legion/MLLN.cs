using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLLN Chunk - Level of detail liquid
    /// </summary>
    public class MLLN : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLLN";

        /// <summary>
        /// Unknown.
        /// </summary>
        public uint Unknown0 { get; set; }

        /// <summary>
        /// MLLI Length.
        /// </summary>
        public uint MLLILength { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public uint Unknown2 { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public ushort Unknown3A { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public ushort Unknown3B { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public uint Unknown4 { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public uint Unknown5 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLN"/> class.
        /// </summary>
        public MLLN()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLN"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLLN(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Unknown0 = br.ReadUInt32();
                MLLILength = br.ReadUInt32();
                Unknown2 = br.ReadUInt32();
                Unknown3A = br.ReadUInt16();
                Unknown3B = br.ReadUInt16();
                Unknown4 = br.ReadUInt32();
                Unknown5 = br.ReadUInt32();
            }
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Unknown0);
                bw.Write(MLLILength);
                bw.Write(Unknown2);
                bw.Write(Unknown3A);
                bw.Write(Unknown3B);
                bw.Write(Unknown4);
                bw.Write(Unknown5);
                return ms.ToArray();
            }
        }
    }
}