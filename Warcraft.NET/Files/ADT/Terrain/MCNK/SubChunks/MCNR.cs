using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks
{
    /// <summary>
    /// MCNR chunk - Holds per-vertex normals of a map chunk.
    /// </summary>
    public class MCNR : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds wotlk padding size
        /// </summary>
        public static readonly int PaddingLength = 13;

        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCNR";

        /// <summary>
        /// Gets or sets vertex normals
        /// </summary>
        public ByteVector3[] VertexNormals { get; set; } = new ByteVector3[9 * 9 + 8 * 8];

        /// <summary>
        /// Gets or sets vertex normals padding
        /// </summary>
        public byte[] Padding { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCNR"/> class.
        /// </summary>
        public MCNR()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCNR"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCNR(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                for (var i = 0; i < VertexNormals.Length; ++i)
                {
                    VertexNormals[i] = br.ReadByteVector3();
                }

                if (ms.Length - ms.Position == PaddingLength)
                {
                    Padding = br.ReadBytes(PaddingLength);
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
                foreach (ByteVector3 vertexNormal in VertexNormals)
                {
                    bw.WriteByteVector3(vertexNormal);
                }

                if (Padding != null)
                {
                    bw.Write(Padding);
                }

                return ms.ToArray();
            }
        }
    }
}
