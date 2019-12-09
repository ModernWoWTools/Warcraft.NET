using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks.BfA
{
    /// <summary>
    /// MHID Chunk - Contains a file id list of all height textures (_h.blp) in this ADT.
    /// </summary>
    public class MHID : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MHID";

        /// <summary>
        /// Gets or sets a list of file id to the height textures reference in this ADT.
        /// </summary>
        public List<uint> Textures { get; set; } = new List<uint>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MHID"/> class.
        /// </summary>
        public MHID()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MHID"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MHID(byte[] inData)
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
                    Textures.Add(br.ReadUInt32());
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
                foreach (uint t in Textures)
                {
                    bw.Write(t);
                }

                return ms.ToArray();
            }
        }
    }
}