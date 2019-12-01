using Warcraft.NET.Files.Interfaces;
using System.IO;

namespace Warcraft.NET.Files.WDT.Chunks
{
    /// <summary>
    /// MVER Chunk - Contains the WDT version.
    /// </summary>
    public class MVER : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MVER";

        /// <summary>
        /// Gets or sets the WDT version.
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Gets or sets lgt file id
        /// </summary>
        uint LgtFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets ooc file id
        /// </summary>
        uint OccFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets fogs file id
        /// </summary>
        uint FogsFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets mpv file id
        /// </summary>
        uint MpvFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets tex file id
        /// </summary>
        uint TexFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets wdl file id
        /// </summary>
        uint WdlFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets pd4 file id
        /// </summary>
        uint Pd4FileID { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MVER"/> class.
        /// </summary>
        public MVER()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MVER"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MVER(byte[] inData)
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