using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class DSTJEntry
    {


        public C3Vector localAnchorA;
        public C3Vector localAnchorB;

        public float some_distance_factor;


        /// <summary>
        /// Initializes a new instance of the <see cref="DSTJEntry"/> class.
        /// </summary>
        public DSTJEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DSTJEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public DSTJEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                localAnchorA = new C3Vector(br.ReadBytes(12));
                localAnchorB = new C3Vector(br.ReadBytes(12));
                some_distance_factor = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a DSTJ entry.
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
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(localAnchorA.asBytes());
                    bw.Write(localAnchorB.asBytes());
                    bw.Write(some_distance_factor);
                }

                return ms.ToArray();
            }
        }
    }
}
