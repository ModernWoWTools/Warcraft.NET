using Warcraft.NET.Files.ADT.Entries;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks
{
    public class MDDF : IIFFChunk, IBinarySerializable
    {
        public const string Signature = "MDDF";

        /// <summary>
        /// Gets or sets <see cref="MDDFEntry"/>s.
        /// </summary>
        public List<MDDFEntry> MDDFEntries { get; set; } = new();

        public MDDF()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDDF"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MDDF(byte[] inData)
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
                var doodadCount = br.BaseStream.Length / MDDFEntry.GetSize();

                for (var i = 0; i < doodadCount; ++i)
                {
                    MDDFEntries.Add(new MDDFEntry(br.ReadBytes(MDDFEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (MDDFEntry doodad in MDDFEntries)
                {
                    bw.Write(doodad.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}
