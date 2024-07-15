using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLFD Chunk - Level of detail offset information
    /// </summary>
    public class MLFD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLFD";

        /// <summary>
        /// Index into <see cref="MLDD"/> per lod
        /// </summary>
        public uint[] ModelLodOffset = new uint[3] { 0, 0, 0 };

        /// <summary>
        /// Number of elements used from <see cref="MLDD"/> per lod
        /// </summary>
        public uint[] ModelLodLength = new uint[3] { 0, 0, 0 };

        /// <summary>
        /// Index into <see cref="MLDD"/> per lod
        /// </summary>
        public uint[] WorldObjectLodOffset = new uint[3] { 0, 0, 0 };

        /// <summary>
        /// Number of elements used from <see cref="MLDD"/> per lod
        /// </summary>
        public uint[] WorldObjectLodLength = new uint[3] { 0, 0, 0 };

        /// <summary>
        /// Initializes a new instance of the <see cref="MLFD"/> class.
        /// </summary>
        public MLFD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLFD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLFD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                ModelLodOffset = new uint[3] { br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32() };
                ModelLodLength = new uint[3] { br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32() };
                WorldObjectLodOffset = new uint[3] { br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32() };
                WorldObjectLodLength = new uint[3] { br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32() };
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
                foreach (uint entry in ModelLodOffset)
                    bw.Write(entry);

                foreach (uint entry in ModelLodLength)
                    bw.Write(entry);

                foreach (uint entry in WorldObjectLodOffset)
                    bw.Write(entry);

                foreach (uint entry in WorldObjectLodLength)
                    bw.Write(entry);

                return ms.ToArray();
            }
        }
    }
}