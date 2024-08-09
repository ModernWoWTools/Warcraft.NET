using System.IO;
using Warcraft.NET.Files.phys.Chunks;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class CAPSEntry
    {

        /// <summary>
        /// Gets or Sets the local start position of the capsule shape
        /// </summary>
        public C3Vector localPosition1;

        /// <summary>
        /// Gets or Sets the local end position of the capsule shape
        /// </summary>
        public C3Vector localPosition2;

        /// <summary>
        /// Gets or Sets the radius of the capsule shape
        /// </summary>
        public float radius;

        public CAPSEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                localPosition1 = new C3Vector(br.ReadBytes(12));
                localPosition2 = new C3Vector(br.ReadBytes(12));
                radius = br.ReadSingle();
            }
        }
        /// <summary>
        /// Gets the size of a caps entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 28;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {                
                bw.Write(localPosition1.asBytes());
                bw.Write(localPosition2.asBytes());
                bw.Write(radius);
                return ms.ToArray();
            }
        }
    }
}