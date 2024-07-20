using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;

namespace Warcraft.NET.Files.ADT.Chunks
{
    /// <summary>
    /// MTEX Chunk - Contains a list of all referenced textures in this ADT.
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionBeforeBfA, AutoDocChunkVersionHelper.VersionAfterLegion)]
    public class MTEX : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MTEX";

        /// <summary>
        /// Gets or sets a list of full paths to the textures referenced in this ADT.
        /// </summary>
        public List<string> Filenames { get; set; } = new();

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
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    Filenames.Add(br.ReadNullTerminatedString());
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
                bw.WriteNullTerminatedString(Filenames.ToArray());

                return ms.ToArray();
            }
        }
    }
}