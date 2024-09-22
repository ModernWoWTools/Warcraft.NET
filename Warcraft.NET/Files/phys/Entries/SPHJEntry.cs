using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class SPHJEntry
    {
        /// <summary>
        /// the local anchor of the Bone A of the joint
        /// </summary>
        public Vector3 AnchorA;

        /// <summary>
        /// the local anchor of the Bone B of the joint
        /// </summary>
        public Vector3 AnchorB;

        /// <summary>
        /// the friction torque
        /// </summary>
        public float FrictionTorque;

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
                AnchorA = br.ReadVector3(AxisConfiguration.ZUp);
                AnchorB = br.ReadVector3(AxisConfiguration.ZUp);
                FrictionTorque = br.ReadSingle();
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
                    bw.WriteVector3(AnchorA);
                    bw.WriteVector3(AnchorB);
                    bw.Write(FrictionTorque);
                }
                return ms.ToArray();
            }
        }
    }
}