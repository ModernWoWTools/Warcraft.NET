using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.WMO.Entries
{
    /// <summary>
    /// An entry struct containing information about doodad sets.
    /// </summary>
    public class MODSEntry
    {
        const short NameByteLength = 0x14;

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
                Name = br.ReadChars(NameByteLength);
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
                if (Name.Length > NameByteLength)
                    throw new InvalidDataException("Name length invalid!");

                List<char> paddedName = new List<char>(Name);
                for (var i = paddedName.Count; i < NameByteLength; i++)
                    paddedName.Add('\0');

                bw.Write(paddedName.ToArray());
                bw.Write(StartIndex);
                bw.Write(Count);

                // Padding
                bw.Write((uint)0);
                return ms.ToArray();
            }
        }
    }
}
