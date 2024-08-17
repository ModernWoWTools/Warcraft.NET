using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class DSTJEntry
    {
        /// <summary>
        /// sets or gets the local Anchor for Bone A from the Joint
        /// </summary>
        public Vector3 LocalAnchorA;

        /// <summary>
        /// sets or gets the local Anchor for Bone B from the Joint
        /// </summary>
        public Vector3 LocalAnchorB;

        /// <summary>
        /// sets or gets the currently unknown value for the distance joint calculation
        /// </summary>
        public float UnknownDistanceFactor;

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
                LocalAnchorA = br.ReadVector3();
                LocalAnchorB = br.ReadVector3();
                UnknownDistanceFactor = br.ReadSingle();
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
                    bw.WriteVector3(LocalAnchorA);
                    bw.WriteVector3(LocalAnchorB);
                    bw.Write(UnknownDistanceFactor);
                }
                return ms.ToArray();
            }
        }
    }
}