using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WDT.Chunks.TWW
{
    /// <summary>
    /// VFEX Chunk
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterDF, AutoDocChunkVersionHelper.VersionBeforeTWW)]
    public class VFEX : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "VFEX";

        public uint Unknown0 { get; set; }
        public float[] Unknown1 { get; set; }

        /// <summary>
        /// Reference to the ID in the VFOG entry this VFEX belongs to.
        /// </summary>
        public uint VfogId { get; set; }

        public uint Unknown3 { get; set; }
        public uint Unknown4 { get; set; }
        public uint Unknown5 { get; set; }
        public uint Unknown6 { get; set; }
        public uint Unknown7 { get; set; }
        public uint Unknown8 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VFEX"/> class.
        /// </summary>
        public VFEX()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VFEX"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public VFEX(byte[] inData)
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
                Unknown1 = new float[16];
                for (var i = 0; i < 16; i++)
                {
                    Unknown1[i] = br.ReadSingle();
                }

                VfogId = br.ReadUInt32();
                Unknown3 = br.ReadUInt32();
                Unknown4 = br.ReadUInt32();
                Unknown5 = br.ReadUInt32();
                Unknown6 = br.ReadUInt32();
                Unknown7 = br.ReadUInt32();
                Unknown8 = br.ReadUInt32();
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
                for (var i = 0; i < 16; i++)
                {
                    bw.Write(Unknown1[i]);
                }
                bw.Write(VfogId);
                bw.Write(Unknown3);
                bw.Write(Unknown4);
                bw.Write(Unknown5);
                bw.Write(Unknown6);
                bw.Write(Unknown7);
                bw.Write(Unknown8);

                return ms.ToArray();
            }
        }
    }
}
