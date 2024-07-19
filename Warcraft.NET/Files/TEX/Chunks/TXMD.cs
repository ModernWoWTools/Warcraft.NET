using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.TEX.Chunks
{
    /// <summary>
    /// TXMD Chunk - Contains texture binary data from the lowest 7 mipmap.
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class TXMD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "TXMD";

        /// <summary>
        /// Gets or sets model extents. Same count as <see cref="TXMD"/>
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXMD"/> class.
        /// </summary>
        public TXMD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TXMD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public TXMD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            Data = inData;
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
            return Data;
        }
    }
}