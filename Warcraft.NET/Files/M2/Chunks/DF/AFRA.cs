using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.DF
{
    public class AFRA : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "AFRA";

        /// <summary>
        /// Gets or sets the full data (deserialization NYI)
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// Initializes a new instance of <see cref="AFRA"/>
        /// </summary>
        public AFRA() { }

        /// <summary>
        /// Initializes a new instance of <see cref="AFRA"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public AFRA(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            Data = inData;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            return Data;
        }
    }
}
