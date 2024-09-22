using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    public class PADC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PADC";

        /// <summary>
        /// Gets or sets the ParentAnimationData (deserialization NYI)
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// Initializes a new instance of <see cref="PADC"/>
        /// </summary>
        public PADC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PADC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PADC(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            Data = inData;
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            return Data;
        }
    }
}