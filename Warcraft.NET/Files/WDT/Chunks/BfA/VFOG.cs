using Warcraft.NET.Files.Interfaces;
using System.IO;
using Warcraft.NET.Files.WDT.Flags;
using System.Collections.Generic;
using Warcraft.NET.Files.WDT.Entries.BfA;

namespace Warcraft.NET.Files.WDT.Chunks
{
    /// <summary>
    /// VFOG Chunk
    /// </summary>
    public class VFOG : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "VFOG";

        /// <summary>
        /// VFOG Entries
        /// </summary>
        public List<VFOGEntry> Entries = [];


        /// <summary>
        /// Initializes a new instance of the <see cref="MPHD"/> class.
        /// </summary>
        public VFOG()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPHD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public VFOG(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var VFOGCount = br.BaseStream.Length / VFOGEntry.GetSize();

                for (var i = 0; i < VFOGCount; ++i)
                {
                    Entries.Add(new VFOGEntry(br.ReadBytes(VFOGEntry.GetSize())));
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
                foreach (VFOGEntry entry in Entries)
                        bw.Write(entry.Serialize());

                return ms.ToArray();
            }
        }
    }
}