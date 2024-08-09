using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class DBOC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "DBOC";

        /// <summary>
        /// Gets or sets the Skin FileDataId
        /// </summary>
        public List<DBOCEntry> DBOCEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="DBOC"/>
        /// </summary>
        public DBOC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="DBOC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public DBOC(byte[] inData) => LoadBinaryData(inData);

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
                    var DBOCcount = br.BaseStream.Length / DBOCEntry.GetSize();
                    for (var i = 0; i < DBOCcount; ++i)
                    {
                        DBOCEntries.Add(new DBOCEntry(br.ReadBytes(DBOCEntry.GetSize())));
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
                foreach (DBOCEntry obj in DBOCEntries)
                {
                    bw.Write(obj.Serialize());
                }

                return ms.ToArray();
            }
        }

    }
}