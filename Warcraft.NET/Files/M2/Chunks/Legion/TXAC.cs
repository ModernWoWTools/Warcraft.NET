using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class TXAC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "TXAC";

        /// <summary>
        /// Gets or sets the Skin FileDataId
        /// </summary>
        public byte[] unk;

        /// <summary>
        /// Initializes a new instance of <see cref="TXAC"/>
        /// </summary>
        public TXAC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="TXAC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public TXAC(byte[] inData) => LoadBinaryData(inData);

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
                unk = br.ReadBytes((int)br.BaseStream.Length);
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(unk);

                return ms.ToArray();
            }
        }
    }
}
