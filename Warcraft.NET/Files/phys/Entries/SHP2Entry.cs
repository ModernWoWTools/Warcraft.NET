using System.IO;
using Warcraft.NET.Files.Phys.Enums;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class SHP2Entry
    {
        /// <summary>
        /// The Shape Type <para />
        /// 0 = Box
        /// 1 = Capsule
        /// 2 = Sphere
        /// 3 = Polytope
        /// </summary>
        public ShapeType ShapeType;

        /// <summary>
        /// The index into the corresponding chunk based on ShapeType
        /// </summary>
        public ushort ShapeIndex;

        /// <summary>
        /// unknown field
        /// </summary>
        public byte[] Unk0;

        /// <summary>
        /// the friction of the shape 
        /// </summary>
        public float Friction;

        /// <summary>
        /// the restitution of the shape
        /// </summary>
        public float Restitution;

        /// <summary>
        /// the density of the shape
        /// </summary>
        public float Density;

        /// <summary>
        /// unknown field
        /// </summary>
        public uint Unk1 = 0;

        /// <summary>
        /// unknown field
        /// </summary>
        public float Unk2 = 1f;

        /// <summary>
        /// unknown field
        /// </summary>
        public ushort Unk3 = 0;

        /// <summary>
        /// unknown field
        /// </summary>
        public ushort Unk4; //no default, padding?

        /// <summary>
        /// Initializes a new instance of the <see cref="SHP2Entry"/> class.
        /// </summary>
        public SHP2Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SHP2Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public SHP2Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                ShapeType = (ShapeType)br.ReadUInt16();
                ShapeIndex = br.ReadUInt16();
                Unk0 = br.ReadBytes(4);
                Friction = br.ReadSingle();
                Restitution = br.ReadSingle();
                Density = br.ReadSingle();
                Unk1 = br.ReadUInt32();
                Unk2 = br.ReadSingle();
                Unk3 = br.ReadUInt16();
                Unk4 = br.ReadUInt16();
            }
        }

        /// <summary>
        /// Gets the size of a SHP2 entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 32;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write((ushort)ShapeType);
                    bw.Write(ShapeIndex);
                    bw.Write(Unk0);
                    bw.Write(Friction);
                    bw.Write(Restitution);
                    bw.Write(Density);
                    bw.Write(Unk1);
                    bw.Write(Unk2);
                    bw.Write(Unk3);
                    bw.Write(Unk4);
                }
                return ms.ToArray();
            }
        }
    }
}