using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WMO.WorldMapObject.MOGP.SubChunks
{
    /// <summary>
    /// MODR Chunk - Holds doodad references
    /// </summary>
    public class MODR : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MODR";

        /// <summary>
        /// Gets or sets doodad references
        /// </summary>
        public List<ushort> Doodadreferences { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MODR"/> class.
        /// </summary>
        public MODR()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODR"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MODR(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    Doodadreferences.Add(br.ReadUInt16());
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
            return (uint)(Doodadreferences.Count * 2);
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (ushort doodadreference in Doodadreferences)
                {
                    bw.Write(doodadreference);
                }

                return ms.ToArray();
            }
        }
    }
}
