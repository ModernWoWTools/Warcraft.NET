using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.ADT.Flags;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLLD Chunk - Level of detail 
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MLLD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLLD";

        /// <summary>
        /// Flags.
        /// </summary>
        public MLLDFlags Flags { get; set; }

        /// <summary>
        /// Size of the first (possibly compressed) depth chunk.
        /// </summary>
        public ushort DepthChunkSize { get; set; }

        /// <summary>
        /// Approximate size of the second (possibly compressed) alpha chunk. Possibly off by 4-7 bytes.
        /// </summary>
        public ushort ApproxAlphaChunkSize { get; set; }

        /// <summary>
        /// Data of the first (possibly compressed) depth chunk.
        /// </summary>
        public byte[] DepthChunkData { get; set; }

        /// <summary>
        /// Data of the second (possibly compressed) alpha chunk.
        /// Note: there might be 4-7 extra bytes at the end due to not full understand of this chunk.
        /// </summary>
        public byte[] AlphaChunkData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLD"/> class.
        /// </summary>
        public MLLD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLLD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Flags = (MLLDFlags)br.ReadUInt32();
                DepthChunkSize = br.ReadUInt16();
                ApproxAlphaChunkSize = br.ReadUInt16();
                DepthChunkData = br.ReadBytes(DepthChunkSize);
                AlphaChunkData = br.ReadBytes((int)(ms.Length - ms.Position));
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
                bw.Write((uint)Flags);
                bw.Write(DepthChunkSize);
                bw.Write(ApproxAlphaChunkSize);
                bw.Write(DepthChunkData);
                bw.Write(AlphaChunkData);
                return ms.ToArray();
            }
        }
    }
}