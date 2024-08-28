using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys.Entries;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class SPHS : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SPHS";

        /// <summary>
        /// sets or gets a list of sphere shapes
        /// </summary>
        public List<SPHSEntry> Spheres = new();

        /// <summary>
        /// Initializes a new instance of <see cref="SPHS"/>
        /// </summary>
        public SPHS() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SPHS"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SPHS(byte[] inData) => LoadBinaryData(inData);

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
                var SHP2count = br.BaseStream.Length / SHP2Entry.GetSize();

                for (var i = 0; i < SHP2count; ++i)
                {
                    Spheres.Add(new SPHSEntry(br.ReadBytes(SPHSEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (SPHSEntry obj in Spheres)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}
