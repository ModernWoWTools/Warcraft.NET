using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.BfA
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLegion, AutoDocChunkVersionHelper.VersionBeforeBfA)]
    public class TXID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "TXID";

        /// <summary>
        /// Gets or Sets the Texture FileDataIds
        /// </summary>
        public List<uint> TextureFileDataIds { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of <see cref="TXID"/>
        /// </summary>
        public TXID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="TXID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public TXID(byte[] inData) => LoadBinaryData(inData);

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
                uint nTextures = (uint)inData.Length / 4;

                for (var i = 0; i < nTextures; i++)
                    TextureFileDataIds.Add(br.ReadUInt32());
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach(uint TextureFileDataId in TextureFileDataIds)
                    bw.Write(TextureFileDataId);

                return ms.ToArray();
            }
        }
    }
}
