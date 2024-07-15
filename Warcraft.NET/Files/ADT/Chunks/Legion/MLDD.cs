using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLDD Chunk - Level of detail offset information
    /// </summary>
    public class MLDD : MDDF, IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        new public const string Signature = "MLDD";

        /// <summary>
        /// Initializes a new instance of the <see cref="MLDD"/> class.
        /// </summary>
        public MLDD() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLDD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLDD(byte[] inData) : base(inData)
        {
        }

        /// <inheritdoc/>
        new public string GetSignature()
        {
            return Signature;
        }
    }
}