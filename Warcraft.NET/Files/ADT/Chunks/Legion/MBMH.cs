using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.ADT.Entries.Legion;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MBMH Chunk - Level of detail
    /// </summary>
    public class MBMH : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MBMH";

        /// <summary>
        /// Gets or sets <see cref="MBMHEntry"/>s.
        /// </summary>
        public List<MBMHEntry> MBMHEntries { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MBMH"/> class.
        /// </summary>
        public MBMH()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBMH"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MBMH(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var objCount = br.BaseStream.Length / MBMHEntry.GetSize();

                for (var i = 0; i < objCount; ++i)
                {
                    MBMHEntries.Add(new MBMHEntry(br.ReadBytes(MBMHEntry.GetSize())));
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
                foreach (var mbmhEntry in MBMHEntries)
                {
                    bw.Write(mbmhEntry.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}