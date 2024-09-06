using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.Skel.Chunks
{
    public class SKPD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SKPD";

        /// <summary>
        /// unk
        /// </summary>
        public uint Unk0;

        /// <summary>
        /// unk
        /// </summary>
        public uint Unk1;

        /// <summary>
        /// unk
        /// </summary>
        public uint ParentSkeletonFileID;

        /// <summary>
        /// unk
        /// </summary>
        public uint Unk2;

        /// <summary>
        /// Gets or Sets the KeyBoneLookupTable
        /// </summary>
        public List<char> Name { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SKPD"/>
        /// </summary>
        public SKPD() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SKPD"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SKPD(byte[] inData) => LoadBinaryData(inData);

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
                Unk0 = br.ReadUInt32();
                ParentSkeletonFileID = br.ReadUInt32();
                Unk1 = br.ReadUInt32();
                Unk2 = br.ReadUInt32();
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                return null;
            }
        }

        private List<T> ReadStructList<T>(uint count, uint offset, BinaryReader br) where T : struct
        {
            br.BaseStream.Position = offset;
            List<T> list = new List<T>();

            for (var i = 0; i < count; i++)
                list.Add(br.ReadStruct<T>());

            return list;
        }
    }
}
