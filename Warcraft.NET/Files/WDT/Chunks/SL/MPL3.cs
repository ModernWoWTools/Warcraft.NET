using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.WDT.Entries.SL;

namespace Warcraft.NET.Files.WDT.Chunks.SL
{
    /// <summary>
    /// MPL3 Chunk - Contains Shadowlands point light information
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class MPL3 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MPL3";

        public List<MPL3Entry> Entries = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MPL3"/> class.
        /// </summary>
        public MPL3()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPL3"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MPL3(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mpl3Count = br.BaseStream.Length / MPL3Entry.GetSize();

                for (var i = 0; i < mpl3Count; ++i)
                {
                    Entries.Add(new MPL3Entry(br.ReadBytes(MPL3Entry.GetSize())));
                }
            }
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (MPL3Entry mpl3Entry in Entries)
                {
                    bw.Write(mpl3Entry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}
