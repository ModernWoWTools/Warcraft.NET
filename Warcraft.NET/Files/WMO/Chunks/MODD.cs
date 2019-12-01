using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.WMO.Entries;

namespace Warcraft.NET.Files.WMO.Chunks
{
    public class MODD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MODD";

        /// <summary>
        /// Gets or sets <see cref="MODDEntry"s />
        /// </summary>
        public List<MODDEntry> MODDEntries { get; set; } = new List<MODDEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MODD"/> class.
        /// </summary>
        public MODD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODD"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MODD(byte[] inData)
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
                var materialCount = br.BaseStream.Length / MODDEntry.GetSize();
                for (var i = 0; i < materialCount; ++i)
                    MODDEntries.Add(new MODDEntry(br.ReadBytes(MODDEntry.GetSize())));
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var material in MODDEntries)
                    bw.Write(material.Serialize());
                return ms.ToArray();
            }
        }
    }
}
