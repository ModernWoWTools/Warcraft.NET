using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MBMB Chunk - Level of detail
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MBMB : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MBMB";

        /// <summary>
        /// Unknown array with 20 byte elements.
        /// </summary>
        public List<byte[]> Entries = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MBMB"/> class.
        /// </summary>
        public MBMB()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBMB"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MBMB(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mbmbCount = br.BaseStream.Length / 20;
                for (var i = 0; i < mbmbCount; ++i)
                {
                    Entries.Add(br.ReadBytes(20));
                }
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
                foreach (var entry in Entries)
                {
                    bw.Write(entry);
                }
                return ms.ToArray();
            }
        }
    }
}