using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class SHP2Entry
    {


        /*0x00*/
        public ushort shapeType;
        /*0x02*/
        public ushort shapeIndex; // into the corresponding chunk
        /*0x04*/
        public byte[] unk;
        /*0x08*/
        public float friction;
        /*0x0c*/
        public float restitution;
        /*0x10*/
        public float density;
        /*0x14*/
        public UInt32 _x14; // default 0
        /*0x18*/
        public float _x18; // default 1.0
        /*0x1c*/
        public UInt16 _x1c; // default 0
        /*0x1e*/
        public UInt16 _x1e; // no default, padding?



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
                shapeType = br.ReadUInt16();
                shapeIndex = br.ReadUInt16();
                unk = br.ReadBytes(4);
                friction = br.ReadSingle();
                restitution = br.ReadSingle();
                density = br.ReadSingle();
                _x14 = br.ReadUInt32();
                _x18 = br.ReadSingle();
                _x1c = br.ReadUInt16();
                _x1e = br.ReadUInt16();
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
                    bw.Write(shapeType);
                    bw.Write(shapeIndex);
                    bw.Write(unk);
                    bw.Write(friction);
                    bw.Write(restitution);
                    bw.Write(density);
                    bw.Write(_x14);
                    bw.Write(_x18);
                    bw.Write(_x1c);
                    bw.Write(_x1e);
                }

                return ms.ToArray();
            }
        }
    }
}
