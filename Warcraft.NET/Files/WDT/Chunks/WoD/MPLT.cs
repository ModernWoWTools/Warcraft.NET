using Warcraft.NET.Files.Interfaces;
using System.IO;
using System.Collections.Generic;
using Warcraft.NET.Files.WDT.Entrys.WoD;

namespace Warcraft.NET.Files.WDT.Chunks.WoD
{
    /// <summary>
    /// MPLT Chunk - Contains WoD light placement information
    /// </summary>
    public class MPLT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MPLT";

        public List<MPLTEntry> Entrys = new List<MPLTEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MPLT"/> class.
        /// </summary>
        public MPLT()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPLT"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MPLT(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var mpltCount = br.BaseStream.Length / MPLTEntry.GetSize();

                for (var i = 0; i < mpltCount; ++i)
                {
                    Entrys.Add(new MPLTEntry(br.ReadBytes(MPLTEntry.GetSize())));
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
                foreach (MPLTEntry entry in Entrys)
                    bw.Write(entry.Serialize());

                return ms.ToArray();
            }
        }
    }
}