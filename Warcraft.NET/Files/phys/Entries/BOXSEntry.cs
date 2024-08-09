using System.IO;
using Warcraft.NET.Files.phys.Chunks;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class BOXSEntry
    {
        /// <summary>
        /// Gets or Sets the matrix of the box shape
        /// </summary>
        public Mat3x4 a;

        /// <summary>
        /// Gets or Sets the local position of the box shape
        /// </summary>
        public C3Vector c;

        public BOXSEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                a = new Mat3x4(br.ReadBytes(48));
                c = new C3Vector(br.ReadBytes(12));
            }
        }
        /// <summary>
        /// Gets the size of a box entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 60;
        }
        
        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(a.asBytes());
                    bw.Write(c.asBytes());
                }

                return ms.ToArray();
            }
        }

    }
}