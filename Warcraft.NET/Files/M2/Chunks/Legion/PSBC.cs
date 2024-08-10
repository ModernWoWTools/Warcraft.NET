using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class PSBC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PSBC";

        byte[] data;
        /// <summary>
        /// Initializes a new instance of <see cref="PSBC"/>
        /// </summary>
        public PSBC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PSBC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PSBC(byte[] inData) => LoadBinaryData(inData);

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
                data = inData;
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(data);

                return ms.ToArray();
            }
        }
    }
}
