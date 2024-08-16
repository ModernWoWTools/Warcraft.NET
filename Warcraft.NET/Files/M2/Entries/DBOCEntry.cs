using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class DBOCEntry
    {
        /// <summary>
        /// unknown float
        /// </summary>
        public float Unk0;
        /// <summary>
        /// unknown float
        /// </summary>
        public float Unk1;
        /// <summary>
        /// unknown int
        /// </summary>
        public uint Unk2;
        /// <summary>
        /// unknown int
        /// </summary>
        public uint Unk3;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBOCEntry"/> class.
        /// </summary>
        public DBOCEntry(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="DBOCEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public DBOCEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                Unk0 = br.ReadSingle();
                Unk1 = br.ReadSingle();
                Unk2 = br.ReadUInt32();
                Unk3 = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of a DBOC Entry
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 16;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(Unk0);
                    bw.Write(Unk1);
                    bw.Write(Unk2);
                    bw.Write(Unk3);
                }
                return ms.ToArray();
            }
        }
    }
}
