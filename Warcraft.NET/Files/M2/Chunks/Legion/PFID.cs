using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    public class PFID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PFID";

        /// <summary>
        /// Gets or Sets the Phys FileDataIds
        /// </summary>
        public List<uint> PhysFileDataIds { get; set; } = new List<uint>();

        /// <summary>
        /// Initializes a new instance of <see cref="PFID"/>
        /// </summary>
        public PFID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PFID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PFID(byte[] inData) => LoadBinaryData(inData);

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
                uint nPhys = (uint)inData.Length / 4;

                for (var i = 0; i < nPhys; i++)
                    PhysFileDataIds.Add(br.ReadUInt32());
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach(uint physFileDataId in PhysFileDataIds)
                    bw.Write(physFileDataId);

                return ms.ToArray();
            }
        }
    }
}
