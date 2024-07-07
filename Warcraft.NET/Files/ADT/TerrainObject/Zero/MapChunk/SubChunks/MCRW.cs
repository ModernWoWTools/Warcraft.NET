using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.TerrainObject.Zero.MapChunk.SubChunks
{
    /// <summary>
    /// MCRW Chunk - Holds world object references.
    /// </summary>
    public class MCRW : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCRW";

        /// <summary>
        /// Holds world object references
        /// </summary>
        public uint[] WorldObjectReferences;

        /// <summary>
        /// Initializes a new instance of the <see cref="MCRW"/> class.
        /// </summary>
        public MCRW()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCRW"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCRW(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                long worldObjectCount = ms.Length / sizeof(uint);
                WorldObjectReferences = new uint[worldObjectCount];
                for (var i = 0; i < worldObjectCount; ++i)
                {
                    WorldObjectReferences[i] = br.ReadUInt32();
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
                foreach (uint worldObject in WorldObjectReferences)
                {
                    bw.Write(worldObject);
                }

                return ms.ToArray();
            }
        }
    }
}