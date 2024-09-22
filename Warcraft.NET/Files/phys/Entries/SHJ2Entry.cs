using System.IO;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class SHJ2Entry
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
        /// The lower twist angle of the angular constraint
        /// </summary>
        public float LowerTwistAngle;

        /// <summary>
        /// The upper twist angle of the angular constraint
        /// </summary>
        public float UpperTwistAngle;

        /// <summary>
        /// The cone angle of the angular constraint
        /// </summary>
        public float ConeAngle;

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
        /// Initializes a new instance of the <see cref="SHJ2Entry"/> class.
        /// </summary>
        public SHJ2Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SHJ2Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public SHJ2Entry(byte[] data)
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
                MotorFrequencyHz = br.ReadSingle();
                MotorDampingRatio = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a SHJ2 entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 124;
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
                    bw.Write(MotorFrequencyHz);
                    bw.Write(MotorDampingRatio);
                }
                return ms.ToArray();
            }
        }
    }
}