using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class AFID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "AFID";

        /// <summary>
        /// Gets or Sets the Anim FileDataIds
        /// </summary>
        public List<AFIDEntry> AFIDEntries { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of <see cref="AFID"/>
        /// </summary>
        public AFID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="AFID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public AFID(byte[] inData) => LoadBinaryData(inData);

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
                var animCount = br.BaseStream.Length / AFIDEntry.GetSize();

                for (var i = 0; i < animCount; ++i)
                {
                    AFIDEntries.Add(new AFIDEntry(br.ReadBytes(AFIDEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (AFIDEntry obj in AFIDEntries)
                {
                    bw.Write(obj.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}