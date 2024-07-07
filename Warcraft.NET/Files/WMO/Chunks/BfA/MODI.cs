using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.WMO.Chunks.BfA
{
    /// <summary>
    /// MODI Chunk - Contains a list model file ids.
    /// </summary>
    public class MODI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MODI";

        /// <summary>
        /// Gets or sets the list of file ids in an MODI chunk.
        /// </summary>
        public List<uint> ModelFileIds { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MODI"/> class.
        /// </summary>
        public MODI()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODI"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MODI(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var offsetCount = inData.Length / sizeof(uint);
                for (var i = 0; i < offsetCount; ++i)
                {
                    ModelFileIds.Add(br.ReadUInt32());
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
                foreach (uint modelFileId in ModelFileIds)
                {
                    bw.Write(modelFileId);
                }

                return ms.ToArray();
            }
        }
    }
}