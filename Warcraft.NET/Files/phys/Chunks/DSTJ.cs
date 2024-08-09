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
    public class DSTJ : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "DSTJ";

        public List<DSTJEntry> DSTJEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="DSTJ"/>
        /// </summary>
        public DSTJ() { }

        /// <summary>
        /// Initializes a new instance of <see cref="DSTJ"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public DSTJ(byte[] inData) => LoadBinaryData(inData);

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
                var DSTJcount = br.BaseStream.Length / DSTJEntry.GetSize();

                for (var i = 0; i < DSTJcount; ++i)
                {
                    DSTJEntries.Add(new DSTJEntry(br.ReadBytes(DSTJEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (DSTJEntry obj in DSTJEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }


        }
    }
}
