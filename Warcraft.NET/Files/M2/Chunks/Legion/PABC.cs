using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class PABC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PABC";

        /// <summary>
        /// Gets or sets the ParentSequenceBounds Entries
        /// </summary>
        public List<ushort> PABCEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="PABC"/>
        /// </summary>
        public PABC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PABC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PABC(byte[] inData) => LoadBinaryData(inData);

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
                    var PABCcount = br.BaseStream.Length / 2;
                    for (var i = 0; i < PABCcount; ++i)
                    {
                        PABCEntries.Add(br.ReadUInt16());
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
                foreach (ushort obj in PABCEntries)
                {
                    bw.Write(obj);
                }
                return ms.ToArray();
            }
        }
    }
}