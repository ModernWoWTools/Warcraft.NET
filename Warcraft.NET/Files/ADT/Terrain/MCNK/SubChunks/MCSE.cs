using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks
{
    /// <summary>
    /// MCSE Chunk - Holds sound emitters.
    /// </summary>
    public class MCSE : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCSE";

        /// <summary>
        /// Gets or sets Vertices
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCSE"/> class.
        /// </summary>
        public MCSE()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCSE"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCSE(byte[] inData)
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
