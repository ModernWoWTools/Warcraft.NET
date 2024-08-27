using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys.Entries;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class SPHJ : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SPHJ";

        /// <summary>
        /// sets or gets the spherical joints
        /// </summary>
        public List<SPHJEntry> SPHJEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="SPHJ"/>
        /// </summary>
        public SPHJ() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SPHJ"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SPHJ(byte[] inData) => LoadBinaryData(inData);

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
                var SPHJcount = br.BaseStream.Length / SPHJEntry.GetSize();
                for (var i = 0; i < SPHJcount; ++i)
                {
                    SPHJEntries.Add(new SPHJEntry(br.ReadBytes(SPHJEntry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (SPHJEntry obj in SPHJEntries)
                {
                    bw.Write(obj.Serialize());
                }
                return ms.ToArray();
            }
        }
    }
}
