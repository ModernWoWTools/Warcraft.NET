using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MCMK.SubChunks
{
    /// <summary>
    /// MCAL Chunk - Contains alpha map data in one of three forms - uncompressed 2048, uncompressed 4096 and compressed.
    /// </summary>
    public class MCAL : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCAL";

        /// <summary>
        /// Holds unformatted data contained in the chunk.
        /// </summary>
        private byte[] Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MCAL"/> class.
        /// </summary>
        public MCAL()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCAL"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCAL(byte[] inData)
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