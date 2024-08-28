using System;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class PGD1 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PGD1";

        /// <summary>
        /// Gets or sets the ParticleGeosetData1 Entries
        /// </summary>
        public List<ushort> PGD1Entries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="PGD1"/>
        /// </summary>
        public PGD1() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PGD1"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PGD1(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            {
                using (var ms = new MemoryStream(inData))
                using (var br = new BinaryReader(ms))
                {
                    var PGD1count = br.BaseStream.Length / 2;
                    for (var i = 0; i < PGD1count; ++i)
                    {
                        PGD1Entries.Add(br.ReadUInt16());
                    }
                }
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (UInt16 obj in PGD1Entries)
                {
                    bw.Write(obj);
                }
                return ms.ToArray();
            }
        }
    }
}