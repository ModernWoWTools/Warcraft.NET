using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    public class SKID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SKID";

        /// <summary>
        /// Gets or sets the Skin FileDataId
        /// </summary>
        public uint SkinFileDataId { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of <see cref="SKID"/>
        /// </summary>
        public SKID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SKID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SKID(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                SkinFileDataId = br.ReadUInt32();
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(SkinFileDataId);

                return ms.ToArray();
            }
        }
    }
}
