using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys.Entries;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class REV2 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "REV2";

        /// <summary>
        /// sets or gets the revolute(V2) joints
        /// </summary>
        public List<REV2Entry> REV2Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="REV2"/>
        /// </summary>
        public REV2() { }

        /// <summary>
        /// Initializes a new instance of <see cref="REV2"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public REV2(byte[] inData) => LoadBinaryData(inData);

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
                var REV2count = br.BaseStream.Length / REV2Entry.GetSize();

                for (var i = 0; i < REV2count; ++i)
                {
                    REV2Entries.Add(new REV2Entry(br.ReadBytes(REV2Entry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (REV2Entry obj in REV2Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}
