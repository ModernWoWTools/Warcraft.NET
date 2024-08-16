using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.BfA
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLegion, AutoDocChunkVersionHelper.VersionBeforeBfA)]
    public class LDV1 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "LDV1";

        /// <summary>
        /// Gets or sets the Lod Data Version 1 Entries
        /// </summary>
        public List<LDV1Entry> LDV1Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="LDV1"/>
        /// </summary>
        public LDV1() { }

        /// <summary>
        /// Initializes a new instance of <see cref="LDV1"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public LDV1(byte[] inData) => LoadBinaryData(inData);

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
                    var LDV1count = br.BaseStream.Length / LDV1Entry.GetSize();
                    for (var i = 0; i < LDV1count; ++i)
                    {
                        LDV1Entries.Add(new LDV1Entry(br.ReadBytes(LDV1Entry.GetSize())));
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
                foreach (LDV1Entry obj in LDV1Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}