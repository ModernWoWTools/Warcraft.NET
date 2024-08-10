using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.BfA
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLegion, AutoDocChunkVersionHelper.VersionBeforeBfA)]
    public class WFV1 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "WFV1";

        public byte[] data;

        /// <summary>
        /// Initializes a new instance of <see cref="WFV1"/>
        /// </summary>
        public WFV1() { }

        /// <summary>
        /// Initializes a new instance of <see cref="WFV1"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public WFV1(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            {
                using (var ms = new MemoryStream(inData))
                using (var br = new BinaryReader(ms))
                {
                    data = inData;
                }
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