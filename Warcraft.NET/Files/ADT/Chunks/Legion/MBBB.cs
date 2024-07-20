using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Entries.Legion;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MBBB Chunk - Level of detail
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MBBB : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MBBB";

        /// <summary>
        /// Gets or sets <see cref="MBBBEntry"/>s.
        /// </summary>
        public List<MBBBEntry> MBBBEntries { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MBBB"/> class.
        /// </summary>
        public MBBB()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBBB"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MBBB(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mbbbCount = br.BaseStream.Length / MBBBEntry.GetSize();
                for (var i = 0; i < mbbbCount; ++i)
                {
                    MBBBEntries.Add(new MBBBEntry(br.ReadBytes(MBBBEntry.GetSize())));
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
                foreach (var mbbb in MBBBEntries)
                {
                    bw.Write(mbbb.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}