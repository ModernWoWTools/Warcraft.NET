using Warcraft.NET.Files.Interfaces;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks
{
    /// <summary>
    /// MVER Chunk - Contains the ADT version.
    /// </summary>
    public class MVER : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MVER";

        /// <summary>
        /// Gets or sets the ADT version.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MVER"/> class.
        /// </summary>
        public MVER()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MVER"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MVER(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Version = br.ReadUInt32();
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
                bw.Write(Version);

                return ms.ToArray();
            }
        }
    }
}