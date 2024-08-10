using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.SL
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class WFV3 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "WFV3";

        /// <summary>
        /// Gets or sets the Skin FileDataId
        /// </summary>
        public List<WFV3Entry> WFV3Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="WFV3"/>
        /// </summary>
        public WFV3() { }

        /// <summary>
        /// Initializes a new instance of <see cref="WFV3"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public WFV3(byte[] inData) => LoadBinaryData(inData);

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
                    var WFV3count = br.BaseStream.Length / WFV3Entry.GetSize();
                    for (var i = 0; i < WFV3count; ++i)
                    {
                        WFV3Entries.Add(new WFV3Entry(br.ReadBytes(WFV3Entry.GetSize())));
                    }
                }
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (WFV3Entry obj in WFV3Entries)
                {
                    bw.Write(obj.Serialize());
                }

                return ms.ToArray();
            }
        }

    }
}