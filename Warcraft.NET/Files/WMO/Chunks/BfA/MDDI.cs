using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.WMO.Chunks.BfA
{
    /// <summary>
    /// MDDI Chunk - Contains scale values to doodads entries
    /// </summary>
    public class MDDI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MDDI";

        /// <summary>
        /// Gets or sets the list of scale values in an MDDI chunk.
        /// </summary>
        public List<float> ScaleValues { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MDDI"/> class.
        /// </summary>
        public MDDI()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDDI"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MDDI(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var offsetCount = inData.Length / sizeof(uint);
                for (var i = 0; i < offsetCount; ++i)
                {
                    ScaleValues.Add(br.ReadUInt32());
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
                foreach (uint scale in ScaleValues)
                {
                    bw.Write(scale);
                }

                return ms.ToArray();
            }
        }
    }
}