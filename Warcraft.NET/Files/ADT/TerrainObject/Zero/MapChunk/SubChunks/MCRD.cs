using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.TerrainObject.Zero.MapChunk.SubChunks
{
    /// <summary>
    /// MCRD Chunk - Holds model references.
    /// </summary>
    public class MCRD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCRD";

        /// <summary>
        /// Holds model references
        /// </summary>
        public uint[] ModelReferences;

        /// <summary>
        /// Initializes a new instance of the <see cref="MCRD"/> class.
        /// </summary>
        public MCRD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCRD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCRD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                long modelCount = ms.Length / sizeof(uint);
                ModelReferences = new uint[modelCount];
                for (var i = 0; i < modelCount; ++i)
                {
                    ModelReferences[i] = br.ReadUInt32();
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
                foreach (uint model in ModelReferences)
                {
                    bw.Write(model);
                }

                return ms.ToArray();
            }
        }
    }
}