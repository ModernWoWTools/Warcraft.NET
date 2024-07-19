using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Entries.Legion;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLND Chunk - Level of detail 
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MLND : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLND";

        /// <summary>
        /// Gets or sets <see cref="MLNDEntry"/>s.
        /// </summary>
        public List<MLNDEntry> MLNDEntries { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MLND"/> class.
        /// </summary>
        public MLND()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLND"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLND(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mlndCount = br.BaseStream.Length / MLNDEntry.GetSize();
                for (var i = 0; i < mlndCount; ++i)
                {
                    MLNDEntries.Add(new MLNDEntry(br.ReadBytes(MLNDEntry.GetSize())));
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
                foreach (var entry in MLNDEntries)
                {
                    bw.Write(entry.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}