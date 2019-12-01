using Warcraft.NET.Files.ADT.Entrys.Legion;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLMX Chunk - Contains model bounding information.
    /// </summary>
    public class MLMX : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLMX";

        /// <summary>
        /// Gets or sets model extents.
        /// </summary>
        public List<MLMXEntry> Entries { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLMX"/> class.
        /// </summary>
        public MLMX()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLMX"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLMX(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var entryCount = br.BaseStream.Length / MLMXEntry.GetSize();

                for (var i = 0; i < entryCount; ++i)
                {
                    Entries.Add(new MLMXEntry(br.ReadBytes(MLMXEntry.GetSize())));
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
                foreach (MLMXEntry entry in Entries)
                {
                    bw.Write(entry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}