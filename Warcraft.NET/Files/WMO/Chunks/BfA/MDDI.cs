using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.WMO.Chunks.BfA
{
    /// <summary>
    /// MDDI Chunk - Contains float values to doodads entrys
    /// </summary>
    public class MDDI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MDDI";

        /// <summary>
        /// Gets or sets the list of float values in an MDDI chunk.
        /// </summary>
        public List<float> FloatValues { get; set; } = new List<float>();

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
                    FloatValues.Add(br.ReadUInt32());
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
                foreach (uint floatValue in FloatValues)
                {
                    bw.Write(floatValue);
                }

                return ms.ToArray();
            }
        }
    }
}