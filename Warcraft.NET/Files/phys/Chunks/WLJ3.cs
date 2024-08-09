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
    public class WLJ3 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "WLJ3";

        public List<WLJ3Entry> WLJ3Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="WLJ3"/>
        /// </summary>
        public WLJ3() { }

        /// <summary>
        /// Initializes a new instance of <see cref="WLJ3"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public WLJ3(byte[] inData) => LoadBinaryData(inData);

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
                var WLJ3count = br.BaseStream.Length / WLJ3Entry.GetSize();

                for (var i = 0; i < WLJ3count; ++i)
                {
                    WLJ3Entries.Add(new WLJ3Entry(br.ReadBytes(WLJ3Entry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (WLJ3Entry obj in WLJ3Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }


        }
    }
}
