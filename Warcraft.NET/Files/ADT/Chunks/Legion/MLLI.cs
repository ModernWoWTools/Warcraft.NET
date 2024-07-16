using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLLI Chunk - Level of detail indices
    /// </summary>
    public class MLLI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLLI";

        /// <summary>
        /// Unknown.
        /// </summary>
        public ShortVector3[] LiquidIndices { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MLLI"/> class.
        /// </summary>
        public MLLI()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLI"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLLI(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var vertexCount = inData.Length / 6;
                LiquidIndices = new ShortVector3[vertexCount];
                for (var i = 0; i < vertexCount; ++i)
                {
                    LiquidIndices[i] = br.ReadShortVector3();
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
                foreach (var vertex in LiquidIndices)
                {
                    bw.WriteShortVector3(vertex);
                }
                return ms.ToArray();
            }
        }
    }
}