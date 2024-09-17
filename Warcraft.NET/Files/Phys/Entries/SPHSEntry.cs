using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class SPHSEntry
    {
        /// <summary>
        /// Gets or Sets the local position of the Sphere shape
        /// </summary>
        public Vector3 LocalPosition;

        /// <summary>
        /// Gets or Sets the radius of the Sphere shape
        /// </summary>
        public float Radius;

        public SPHSEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                LocalPosition = br.ReadVector3();
                Radius = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a Sphere entry.
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
                    bw.WriteVector3(LocalPosition);
                    bw.Write(Radius);
                }
                return ms.ToArray();
            }
        }
    }
}