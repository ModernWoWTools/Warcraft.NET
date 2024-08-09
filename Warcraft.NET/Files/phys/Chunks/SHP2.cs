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
    public class SHP2 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SHP2";

        public List<SHP2Entry> SHP2Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="SHP2"/>
        /// </summary>
        public SHP2() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SHP2"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SHP2(byte[] inData) => LoadBinaryData(inData);

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
                var SHP2count = br.BaseStream.Length / SHP2Entry.GetSize();

                for (var i = 0; i < SHP2count; ++i)
                {
                    SHP2Entries.Add(new SHP2Entry(br.ReadBytes(SHP2Entry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (SHP2Entry obj in SHP2Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }


        }
    }
}
