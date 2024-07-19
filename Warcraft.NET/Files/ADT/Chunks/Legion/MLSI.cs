using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLSI Chunk - Level of detail 
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MLSI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLSI";

        /// <summary>
        /// Skirt indices.
        /// </summary>
        public ushort[] SkirtIndices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLSI"/> class.
        /// </summary>
        public MLSI()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLSI"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLSI(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mlsiCount = br.BaseStream.Length / sizeof(uint);
                SkirtIndices = new ushort[mlsiCount];
                for (var i = 0; i < mlsiCount; i++)
                {
                    SkirtIndices[i] = br.ReadUInt16();
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
                foreach (var skirtIndex in SkirtIndices)
                {
                    bw.Write(skirtIndex);
                }
                return ms.ToArray();
            }
        }
    }
}