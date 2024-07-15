using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WDT.Chunks.Legion
{
    /// <summary>
    /// MTEX Chunk - Contains textures used for lights
    /// </summary>
    public class MTEX : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MTEX";

        public List<uint> Entries = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MTEX"/> class.
        /// </summary>
        public MTEX()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MTEX"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MTEX(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var textureCount = br.BaseStream.Length / 4;

                for (var i = 0; i < textureCount; ++i)
                {
                    Entries.Add(br.ReadUInt32());
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
                foreach (var texture in Entries)
                {
                    bw.Write(texture);
                }

                return ms.ToArray();
            }
        }
    }
}
