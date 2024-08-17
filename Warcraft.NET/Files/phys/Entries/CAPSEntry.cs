using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class CAPSEntry
    {
        /// <summary>
        /// Gets or Sets the local start position of this capsule shape
        /// </summary>
        public Vector3 LocalPosition1;

        /// <summary>
        /// Gets or Sets the local end position of this capsule shape
        /// </summary>
        public Vector3 LocalPosition2;

        /// <summary>
        /// Gets or Sets the Radius of this capsule shape
        /// </summary>
        public float Radius;

        public CAPSEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                LocalPosition1 = br.ReadVector3(AxisConfiguration.ZUp);
                LocalPosition2 = br.ReadVector3(AxisConfiguration.ZUp);
                Radius = br.ReadSingle();
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
                bw.WriteVector3(LocalPosition1);
                bw.WriteVector3(LocalPosition2);
                bw.Write(Radius);
                return ms.ToArray();
            }
        }
    }
}