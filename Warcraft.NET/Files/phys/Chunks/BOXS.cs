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
    public class BOXS : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "BOXS";

        public List<BOXSEntry> BOXSEntries = new();


        /// <summary>
        /// Initializes a new instance of <see cref="BOXS"/>
        /// </summary>
        public BOXS() { }

        /// <summary>
        /// Initializes a new instance of <see cref="BOXS"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public BOXS(byte[] inData) => LoadBinaryData(inData);

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
                var boxscount = br.BaseStream.Length / BOXSEntry.GetSize();

                for (var i = 0; i < boxscount; ++i)
                {
                    BOXSEntries.Add(new BOXSEntry(br.ReadBytes(BOXSEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (BOXSEntry obj in BOXSEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();

            }
        }
    }
}
