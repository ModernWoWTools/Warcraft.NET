using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class SPHJEntry
    {
        public C3Vector anchorA;
        public C3Vector anchorB;
        public float frictionTorque;


        /// <summary>
        /// Initializes a new instance of the <see cref="SPHJEntry"/> class.
        /// </summary>
        public SPHJEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPHJEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public SPHJEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                anchorA = new C3Vector(br.ReadBytes(12));
                anchorB = new C3Vector(br.ReadBytes(12));
                frictionTorque = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a SPHJ entry.
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
                    bw.Write(anchorA.asBytes());
                    bw.Write(anchorB.asBytes());
                    bw.Write(frictionTorque);
                }

                return ms.ToArray();
            }
        }
    }
}
