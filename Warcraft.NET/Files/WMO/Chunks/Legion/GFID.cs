using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.WMO.Chunks.Legion
{
    /// <summary>
    /// GFID Chunk - Contains a list group and lod file ids.
    /// </summary>
    public class GFID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "GFID";

        /// <summary>
        /// Gets or sets the list of file ids in an GFID chunk.
        /// </summary>
        public List<uint> GroupFileIds { get; set; } = new List<uint>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GFID"/> class.
        /// </summary>
        public GFID()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GFID"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public GFID(byte[] inData)
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
                    GroupFileIds.Add(br.ReadUInt32());
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
                foreach (uint groupFileId in GroupFileIds)
                {
                    bw.Write(groupFileId);
                }

                return ms.ToArray();
            }
        }
    }
}