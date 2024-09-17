using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class EXPT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "EXPT";

        /// <summary>
        /// Gets or sets the EXPT Entries
        /// </summary>
        public List<EXPTEntry> EXP2Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="EXPT"/>
        /// </summary>
        public EXPT() { }

        /// <summary>
        /// Initializes a new instance of <see cref="EXPT"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public EXPT(byte[] inData) => LoadBinaryData(inData);

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
                    var exp2count = br.BaseStream.Length / EXPTEntry.GetSize();
                    for (var i = 0; i < exp2count; ++i)
                    {
                        EXP2Entries.Add(new EXPTEntry(br.ReadBytes(EXPTEntry.GetSize())));
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
                foreach (EXPTEntry obj in EXP2Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}