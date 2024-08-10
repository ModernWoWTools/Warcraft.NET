using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class BDY4Entry
    {

        public enum Body_Type : ushort
        {
            root = 0,
            dynamic = 1,
            unk = 2,
        }

        public Body_Type type { get; set; }        // maps to dmBodyDef type enum. 0 -> 1, 1 -> 0 = dm_dynamicBody, * -> 2. Only one should be of type 0 (root). possibly only 0 and 1.
        public ushort boneIndex { get; set; }
        public C3Vector position { get; set; }
        public ushort shapeIndex { get; set; }
        public byte[] PADDING { get; set; }
        public int shapesCount { get; set; }    // shapes_count shapes are in this body.
        public float unk0 { get; set; }
        //#if version >= 3 // BDY3
        public float _x1c { get; set; }         // default 1.0
        public float drag { get; set; }         // default 0, maybe incorrect
        public float unk1;                      // default 0, seems to be some sort of weight. 
                                                // If version >= 3 and unk1 == 0 the body will be non kinematic even if the flag is set, it needs to get its transform from the parent bone.
                                                // See offhand_1h_artifactskulloferedar_d_06 where all the bodies have the kinematic flag
        public float _x28 { get; set; }         // default 0.89999998

        public byte[] x2c { get; set; }// default 0x00000000


        /// <summary>
        /// Initializes a new instance of the <see cref="BDY4Entry"/> class.
        /// </summary>
        public BDY4Entry(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="BDY4Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public BDY4Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                type = (Body_Type)br.ReadUInt16();
                boneIndex = br.ReadUInt16();
                position = new C3Vector(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                shapeIndex = br.ReadUInt16();
                PADDING = br.ReadBytes(2);
                shapesCount = br.ReadInt32();
                unk0 = br.ReadSingle();
                _x1c = br.ReadSingle();
                drag = br.ReadSingle();
                unk1 = br.ReadSingle();
                _x28 = br.ReadSingle();
                x2c = br.ReadBytes(4);
            }
        }

        /// <summary>
        /// Gets the size of a bdy4 entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 48;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write((ushort)type);
                    bw.Write(boneIndex);
                    bw.Write(position.X);
                    bw.Write(position.Y);
                    bw.Write(position.Z);
                    bw.Write(shapeIndex);
                    bw.Write(PADDING);
                    bw.Write(shapesCount);
                    bw.Write(unk0);
                    bw.Write(_x1c);
                    bw.Write(drag);
                    bw.Write(unk1);
                    bw.Write(_x28);
                    bw.Write(x2c);
                }

                return ms.ToArray();
            }
        }
    }
}
