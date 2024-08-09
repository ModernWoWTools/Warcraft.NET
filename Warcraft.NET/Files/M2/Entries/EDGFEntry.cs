using System;
using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class EDGFEntry
    {
        public float[] _0x0;
        public float _0x8;
        public byte[] _0xC;

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
                _0x0 = new float[2];
                _0x0[0] = br.ReadSingle();
                _0x0[1] = br.ReadSingle();
                _0x8 = br.ReadSingle();
                _0xC = br.ReadBytes(4);
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
                    bw.Write(_0x0[0]);
                    bw.Write(_0x0[1]);
                    bw.Write(_0x8);
                    bw.Write(_0xC);
                }

                return ms.ToArray();
            }
        }
    }
}
