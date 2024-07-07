using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.TEX.Entrys;

namespace Warcraft.NET.Files.TEX.Chunks
{
    /// <summary>
    /// TXBT Chunk - Contains blob texture information.
    /// </summary>
    public class TXBT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "TXBT";

        /// <summary>
        /// Gets or sets model extents. Same count as <see cref="TXBT"/>
        /// </summary>
        public List<TXBTEntry> Entries { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="TXBT"/> class.
        /// </summary>
        public TXBT()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXBT"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public TXBT(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var entryCount = br.BaseStream.Length / TXBTEntry.GetSize();

                for (var i = 0; i < entryCount; ++i)
                {
                    Entries.Add(new TXBTEntry(br.ReadBytes(TXBTEntry.GetSize())));
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
                foreach (TXBTEntry entry in Entries)
                {
                    bw.Write(entry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}