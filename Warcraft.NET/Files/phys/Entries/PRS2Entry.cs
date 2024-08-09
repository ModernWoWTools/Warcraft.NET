using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class PRS2Entry
    {


        public Mat3x4 frameA;
        public Mat3x4 frameB;
        public float lowerLimit;
        public float upperLimit;
        public float _68;
        public float maxMotorForce;
        public float _70;
        public UInt32 motorMode;

        public float motorFrequencyHz;
        public float motorDampingRatio;


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
                frameA = new Mat3x4(br.ReadBytes(48));
                frameB = new Mat3x4(br.ReadBytes(48));
                lowerLimit = br.ReadSingle();
                upperLimit = br.ReadSingle();
                _68 = br.ReadSingle();
                maxMotorForce = br.ReadSingle();
                _70 = br.ReadSingle();
                motorMode = br.ReadUInt32();
                motorFrequencyHz = br.ReadSingle();
                motorDampingRatio = br.ReadSingle();
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
                    bw.Write(frameA.asBytes());
                    bw.Write(frameB.asBytes());
                    bw.Write(lowerLimit);
                    bw.Write(upperLimit);
                    bw.Write(_68);
                    bw.Write(maxMotorForce);
                    bw.Write(_70);
                    bw.Write(motorMode);
                    bw.Write(motorFrequencyHz);
                    bw.Write(motorDampingRatio);
                }

                return ms.ToArray();
            }
        }
    }
}
