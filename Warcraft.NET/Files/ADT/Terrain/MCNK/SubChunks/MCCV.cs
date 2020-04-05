using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks
{
    /// <summary>
    /// MCCV Chunk - Painted per-vertex shading.
    /// </summary>
    public class MCCV : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCCV";

        /// <summary>
        /// Gets or sets Vertex shading
        /// </summary>
        public RGBA[] VertexShading { get; set; } = new RGBA[9 * 9 + 8 * 8];

        /// <summary>
        /// Initializes a new instance of the <see cref="MCCV"/> class.
        /// </summary>
        public MCCV()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCCV"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCCV(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                for (var i = 0; i < VertexShading.Length; ++i)
                {
                    VertexShading[i] = br.ReadRGBA();
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
                foreach (RGBA vertexShading in VertexShading)
                {
                    bw.WriteRGBA(vertexShading);
                }

                return ms.ToArray();
            }
        }
    }
}
