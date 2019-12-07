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
        /// Gets or sets the WDT flags.
        /// </summary>
        public MPHDFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets lgt file id
        /// </summary>
        public uint LgtFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets ooc file id
        /// </summary>
        public uint OccFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets fogs file id
        /// </summary>
        public uint FogsFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets mpv file id
        /// </summary>
        public uint MpvFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets tex file id
        /// </summary>
        public uint TexFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets wdl file id
        /// </summary>
        public uint WdlFileID { get; set; } = 0;

        /// <summary>
        /// Gets or sets pd4 file id
        /// </summary>
        public uint Pd4FileID { get; set; } = 0;

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
                Flags = (MPHDFlags)br.ReadInt32();
                LgtFileID = br.ReadUInt32();
                OccFileID = br.ReadUInt32();
                FogsFileID = br.ReadUInt32();
                MpvFileID = br.ReadUInt32();
                TexFileID = br.ReadUInt32();
                WdlFileID = br.ReadUInt32();
                Pd4FileID = br.ReadUInt32();
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
                bw.Write((int)Flags);
                bw.Write(LgtFileID);
                bw.Write(OccFileID);
                bw.Write(FogsFileID);
                bw.Write(MpvFileID);
                bw.Write(TexFileID);
                bw.Write(WdlFileID);
                bw.Write(Pd4FileID);

                return ms.ToArray();
            }
        }
    }
}