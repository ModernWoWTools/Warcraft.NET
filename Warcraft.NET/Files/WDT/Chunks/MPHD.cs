using Warcraft.NET.Files.Interfaces;
using System.IO;
using Warcraft.NET.Files.WDT.Flags;

namespace Warcraft.NET.Files.WDT.Chunks
{
    /// <summary>
    /// MPHD Chunk - Contains the WDT flags.
    /// </summary>
    public class MPHD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MPHD";

        /// <summary>
        /// Gets or sets the WDT version.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Gets or sets the WDT flags.
        /// </summary>
        public MPHDFlags Flags { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="MPHD"/> class.
        /// </summary>
        public MPHD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPHD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MPHD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Version = br.ReadUInt32();
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
                bw.Write(Version);

                return ms.ToArray();
            }
        }
    }
}