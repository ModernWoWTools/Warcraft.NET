using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class REV2Entry
    {
        public Mat3x4 frameA;
        public Mat3x4 frameB;
        public float lowerAngle;
        public float upperAngle;

        public float maxMotorTorque; 
        public UInt32 motorMode; // 1: motorPositionMode → frequency > 0, 2: motorVelocityMode

        public float motorFrequencyHz;
        public float motorDampingRatio;


        /// <summary>
        /// Initializes a new instance of the <see cref="REV2Entry"/> class.
        /// </summary>
        public REV2Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="REV2Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public REV2Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                frameA = new Mat3x4(br.ReadBytes(48));
                frameB = new Mat3x4(br.ReadBytes(48));
                lowerAngle = br.ReadSingle();
                upperAngle = br.ReadSingle();
                maxMotorTorque = br.ReadSingle();
                motorMode = br.ReadUInt32();
                motorFrequencyHz = br.ReadSingle();
                motorDampingRatio = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a REV2 entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 120;
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
                    bw.Write(lowerAngle);
                    bw.Write(upperAngle);
                    bw.Write(maxMotorTorque);
                    bw.Write(motorMode);
                    bw.Write(motorFrequencyHz);
                    bw.Write(motorDampingRatio);
                }

                return ms.ToArray();
            }
        }
    }
}
