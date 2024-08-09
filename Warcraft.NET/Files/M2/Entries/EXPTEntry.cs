using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class EXPTEntry
    {
        public float zSource;
        public float colorMult;
        public float alphaMult;


        /// <summary>
        /// Initializes a new instance of the <see cref="EXPTEntry"/> class.
        /// </summary>
        public EXPTEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EXPTEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public EXPTEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                zSource = br.ReadSingle();
                colorMult = br.ReadSingle();
                alphaMult = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a animation file id entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 12;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(zSource);
                    bw.Write(colorMult);
                    bw.Write(alphaMult);
                }

                return ms.ToArray();
            }
        }
    }
}
