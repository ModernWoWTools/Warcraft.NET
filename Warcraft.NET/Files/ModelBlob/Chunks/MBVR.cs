using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ModelBlob.Chunks
{
    /// <summary>
    /// MBVR Chunk - Contains the model blob version.
    /// </summary>
    public class MBVR : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MBVR";

        /// <summary>
        /// Gets or sets the ADT version.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBVR"/> class.
        /// </summary>
        public MBVR()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBVR"/> class.
        /// </summary>
        /// <param name="version">File version</param>
        public MBVR(uint version)
        {
            Version = version;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBVR"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MBVR(byte[] inData)
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
