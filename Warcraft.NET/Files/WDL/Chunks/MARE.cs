using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WDL.Chunks
{
    /// <summary>
    /// MARE Chunk - Represents a LOD level of a map area.
    /// </summary>
    public class MARE : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MARE";

        /// <summary>
        /// Gets the high-resolution vertices.
        /// </summary>
        public List<short> HighResVertices { set; get; } = new(new short[289]);

        /// <summary>
        /// Gets the low-resolution vertices.
        /// </summary>
        public List<short> LowResVertices { set; get; } = new(new short[256]);

        /// <summary>
        /// Initializes a new instance of the <see cref="MARE"/> class.
        /// </summary>
        public MARE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MARE"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MARE(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using var ms = new MemoryStream(inData);
            using var br = new BinaryReader(ms);
            for (var y = 0; y < 17; ++y)
            {
                for (var x = 0; x < 17; ++x)
                {
                    HighResVertices.Add(br.ReadInt16());
                }
            }

            for (var y = 0; y < 16; ++y)
            {
                for (var x = 0; x < 16; ++x)
                {
                    LowResVertices.Add(br.ReadInt16());
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
            return GetSizeStatic();
        }

        /// <inheritdoc/>
        public static uint GetSizeStatic()
        {
            return ((17 * 17) * sizeof(short)) + ((16 * 16) * sizeof(short));
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var lodVertex in HighResVertices)
                {
                    bw.Write(lodVertex);
                }

                foreach (var lodVertex in LowResVertices)
                {
                    bw.Write(lodVertex);
                }
            }

            return ms.ToArray();
        }
    }
}