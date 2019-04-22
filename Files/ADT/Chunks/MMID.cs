using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks
{
    /// <summary>
    /// MMID Chunk - Contains a list of M2 model indexes.
    /// </summary>
    public class MMID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MMID";

        /// <summary>
        /// Gets or sets the list of indexes for models in an MMID chunk.
        /// </summary>
        public List<uint> ModelFilenameOffsets { get; set; } = new List<uint>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MMID"/> class.
        /// </summary>
        public MMID()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MMID"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MMID(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var offsetCount = inData.Length / 4;
                for (var i = 0; i < offsetCount; ++i)
                {
                    ModelFilenameOffsets.Add(br.ReadUInt32());
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
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (uint modelFilenameOffset in ModelFilenameOffsets)
                {
                    bw.Write(modelFilenameOffset);
                }

                return ms.ToArray();
            }
        }
    }
}