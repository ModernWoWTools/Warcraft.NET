using System.IO;

namespace Warcraft.NET.Files.WMO.Entries
{
    /// <summary>
    /// An entry struct containing information about doodad sets.
    /// </summary>
    public class MODSEntry
    {
        /// <summary>
        /// Gets or sets doodad set name.
        /// </summary>
        public char[] Name { get; set; }

        /// <summary>
        /// Gets or sets doodad start index
        /// </summary>
        public uint StartIndex { get; set; }

        /// <summary>
        /// Gets or sets doodad count
        /// </summary>
        public uint Count { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODSEntry"/> class.
        /// </summary>
        public MODSEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODSEntry"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MODSEntry(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public static int GetSize()
        {
            return 32;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Name = br.ReadChars(0x14);
                StartIndex = br.ReadUInt32();
                Count = br.ReadUInt32();

                // Padding
                ms.Seek(4, SeekOrigin.Current);
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Name);
                bw.Write(StartIndex);
                bw.Write(Count);

                // Padding
                bw.Write(new char[4]);
                return ms.ToArray();
            }
        }
    }
}
