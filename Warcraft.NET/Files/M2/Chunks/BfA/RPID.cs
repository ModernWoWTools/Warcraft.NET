using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.BfA
{
    public class RPID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "RPID";

        /// <summary>
        /// Gets or Sets the recursive particle FileDataIds
        /// </summary>
        public List<uint> RecursiveParticleFileDataIds { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of <see cref="RPID"/>
        /// </summary>
        public RPID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="RPID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public RPID(byte[] inData) => LoadBinaryData(inData);

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
                uint nRecursiveParticle = (uint)inData.Length / 4;

                for (var i = 0; i < nRecursiveParticle; i++)
                    RecursiveParticleFileDataIds.Add(br.ReadUInt32());
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (uint recursiveParticleFileDataId in RecursiveParticleFileDataIds)
                    bw.Write(recursiveParticleFileDataId);

                return ms.ToArray();
            }
        }
    }
}
