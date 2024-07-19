using Warcraft.NET.Files.Interfaces;
using System.IO;
using Warcraft.NET.Files.WDT.Entries;
using Warcraft.NET.Attribute;

namespace Warcraft.NET.Files.WDT.Chunks
{
    /// <summary>
    /// MAIN Chunk - Contains file ids for map files
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAll)]
    public class MAIN : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MAIN";

        public MAINEntry[,] Entries = new MAINEntry[64, 64];

        /// <summary>
        /// Initializes a new instance of the <see cref="MAIN"/> class.
        /// </summary>
        public MAIN()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAIN"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MAIN(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        Entries[x, y] = new MAINEntry(br.ReadBytes(MAINEntry.GetSize()));
                    }
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
                for (int y = 0; y < 64; y++)
                {
                    for (int x = 0; x < 64; x++)
                    {
                        bw.Write(Entries[x, y].Serialize());
                    }
                }

                return ms.ToArray();
            }
        }
    }
}