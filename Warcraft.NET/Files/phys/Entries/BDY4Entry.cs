using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Phys.Enums;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class BDY4Entry
    {
        /// <summary>
        /// Sets or gets the BodyType of the rigidbody
        /// 0=static
        /// 1=dynamic
        /// 2=unknown
        /// </summary>
        public BodyType BodyType { get; set; }        // maps to dmBodyDef BodyType enum. 0 -> 1, 1 -> 0 = dm_dynamicBody, * -> 2. Only one should be of BodyType 0 (root). possibly only 0 and 1.

        /// <summary>
        /// Sets or gets the index of the bone, which is connected to this rigidbody
        /// </summary>
        public ushort BoneIndex { get; set; }

        /// <summary>
        /// sets or gets the default Position of the rigidbody
        /// </summary>
        public Vector3 Position { get; set; } = new Vector3(0, 0, 0);

        /// <summary>
        /// sets or gets the index of the shape, which is connected to this rigidbody
        /// </summary>
        public ushort ShapesIndex { get; set; }

        /// <summary>
        /// sets or gets a currently unknown field. Possibly 'Padding'
        /// </summary>
        public byte[] Unk0 { get; set; } = { 0, 0 };

        /// <summary>
        /// sets or gets the amount of shapes this rigidbody has
        /// </summary>
        public int ShapesCount { get; set; }    // shapes_count shapes are in this body.

        /// <summary>
        /// sets or gets a currently unknown field. Possibly 'UpliftFactor'
        /// </summary>
        public float Unk1 { get; set; } = 0f;

        //#if version >= 3 // BDY3
        /// <summary>
        /// sets or gets a currently unknown field. Possibly 'GravityScale'
        /// </summary>
        public float Unk2 { get; set; } = 1.0f;

        /// <summary>
        /// sets or gets the Drag value of the rigidbody.
        /// </summary>
        public float Drag { get; set; } = 0f;

        /// <summary>
        /// sets or gets a currently unknown field. Possibly 'Mass/Weight'
        /// </summary>
        public float Unk3 = 0f;

        /// <summary>
        /// sets or gets a currently unknown field.
        /// </summary>
        public float Unk4 { get; set; } = 0.89999998f;

        /// <summary>
        /// sets or gets a currently unknown field.
        /// </summary>
        public byte[] Unk5 { get; set; } = { 0, 0, 0, 0 };

        /// <summary>
        /// Initializes a new instance of the <see cref="BDY4Entry"/> class.
        /// </summary>
        public BDY4Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BDY4Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public BDY4Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                BodyType = (BodyType)br.ReadUInt16();
                BoneIndex = br.ReadUInt16();
                Position = br.ReadVector3(AxisConfiguration.ZUp);
                ShapesIndex = br.ReadUInt16();
                Unk0 = br.ReadBytes(2);
                ShapesCount = br.ReadInt32();
                Unk1 = br.ReadSingle();
                Unk2 = br.ReadSingle();
                Drag = br.ReadSingle();
                Unk3 = br.ReadSingle();
                Unk4 = br.ReadSingle();
                Unk5 = br.ReadBytes(4);
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
                    bw.Write((ushort)BodyType);
                    bw.Write(BoneIndex);
                    bw.Write(Position.X);
                    bw.Write(Position.Y);
                    bw.Write(Position.Z);
                    bw.Write(ShapesIndex);
                    bw.Write(Unk0);
                    bw.Write(ShapesCount);
                    bw.Write(Unk1);
                    bw.Write(Unk2);
                    bw.Write(Drag);
                    bw.Write(Unk3);
                    bw.Write(Unk4);
                    bw.Write(Unk5);
                }
                return ms.ToArray();
            }
        }
    }
}