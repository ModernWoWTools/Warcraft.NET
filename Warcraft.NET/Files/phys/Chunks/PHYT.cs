using System;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.Phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class PHYT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PHYT";

        /// <summary>
        /// Gets or Sets PHYT (unknown)
        /// </summary>
        public uint Phyt;

        /// <summary>
        /// Initializes a new instance of <see cref="PHYT"/>
        /// </summary>
        public PHYT() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PHYT"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PHYT(byte[] inData) => LoadBinaryData(inData);

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
                Phyt = br.ReadUInt32();
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Phyt);
                return ms.ToArray();
            }
        }
    }
}
