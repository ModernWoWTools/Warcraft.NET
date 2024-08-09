using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;
using System.Collections.Generic;
using Warcraft.NET.Files.phys.Entries;

namespace Warcraft.NET.Files.phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class CAPS : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "CAPS";

        public List<CAPSEntry> CAPSEntries = new();


        /// <summary>
        /// Initializes a new instance of <see cref="CAPS"/>
        /// </summary>
        public CAPS() { }

        /// <summary>
        /// Initializes a new instance of <see cref="CAPS"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public CAPS(byte[] inData) => LoadBinaryData(inData);

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
                var capscount = br.BaseStream.Length / CAPSEntry.GetSize();

                for (var i = 0; i < capscount; ++i)
                {
                    CAPSEntries.Add(new CAPSEntry(br.ReadBytes(CAPSEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (CAPSEntry obj in CAPSEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }


        }
    }
}
