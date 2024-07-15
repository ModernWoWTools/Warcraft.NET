using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MBMI Chunk - Level of detail
    /// </summary>
    public class MBMI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MBMI";

        /// <summary>
        /// Blend Mesh indices.
        /// </summary>
        public ushort[] BlendMeshIndices { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="MBMI"/> class.
        /// </summary>
        public MBMI()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBMI"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MBMI(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var count = (int)(ms.Length / 2);
                BlendMeshIndices = new ushort[count];
                for (var i = 0; i < count; i++)
                {
                    BlendMeshIndices[i] = br.ReadUInt16();
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
                foreach (var index in BlendMeshIndices)
                {
                    bw.Write(index);
                }
                return ms.ToArray();
            }
        }
    }
}