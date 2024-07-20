using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.TEX.Chunks
{
    /// <summary>
    /// TXVR Chunk - Contains the texture blob version.
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class TXVR : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "TXVR";

        /// <summary>
        /// Gets or sets the ADT version.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXVR"/> class.
        /// </summary>
        public TXVR()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXVR"/> class.
        /// </summary>
        /// <param name="version">File version</param>
        public TXVR(uint version)
        {
            Version = version;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXVR"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public TXVR(byte[] inData)
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
