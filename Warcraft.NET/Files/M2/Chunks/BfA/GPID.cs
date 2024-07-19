using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.BfA
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLegion, AutoDocChunkVersionHelper.VersionBeforeBfA)]
    public class GPID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "GPID";

        /// <summary>
        /// Gets or Sets the geometry particle FileDataIds
        /// </summary>
        public List<uint> GeometryParticleFileDataIds { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of <see cref="GPID"/>
        /// </summary>
        public GPID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="GPID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public GPID(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                uint nGeometryParticle = (uint)inData.Length / 4;

                for (var i = 0; i < nGeometryParticle; i++)
                    GeometryParticleFileDataIds.Add(br.ReadUInt32());
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (uint geometryParticleFileDataId in GeometryParticleFileDataIds)
                    bw.Write(geometryParticleFileDataId);

                return ms.ToArray();
            }
        }
    }
}
