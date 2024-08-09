using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.phys.Entries;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class SHOJ : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SHOJ";

        public List<SHOJEntry> SHOJEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="SHOJ"/>
        /// </summary>
        public SHOJ() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SHOJ"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SHOJ(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var SHOJcount = br.BaseStream.Length / SHOJEntry.GetSize();

                for (var i = 0; i < SHOJcount; ++i)
                {
                    SHOJEntries.Add(new SHOJEntry(br.ReadBytes(SHOJEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (SHOJEntry obj in SHOJEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }


        }
    }
}
