using Warcraft.NET.Files.Interfaces;
using System.IO;
using System.Collections.Generic;
using Warcraft.NET.Attribute;

namespace Warcraft.NET.Files.WDT.Chunks.WoD
{
    /// <summary>
    /// MAOH Chunk - Contains occlusion heightmap
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLegion, AutoDocChunkVersionHelper.VersionBeforeBfA)]
    public class MAOH : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MAOH";

        public List<short> InterleavedMap = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOH"/> class.
        /// </summary>
        public MAOH()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOH"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MAOH(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var MAOHCount = br.BaseStream.Length / sizeof(short);

                for (var i = 0; i < MAOHCount; ++i)
                {
                    InterleavedMap.Add(br.ReadInt16());
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
                foreach (short height in InterleavedMap)
                    bw.Write(height);

                return ms.ToArray();
            }
        }
    }
}