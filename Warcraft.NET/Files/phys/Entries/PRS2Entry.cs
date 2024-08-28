using System.IO;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class PRS2Entry
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
        /// The lower limit
        /// </summary>
        public float LowerLimit;

        /// <summary>
        /// The upper limit
        /// </summary>
        public float UpperLimit;

        /// <summary>
        /// Unknown value
        /// </summary>
        public float Unk0;

        /// <summary>
        /// Max motor force if enabled
        /// </summary>
        public float MaxMotorForce;

        /// <summary>
        /// unknown value
        /// </summary>
        public float Unk1;

        /// <summary>
        /// The MotorMode <para />
        /// 0 = disabled?
        /// 1 = motorPositionMode (MotorFrequencyHz>0)
        /// 2 = motorVelocityMode
        /// </summary>
        public uint MotorMode;

        /// <summary>
        /// how often per second the motor damps
        /// </summary>
        public float MotorFrequencyHz;

        /// <summary>
        /// the ratio how much the motor damps
        /// </summary>
        public float MotorDampingRatio;

        /// <summary>
        /// Initializes a new instance of the <see cref="PRS2Entry"/> class.
        /// </summary>
        public PRS2Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PRS2Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public PRS2Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                FrameA = br.ReadMatrix3x4();
                FrameB = br.ReadMatrix3x4();
                LowerLimit = br.ReadSingle();
                UpperLimit = br.ReadSingle();
                Unk0 = br.ReadSingle();
                MaxMotorForce = br.ReadSingle();
                Unk1 = br.ReadSingle();
                MotorMode = br.ReadUInt32();
                MotorFrequencyHz = br.ReadSingle();
                MotorDampingRatio = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a PRS2 entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 128;
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
                    bw.Write(LowerLimit);
                    bw.Write(UpperLimit);
                    bw.Write(Unk0);
                    bw.Write(MaxMotorForce);
                    bw.Write(Unk1);
                    bw.Write(MotorMode);
                    bw.Write(MotorFrequencyHz);
                    bw.Write(MotorDampingRatio);
                }
                return ms.ToArray();
            }
        }
    }
}