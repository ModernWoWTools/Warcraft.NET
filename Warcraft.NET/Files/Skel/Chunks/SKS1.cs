using System;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.Skel.Chunks
{
    public class SKS1 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SKS1";

        /// <summary>
        /// Gets or Sets the global sequences
        /// </summary>
        public List<SequenceStruct> Sequences { get; set; }

        /// <summary>
        /// Gets or Sets the animations
        /// </summary>
        public List<AnimationStruct> Animations { get; set; }

        /// <summary>
        /// Gets or Sets the animation lookups
        /// </summary>
        public List<AnimationLookupStruct> AnimationLookups { get; set; }

        /// <summary>
        /// Gets or Sets the Unknown field (padding?)
        /// </summary>
        public UInt64 Unk { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SKS1"/>
        /// </summary>
        public SKS1() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SKS1"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SKS1(byte[] inData) => LoadBinaryData(inData);

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
                Sequences = ReadStructList<SequenceStruct>(br.ReadUInt32(), br.ReadUInt32(), br);
                Animations = ReadStructList<AnimationStruct>(br.ReadUInt32(), br.ReadUInt32(), br);
                AnimationLookups = ReadStructList<AnimationLookupStruct>(br.ReadUInt32(), br.ReadUInt32(), br);
                Unk = br.ReadUInt64();
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
