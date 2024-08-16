using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys.Entries;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class BDY4 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "BDY4";

        /// <summary>
        /// Gets or Sets the used rigidbodies(V4)
        /// </summary>
        public List<BDY4Entry> BDY4Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="BDY4"/>
        /// </summary>
        public BDY4() { }

        /// <summary>
        /// Initializes a new instance of <see cref="BDY4"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public BDY4(byte[] inData) => LoadBinaryData(inData);

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
                var bdy4count = br.BaseStream.Length / BDY4Entry.GetSize();

                for (var i = 0; i < bdy4count; ++i)
                {
                    BDY4Entries.Add(new BDY4Entry(br.ReadBytes(BDY4Entry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (BDY4Entry obj in BDY4Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }

    }
}
