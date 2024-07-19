using System.IO;
using System.Numerics;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLLV Chunk - Level of detail vertices
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MLLV : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLLV";

        /// <summary>
        /// Unknown.
        /// </summary>
        public Vector3[] LiquidVertices { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MLLI"/> class.
        /// </summary>
        public MLLV()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLV"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLLV(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var vertexCount = inData.Length / 12;
                LiquidVertices = new Vector3[vertexCount];
                for (var i = 0; i < vertexCount; ++i)
                {
                    LiquidVertices[i] = br.ReadVector3();
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
                foreach (var vertex in LiquidVertices)
                {
                    bw.WriteVector3(vertex);
                }
                return ms.ToArray();
            }
        }
    }
}