using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class SFID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SFID";

        /// <summary>
        /// Gets or Sets the Skin FileDataIds
        /// </summary>
        public List<uint> SkinFileDataIds { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of <see cref="SFID"/>
        /// </summary>
        public SFID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SFID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SFID(byte[] inData) => LoadBinaryData(inData);

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
                uint nSkin = (uint)inData.Length / 4;

                for (var i = 0; i < nSkin; i++)
                    SkinFileDataIds.Add(br.ReadUInt32());
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach(uint SkinFileDataId in SkinFileDataIds)
                    bw.Write(SkinFileDataId);

                return ms.ToArray();
            }
        }
    }
}
