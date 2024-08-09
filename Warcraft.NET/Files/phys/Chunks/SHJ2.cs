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
    public class SHJ2 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SHJ2";

        public List<SHJ2Entry> SHJ2Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="SHJ2"/>
        /// </summary>
        public SHJ2() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SHJ2"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SHJ2(byte[] inData) => LoadBinaryData(inData);

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
                var SHJ2count = br.BaseStream.Length / SHJ2Entry.GetSize();

                for (var i = 0; i < SHJ2count; ++i)
                {
                    SHJ2Entries.Add(new SHJ2Entry(br.ReadBytes(SHJ2Entry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (SHJ2Entry obj in SHJ2Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }


        }
    }
}
