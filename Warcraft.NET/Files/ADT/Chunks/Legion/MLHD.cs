using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLHD Chunk - Level of detail header
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MLHD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLHD";

        /// <summary>
        /// Unknown uint
        /// </summary>
        public uint Unknown0 { get; set; }

        /// <summary>
        /// Unknown bounding box
        /// </summary>
        public BoundingBox UnkBoundingBox { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLHD"/> class.
        /// </summary>
        public MLHD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLHD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLHD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Unknown0 = br.ReadUInt32();
                UnkBoundingBox = br.ReadBoundingBox();
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
            return 28;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Unknown0);
                bw.WriteBoundingBox(UnkBoundingBox);
                return ms.ToArray();
            }
        }
    }
}