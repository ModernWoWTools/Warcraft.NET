using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.SL
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class DETL : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "DETL";

        /// <summary>
        /// Gets or sets the DETL Entries
        /// </summary>
        public List<DETLEntry> DETLEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="DETL"/>
        /// </summary>
        public DETL() { }

        /// <summary>
        /// Initializes a new instance of <see cref="DETL"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public DETL(byte[] inData) => LoadBinaryData(inData);

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
                    var DETLcount = br.BaseStream.Length / DETLEntry.GetSize();
                    for (var i = 0; i < DETLcount; ++i)
                    {
                        DETLEntries.Add(new DETLEntry(br.ReadBytes(DETLEntry.GetSize())));
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
                foreach (DETLEntry obj in DETLEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}