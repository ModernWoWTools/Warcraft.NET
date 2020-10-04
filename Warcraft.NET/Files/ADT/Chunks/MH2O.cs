using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks
{
    /// <summary>
    /// MH2O Chunk - Fake chunk
    /// </summary>
    public class MH2O : IIFFChunk, IBinarySerializable
    {
        public const string Signature = "MH2O";

        private byte[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2O"/> class.
        /// </summary>
        public MH2O()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2O"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MH2O(byte[] inData)
        {
            data = inData;
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
        public void LoadBinaryData(byte[] inData)
        {
            data = inData;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            return data;
        }
    }
}
