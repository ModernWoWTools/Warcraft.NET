using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys.Entries;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class PRS2 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PRS2";

        /// <summary>
        /// sets or gets the prismatic(V2) joints
        /// </summary>
        public List<PRS2Entry> PRS2Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="PRS2"/>
        /// </summary>
        public PRS2() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PRS2"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PRS2(byte[] inData) => LoadBinaryData(inData);

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
                var PRS2count = br.BaseStream.Length / PRS2Entry.GetSize();

                for (var i = 0; i < PRS2count; ++i)
                {
                    PRS2Entries.Add(new PRS2Entry(br.ReadBytes(PRS2Entry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (PRS2Entry obj in PRS2Entries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}
