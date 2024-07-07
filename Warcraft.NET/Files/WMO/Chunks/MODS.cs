using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.WMO.Entries;

namespace Warcraft.NET.Files.WMO.Chunks
{
    public class MODS : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MODS";

        /// <summary>
        /// Gets or sets <see cref="MODSEntry"s />
        /// </summary>
        public List<MODSEntry> MODSEntries { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MODS"/> class.
        /// </summary>
        public MODS()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODS"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MODS(byte[] inData)
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
                var materialCount = br.BaseStream.Length / MODSEntry.GetSize();
                for (var i = 0; i < materialCount; ++i)
                    MODSEntries.Add(new MODSEntry(br.ReadBytes(MODSEntry.GetSize())));
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var material in MODSEntries)
                    bw.Write(material.Serialize());
                return ms.ToArray();
            }
        }
    }
}
