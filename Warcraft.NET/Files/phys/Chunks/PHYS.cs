using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class PHYS : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PHYS";

        /// <summary>
        /// Gets or Sets the version of the physics
        /// </summary>
        public ushort version;

        /// <summary>
        /// Initializes a new instance of <see cref="PHYS"/>
        /// </summary>
        public PHYS() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PHYS"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PHYS(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                version = br.ReadUInt16();
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(version);
                return ms.ToArray();
            }
        }
    }
}
