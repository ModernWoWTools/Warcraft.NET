using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.ADT.Entries.Legion;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MBMI Chunk - Level of detail
    /// </summary>
    public class MBNV : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MBNV";

        /// <summary>
        /// Blend Mesh Vertices.
        /// </summary>
        public List<MBNVEntry> Entries { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MBNV"/> class.
        /// </summary>
        public MBNV()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBNV"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MBNV(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mbnvCount = br.BaseStream.Length / MBNVEntry.GetSize();
                for (var i = 0; i < mbnvCount; ++i)
                {
                    Entries.Add(new MBNVEntry(br.ReadBytes(MBNVEntry.GetSize())));
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
                    bw.Write(entry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}