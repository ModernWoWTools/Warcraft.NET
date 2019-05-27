using System.IO;

namespace Warcraft.NET.Files.ADT.Entrys.Wotlk
{
    /// <summary>
    /// Pointers to MCNK chunks and their sizes.
    /// </summary>
    public class MCINEntry
    {
        /// <summary>
        /// Offset of the MCNK Chunk
        /// </summary>
        public uint Adress { get; set; }

        /// <summary>
        /// The number of bytes from the <see cref="MCNK"/> Chunk
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        /// always 0. only set in the client., FLAG_LOADED = 1
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// not in the adt file. client use only
        /// </summary>
        public uint AsyncID { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCINEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MCINEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                Adress = br.ReadUInt32();
                Size = br.ReadUInt32();
                Flags = br.ReadUInt32();
                AsyncID = br.ReadUInt32();
            }
        }

        /// <inheritdoc/>
        public static int GetSize()
        {
            return sizeof(uint) * 4;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Adress);
                bw.Write(Size);
                bw.Write(Flags);
                bw.Write(AsyncID);

                return ms.ToArray();
            }
        }
    }
}
