using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class BFID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "BFID";

        /// <summary>
        /// Gets or Sets the Bone FileDataIds
        /// </summary>
        public List<uint> BoneFileDataIds { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of <see cref="BFID"/>
        /// </summary>
        public BFID() { }

        /// <summary>
        /// Initializes a new instance of <see cref="BFID"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public BFID(byte[] inData) => LoadBinaryData(inData);

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
                uint nBone = (uint)inData.Length / 4;
                for (var i = 0; i < nBone; i++)
                    BoneFileDataIds.Add(br.ReadUInt32());
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach(uint boneFileDataId in BoneFileDataIds)
                    bw.Write(boneFileDataId);
                return ms.ToArray();
            }
        }
    }
}