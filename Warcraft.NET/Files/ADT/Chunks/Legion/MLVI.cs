using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLVI Chunk - Level of detail 
    /// </summary>
    public class MLVI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLVI";

        /// <summary>
        /// Vertex indices.
        /// </summary>
        public ushort[] VertexIndices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLVI"/> class.
        /// </summary>
        public MLVI()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLVI"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLVI(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mlviCount = br.BaseStream.Length / sizeof(uint);
                VertexIndices = new ushort[mlviCount];
                for (var i = 0; i < mlviCount; i++)
                {
                    VertexIndices[i] = br.ReadUInt16();
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
            return (uint)VertexIndices.Length * 4;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var vertexIndex in VertexIndices)
                {
                    bw.Write(vertexIndex);
                }
                return ms.ToArray();
            }
        }
    }
}