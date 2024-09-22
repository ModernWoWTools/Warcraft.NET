using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class PEDC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PEDC";

        /// <summary>
        /// Gets or sets the ParentEventData (deserialization NYI)
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// Initializes a new instance of <see cref="PEDC"/>
        /// </summary>
        public PEDC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PEDC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PEDC(byte[] inData) => LoadBinaryData(inData);

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