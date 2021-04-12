using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WDL.Chunks
{
    /// <summary>
    /// MAOE Chunk - Represents the ocean texture
    /// </summary>
    public class MAOE : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MAOE";

        /// <summary>
        /// Gets or Sets chunk data.
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOE"/> class.
        /// </summary>
        public MAOE() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOE"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MAOE(byte[] inData)
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
            return GetSizeStatic();
        }

        /// <inheritdoc/>
        public static uint GetSizeStatic()
        {
            return 0x20;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            return Data;
        }
    }
}