using System.IO;

namespace Warcraft.NET.Files.ADT.Entries.Legion
{
    /// <summary>
    /// LOD quad tree entry.
    /// </summary>
    public class MLNDEntry
    {
        /// <summary>
        /// MLVI used element offset
        /// </summary>
        public uint MLVIOffset { get; set; }

        /// <summary>
        /// MLVI used element length
        /// </summary>
        public uint MLVILength { get; set; }

        /// <summary>
        /// Unknown 0.
        /// </summary>
        public uint Unknown0 { get; set; }

        /// <summary>
        /// Unknown 1.
        /// </summary>
        public uint Unknown1 { get; set; }

        /// <summary>
        /// Indexes into MLND for child leaves.
        /// </summary>
        public ushort[] Indices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLNDEntry"/> class.
        /// </summary>
        public MLNDEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLNDEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MLNDEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                MLVIOffset = br.ReadUInt32();
                MLVILength = br.ReadUInt32();
                Unknown0 = br.ReadUInt32();
                Unknown1 = br.ReadUInt32();
                Indices = new ushort[4];
                for (var i = 0; i < 4; ++i)
                {
                    Indices[i] = br.ReadUInt16();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 24;
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(MLVIOffset);
                bw.Write(MLVILength);
                bw.Write(Unknown0);
                bw.Write(Unknown1);
                foreach (var index in Indices)
                {
                    bw.Write(index);
                }
                return ms.ToArray();
            }
        }
    }
}