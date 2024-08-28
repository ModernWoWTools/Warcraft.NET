using System.IO;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class WLJ3Entry
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
        /// how often the angular dampening is applied per second
        /// </summary>
        public float AngularFrequencyHz;

        /// <summary>
        /// the ratio how strong the angular dampening is applied
        /// </summary>
        public float AngularDampingRatio;

        /// <summary>
        /// how often the linear dampening is applied per second
        /// </summary>
        public float LinearFrequencyHz; // default 0

        /// <summary>
        /// the ratio how linear the angular dampening is applied
        /// </summary>
        public float LinearDampingRatio; // default 0

        /// <summary>
        /// unknown field
        /// </summary>
        public float Unk0;

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
                FrameA = br.ReadMatrix3x4();
                FrameB = br.ReadMatrix3x4();
                AngularFrequencyHz = br.ReadSingle();
                AngularDampingRatio = br.ReadSingle();
                LinearFrequencyHz = br.ReadSingle();
                LinearDampingRatio = br.ReadSingle();
                Unk0 = br.ReadSingle();
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
            using (var bw = new BinaryWriter(ms))
            {
                bw.WriteMatrix3x4(FrameA);
                bw.WriteMatrix3x4(FrameB);
                bw.Write(AngularFrequencyHz);
                bw.Write(AngularDampingRatio);
                bw.Write(LinearFrequencyHz);
                bw.Write(LinearDampingRatio);
                bw.Write(Unk0);
                return ms.ToArray();
            }
        }
    }
}
