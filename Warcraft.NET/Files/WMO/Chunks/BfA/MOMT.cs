using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.WMO.Entries.BfA;

namespace Warcraft.NET.Files.WMO.Chunks.BfA
{
    public class MOMT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MOMT";

        /// <summary>
        /// Gets or sets <see cref="MOMTEntry"s />
        /// </summary>
        public List<MOMTEntry> MOMTEntries { get; set; } = new List<MOMTEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MOMT"/> class.
        /// </summary>
        public MOMT()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MOMT"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MOMT(byte[] inData)
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
                var materialCount = br.BaseStream.Length / MOMTEntry.GetSize();
                for (var i = 0; i < materialCount; ++i)
                    MOMTEntries.Add(new MOMTEntry(br.ReadBytes(MOMTEntry.GetSize())));
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var material in MOMTEntries)
                    bw.Write(material.Serialize());
                return ms.ToArray();
            }
        }
    }
}
