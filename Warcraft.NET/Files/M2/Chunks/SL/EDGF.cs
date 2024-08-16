using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class EDGF : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "EDGF";

        /// <summary>
        /// Gets or sets the EdgeFade Entries
        /// </summary>
        public List<EDGFEntry> EDGFEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="EDGF"/>
        /// </summary>
        public EDGF() { }

        /// <summary>
        /// Initializes a new instance of <see cref="EDGF"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public EDGF(byte[] inData) => LoadBinaryData(inData);

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
                    var EDGFcount = br.BaseStream.Length / EDGFEntry.GetSize();
                    for (var i = 0; i < EDGFcount; ++i)
                    {
                        EDGFEntries.Add(new EDGFEntry(br.ReadBytes(EDGFEntry.GetSize())));
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
                foreach (EDGFEntry obj in EDGFEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}