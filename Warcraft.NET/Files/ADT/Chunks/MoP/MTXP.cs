using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.ADT.Entrys.MoP;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.MoP
{
    /// <summary>
    /// MTXP Chunk - Array of flags for entries in MTEX. Always same number of entries as MTEX
    /// </summary>
    public class MTXP : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MTXP";

        /// <summary>
        /// Gets the texture flags.
        /// </summary>
        public List<MTXPEntry> TextureFlagEntries { get; } = new List<MTXPEntry>();

        public MTXP()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MTXP"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MTXP(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var entryCount = br.BaseStream.Length / MTXPEntry.GetSize();

                for (var i = 0; i < entryCount; ++i)
                {
                    TextureFlagEntries.Add(new MTXPEntry(br.ReadBytes(MTXPEntry.GetSize())));
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
                foreach (MTXPEntry textureFlagEntry in TextureFlagEntries)
                {
                    bw.Write(textureFlagEntry.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}