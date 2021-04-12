using Warcraft.NET.Files.Interfaces;
using System.IO;
using System.Collections.Generic;
using Warcraft.NET.Files.WDT.Entrys.WoD;

namespace Warcraft.NET.Files.WDT.Chunks.WoD
{
    /// <summary>
    /// MAOI Chunk - Contains occlusion index information
    /// </summary>
    public class MAOI : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MAOI";

        public List<MAOIEntry> Entrys = new List<MAOIEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOI"/> class.
        /// </summary>
        public MAOI()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOI"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MAOI(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var MAOICount = br.BaseStream.Length / MAOIEntry.GetSize();

                for (var i = 0; i < MAOICount; ++i)
                {
                    Entrys.Add(new MAOIEntry(br.ReadBytes(MAOIEntry.GetSize())));
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
                foreach (MAOIEntry entry in Entrys)
                    bw.Write(entry.Serialize());

                return ms.ToArray();
            }
        }
    }
}