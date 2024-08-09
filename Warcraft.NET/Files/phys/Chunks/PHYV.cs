using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.phys.Entries;

namespace Warcraft.NET.Files.phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class PHYV : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PHYV";

        /// <summary>
        /// Gets or Sets the version of the physics
        /// </summary>
        public float[] values;

        /// <summary>
        /// Initializes a new instance of <see cref="PHYV"/>
        /// </summary>
        public PHYV() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PHYV"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PHYV(byte[] inData) => LoadBinaryData(inData);

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
                var PHYVcount = br.BaseStream.Length / 24;
                values = new float[PHYVcount * 6];
                for (var i = 0; i < PHYVcount; ++i)
                {
                    values[i] = br.ReadSingle();
                    values[i+1] = br.ReadSingle();
                    values[i+2] = br.ReadSingle();
                    values[i+3] = br.ReadSingle();
                    values[i+4] = br.ReadSingle();
                    values[i+5] = br.ReadSingle();
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (float f in values)
                {
                    bw.Write(f);
                }
                return ms.ToArray();
            }
        }

    }
}
