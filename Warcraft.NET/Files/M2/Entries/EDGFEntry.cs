using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class EDGFEntry
    {
        /// <summary>
        /// unknown 2-floats array
        /// </summary>
        public float[] Unk0;

        /// <summary>
        /// unknown float
        /// </summary>
        public float Unk1;

        /// <summary>
        /// unknown 4 byte array
        /// </summary>
        public byte[] Unk2;

        /// <summary>
        /// Initializes a new instance of the <see cref="EDGFEntry"/> class.
        /// </summary>
        public EDGFEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EDGFEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public EDGFEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                Unk0 = new float[2];
                Unk0[0] = br.ReadSingle();
                Unk0[1] = br.ReadSingle();
                Unk1 = br.ReadSingle();
                Unk2 = br.ReadBytes(4);
            }
        }

        /// <summary>
        /// Gets the size of a EDGF Entry
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
                    bw.Write(Unk0[0]);
                    bw.Write(Unk0[1]);
                    bw.Write(Unk1);
                    bw.Write(Unk2);
                }
                return ms.ToArray();
            }
        }
    }
}