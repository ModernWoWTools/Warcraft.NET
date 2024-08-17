using System.IO;
using Warcraft.NET.Files.Phys.Enums;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class JOINEntry
    {
        /// <summary>
        /// sets or gets the index of the first connected Rigidbody
        /// </summary>
        public uint BodyAIdx;

        /// <summary>
        /// sets or gets the index of the second connected Rigidbody
        /// </summary>
        public uint BodyBIdx;

        /// <summary>
        /// sets or gets a Unknown field.
        /// </summary>
        public byte[] Unk;

        /// <summary>
        /// sets or gets the JointType<para />
        /// 0 = SphericalJoint<para />
        /// 1 = ShoulderJoint<para />
        /// 2 = WeldJoint<para />
        /// 3 = RevoluteJoint<para />
        /// 4 = PrismaticJoint<para />
        /// 5 = DistanceJoint
        /// </summary>
        public JointType JointType;

        /// <summary>
        /// sets or gets the index of the joint into the joint list based on the JointType field
        /// </summary>
        public ushort JointID;

        /// <summary>
        /// Initializes a new instance of the <see cref="JOINEntry"/> class.
        /// </summary>
        public JOINEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JOINEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public JOINEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                BodyAIdx = br.ReadUInt32();
                BodyBIdx = br.ReadUInt32();
                Unk = br.ReadBytes(4);
                JointType = (JointType)br.ReadUInt16();
                JointID = br.ReadUInt16();
            }
        }

        /// <summary>
        /// Gets the size of a JOIN entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 16;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(BodyAIdx);
                    bw.Write(BodyBIdx);
                    bw.Write(Unk);
                    bw.Write((ushort)JointType);
                    bw.Write(JointID);
                }
                return ms.ToArray();
            }
        }
    }
}
