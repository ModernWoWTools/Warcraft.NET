using System.IO;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class REV2Entry
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
        /// The lower swing angle
        /// </summary>
        public float LowerAngle;
        /// <summary>
        /// The upper swing angle
        /// </summary>
        public float UpperAngle;

        /// <summary>
        /// The max motor torque 
        /// </summary>
        public float MaxMotorTorque;
        /// <summary>
        /// The MotorMode <para />
        /// 0 = disabled?
        /// 1 = motorPositionMode (MotorFrequencyHz>0)
        /// 2 = motorVelocityMode
        /// </summary>
        public uint MotorMode; // 1: motorPositionMode → frequency > 0, 2: motorVelocityMode
        /// <summary>
        /// how often per second the motor damps
        /// </summary>
        public float MotorFrequencyHz;
        /// <summary>
        /// the ratio how much the motor damps
        /// </summary>
        public float MotorDampingRatio;

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
                FrameA = br.ReadMatrix3x4();
                FrameB = br.ReadMatrix3x4();
                LowerAngle = br.ReadSingle();
                UpperAngle = br.ReadSingle();
                MaxMotorTorque = br.ReadSingle();
                MotorMode = br.ReadUInt32();
                MotorFrequencyHz = br.ReadSingle();
                MotorDampingRatio = br.ReadSingle();
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
                    bw.WriteMatrix3x4(FrameA);
                    bw.WriteMatrix3x4(FrameB);
                    bw.Write(LowerAngle);
                    bw.Write(UpperAngle);
                    bw.Write(MaxMotorTorque);
                    bw.Write(MotorMode);
                    bw.Write(MotorFrequencyHz);
                    bw.Write(MotorDampingRatio);
                }
                return ms.ToArray();
            }
        }
    }
}