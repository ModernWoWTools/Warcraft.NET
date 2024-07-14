using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.WDT.Entries.Legion;

namespace Warcraft.NET.Files.WDT.Chunks.Legion
{
    /// <summary>
    /// MLTA Chunk - Contains light animations
    /// </summary>
    public class MLTA : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLTA";

        public List<MLTAEntry> Entries = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MLTA"/> class.
        /// </summary>
        public MLTA()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLTA"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLTA(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mltaCount = br.BaseStream.Length / MLTAEntry.GetSize();

                for (var i = 0; i < mltaCount; ++i)
                {
                    Entries.Add(new MLTAEntry(br.ReadBytes(MLTAEntry.GetSize())));
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
                foreach (MLTAEntry mltaEntry in Entries)
                {
                    bw.Write(mltaEntry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}
