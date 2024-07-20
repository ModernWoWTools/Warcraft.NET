using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;

namespace Warcraft.NET.Files.ADT.Chunks
{
    /// <summary>
    /// MWMO Chunk - Contains a list of all referenced WMO models in this ADT.
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionBeforeSL, AutoDocChunkVersionHelper.VersionAfterBfA)]
    public class MWMO : IIFFChunk, IBinarySerializable
    {
        public const string Signature = "MWMO";

        /// <summary>
        /// Gets or sets a list of full paths to the M2 models referenced in this ADT.
        /// </summary>
        public List<string> Filenames { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MWMO"/> class.
        /// </summary>
        public MWMO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MWMO"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MWMO(byte[] inData)
        {
            LoadBinaryData(inData);
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
