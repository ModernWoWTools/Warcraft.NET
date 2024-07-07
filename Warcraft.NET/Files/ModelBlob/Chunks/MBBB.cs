using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ModelBlob.Chunks
{
    /// <summary>
    /// MBBB Chunk - Contains model blob extents referenced by fileid.
    /// </summary>
    public class MBBB : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MBBB";

        /// <summary>
        /// Gets or sets model extents.
        /// </summary>
        public Dictionary<uint, BoundingBox> Entries { get; set; } = new();

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
                var entryCount = br.BaseStream.Length / 28;

                for (var i = 0; i < entryCount; ++i)
                {
                    Entries.Add(br.ReadUInt32(), br.ReadBoundingBox(Structures.AxisConfiguration.Native));
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
                foreach (var entry in Entries)
                {
                    bw.Write(entry.Key);
                    bw.WriteBoundingBox(entry.Value, Structures.AxisConfiguration.Native);
                }

                return ms.ToArray();
            }
        }
    }
}