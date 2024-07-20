using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Entries.Legion;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLLL Chunk - Level of detail levels
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MLLL : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLLL";

        /// <summary>
        /// Gets or sets <see cref="MLLLEntry"/>s.
        /// </summary>
        public List<MLLLEntry> MLLLEntries { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLL"/> class.
        /// </summary>
        public MLLL()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLL"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLLL(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mlllCount = br.BaseStream.Length / MLLLEntry.GetSize();
                for (var i = 0; i < mlllCount; ++i)
                {
                    MLLLEntries.Add(new MLLLEntry(br.ReadBytes(MLLLEntry.GetSize())));
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
                foreach (var entry in MLLLEntries)
                {
                    bw.Write(entry.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}