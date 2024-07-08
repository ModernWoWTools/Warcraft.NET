using System.IO;
using Warcraft.NET.Files.WDT.Flags;

namespace Warcraft.NET.Files.WDT.Entries
{
    /// <summary>
    /// Contains tile information
    /// </summary>
    public class MAINEntry
    {
        /// <summary>
        /// Tile Flags
        /// </summary>
        public MAINFlags Flags { get; set; } = 0;

        /// <summary>
        /// Only set during runtime
        /// </summary>
        public uint AsyncId { get; set; } = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MAINEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MAINEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    Flags = (MAINFlags)br.ReadUInt32();
                    AsyncId = br.ReadUInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 8;
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
                bw.Write((uint)Flags);
                bw.Write(AsyncId);

                return ms.ToArray();
            }
        }
    }
}
