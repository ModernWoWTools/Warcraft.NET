using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys.Entries;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class JOIN : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "JOIN";

        /// <summary>
        /// sets or gets the joints
        /// </summary>
        public List<JOINEntry> JOINEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="JOIN"/>
        /// </summary>
        public JOIN() { }

        /// <summary>
        /// Initializes a new instance of <see cref="JOIN"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public JOIN(byte[] inData) => LoadBinaryData(inData);

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
                var JOINcount = br.BaseStream.Length / JOINEntry.GetSize();
                for (var i = 0; i < JOINcount; ++i)
                {
                    JOINEntries.Add(new JOINEntry(br.ReadBytes(JOINEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (JOINEntry obj in JOINEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}
