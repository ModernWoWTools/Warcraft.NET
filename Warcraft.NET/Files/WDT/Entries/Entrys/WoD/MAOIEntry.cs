using System.IO;

namespace Warcraft.NET.Files.WDT.Entrys.WoD
{
    /// <summary>
    /// An entry struct containing occlusion index information
    /// </summary>
    public class MAOIEntry
    {
        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileX { get; set; }

        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileY { get; set; }

        /// <summary>
        /// Offset in MAOH chunk
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Size always (17*17+16*16)*2
        /// </summary>
        public uint Size { get; set; }

        public MAOIEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOIEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MAOIEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    TileX = br.ReadUInt16();
                    TileY = br.ReadUInt16();
                    Offset = br.ReadUInt32();
                    Size = br.ReadUInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 12;
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
                bw.Write(TileX);
                bw.Write(TileY);
                bw.Write(Offset);
                bw.Write(Size);

                return ms.ToArray();
            }
        }
    }
}
