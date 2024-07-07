using Warcraft.NET.Files.ADT.Entrys.Legion;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    public class MLMD : IIFFChunk, IBinarySerializable
    {
        public const string Signature = "MLMD";

        /// <summary>
        /// Gets or sets <see cref="MLMDEntry"/>s.
        /// </summary>
        public List<MLMDEntry> MLMDEntrys { get; set; } = new();

        public MLMD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLMD"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MLMD(byte[] inData)
        {
            LoadBinaryData(inData);
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
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var objCount = br.BaseStream.Length / MLMDEntry.GetSize();

                for (var i = 0; i < objCount; ++i)
                {
                    MLMDEntrys.Add(new MLMDEntry(br.ReadBytes(MLMDEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (MLMDEntry obj in MLMDEntrys)
                {
                    bw.Write(obj.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}
