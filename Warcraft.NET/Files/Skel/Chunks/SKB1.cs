using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.Skel.Chunks
{
    public class SKB1 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SKB1";

        /// <summary>
        /// Gets or Sets the bones
        /// </summary>
        public List<BoneStruct> Bones { get; set; }

        /// <summary>
        /// Gets or Sets the KeyBoneLookupTable
        /// </summary>
        public List<KeyBoneLookupStruct> KeyBoneLookup { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SKB1"/>
        /// </summary>
        public SKB1() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SKB1"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SKB1(byte[] inData) => LoadBinaryData(inData);

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
                Bones = ReadStructList<BoneStruct>(br.ReadUInt32(), br.ReadUInt32(), br);
                KeyBoneLookup = ReadStructList<KeyBoneLookupStruct>(br.ReadUInt32(), br.ReadUInt32(), br);
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
