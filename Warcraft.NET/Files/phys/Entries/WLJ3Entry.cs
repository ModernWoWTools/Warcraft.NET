using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class WLJ3Entry
    {
        public Mat3x4 frameA;
        public Mat3x4 frameB;
        public float angularFrequencyHz;
        public float angularDampingRatio;

        public float linearFrequencyHz; // default 0
        public float linearDampingRatio; // default 0

        public float unk70;


        /// <summary>
        /// Initializes a new instance of the <see cref="WLJ3Entry"/> class.
        /// </summary>
        public WLJ3Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WLJ3Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public WLJ3Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                frameA = new Mat3x4(br.ReadBytes(48));
                frameB = new Mat3x4(br.ReadBytes(48));
                angularFrequencyHz = br.ReadSingle();
                angularDampingRatio = br.ReadSingle();
                linearFrequencyHz = br.ReadSingle();
                linearDampingRatio = br.ReadSingle();
                unk70 = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of a WLJ3 entry.
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
                    bw.Write(angularFrequencyHz);
                    bw.Write(angularDampingRatio);
                    bw.Write(linearFrequencyHz);
                    bw.Write(linearDampingRatio);
                    bw.Write(unk70);
                }

                return ms.ToArray();
            }
        }
    }
}
