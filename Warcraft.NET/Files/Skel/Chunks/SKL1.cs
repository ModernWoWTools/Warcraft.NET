using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.Skel.Chunks
{
    public class SKL1 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SKL1";

        /// <summary>
        /// Gets or Sets the Unk Field (always 256 so far, possibly flags?)
        /// </summary>
        public uint Unk;

        /// <summary>
        /// Gets or Sets the KeyBoneLookupTable
        /// </summary>
        public List<char> Name { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SKL1"/>
        /// </summary>
        public SKL1() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SKL1"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SKL1(byte[] inData) => LoadBinaryData(inData);

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
                Unk = br.ReadUInt32();
                Name = ReadStructList<char>(br.ReadUInt32(), br.ReadUInt32(), br);
                uint t = br.ReadUInt32();
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
