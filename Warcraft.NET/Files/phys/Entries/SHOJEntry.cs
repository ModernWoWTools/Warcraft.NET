using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class SHOJEntry
    {


        public Mat3x4 frameA;
        public Mat3x4 frameB;
        public float lowerTwistAngle;
        public float upperTwistAngle;
        public float coneAngle;
        public float maxMotorTorque;
        public UInt32 motorMode; // NO BACKWARDS COMPATIBILITY as of Legion (7.0.1.20979) and Legion (7.3.0.24931)! client always assumes new size!


        /// <summary>
        /// Initializes a new instance of the <see cref="SHOJEntry"/> class.
        /// </summary>
        public SHOJEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SHOJEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public SHOJEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                frameA = new Mat3x4(br.ReadBytes(48));
                frameB = new Mat3x4(br.ReadBytes(48));
                lowerTwistAngle = br.ReadSingle();
                upperTwistAngle = br.ReadSingle();
                coneAngle = br.ReadSingle();
                maxMotorTorque = br.ReadSingle();
                motorMode = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of a SHOJ entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 116;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(frameA.asBytes());
                    bw.Write(frameB.asBytes());
                    bw.Write(lowerTwistAngle);
                    bw.Write(upperTwistAngle);
                    bw.Write(coneAngle);
                    bw.Write(maxMotorTorque);
                    bw.Write(motorMode);
                }

                return ms.ToArray();
            }
        }
    }
}
