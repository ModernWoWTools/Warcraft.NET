using System;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class SHOJEntry
    {
        /// <summary>
        /// The Transformation Matrix for Bone A of this Joint
        /// </summary>
        public Matrix3x4 FrameA;

        /// <summary>
        /// The Transformation Matrix for Bone B of this Joint
        /// </summary>
        public Matrix3x4 FrameB;

        /// <summary>
        /// The lower twist angle for the angular constraint
        /// </summary>
        public float LowerTwistAngle;

        /// <summary>
        /// The upper twist angle for the angular constraint
        /// </summary>
        public float UpperTwistAngle;

        /// <summary>
        /// The cone angle for the angular constraint
        /// </summary>
        public float ConeAngle;

        /// <summary>
        /// The Maximum torque the motor can apply
        /// </summary>
        public float MaxMotorTorque;

        /// <summary>
        /// The MotorMode <para />
        /// 0 = disabled?
        /// 1 = motorPositionMode (MotorFrequencyHz>0)
        /// 2 = motorVelocityMode
        /// </summary>
        public uint MotorMode; // NO BACKWARDS COMPATIBILITY as of Legion (7.0.1.20979) and Legion (7.3.0.24931)! client always assumes new size!

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
                FrameA = br.ReadMatrix3x4();
                FrameB = br.ReadMatrix3x4();
                LowerTwistAngle = br.ReadSingle();
                UpperTwistAngle = br.ReadSingle();
                ConeAngle = br.ReadSingle();
                MaxMotorTorque = br.ReadSingle();
                MotorMode = br.ReadUInt32();
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
                    bw.WriteMatrix3x4(FrameA);
                    bw.WriteMatrix3x4(FrameB);
                    bw.Write(LowerTwistAngle);
                    bw.Write(UpperTwistAngle);
                    bw.Write(ConeAngle);
                    bw.Write(MaxMotorTorque);
                    bw.Write(MotorMode);
                }
                return ms.ToArray();
            }
        }
    }
}
