using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks
{
    /// <summary>
    /// MCLV Chunk - Painted per-vertex lighting.
    /// </summary>
    public class MCLV : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCLV";

        /// <summary>
        /// Gets or sets Vertex lighting
        /// </summary>
        public RGBA[] VertexLighting { get; set; } = new RGBA[9 * 9 + 8 * 8];

        /// <summary>
        /// Initializes a new instance of the <see cref="MCLV"/> class.
        /// </summary>
        public MCLV()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCLV"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCLV(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                long vertexLightingCount = ms.Length / VertexLighting.Length;

                for (var i = 0; i < vertexLightingCount; ++i)
                {
                    VertexLighting[i] = br.ReadRGBA();
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
                foreach (RGBA vertexLighting in VertexLighting)
                {
                    bw.WriteRGBA(vertexLighting);
                }

                return ms.ToArray();
            }
        }
    }
}
