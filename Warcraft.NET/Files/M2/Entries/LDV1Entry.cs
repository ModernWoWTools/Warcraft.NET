using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class LDV1Entry
    {
        /// <summary>
        /// unknown field
        /// </summary>
        public ushort Unk0;

        /// <summary>
        /// the maximum lod count
        /// </summary>
        public ushort LodCount;

        /// <summary>
        /// unknown field
        /// </summary>
        public float Unk1;

        /// <summary>
        /// lod serves as indes into this array
        /// </summary>
        public byte[] ParticleBoneLod;

        /// <summary>
        /// unknown field
        /// </summary>
        public uint Unk2;

        /// <summary>
        /// Initializes a new instance of the <see cref="LDV1Entry"/> class.
        /// </summary>
        public LDV1Entry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LDV1Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public LDV1Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                Unk0 = br.ReadUInt16();
                LodCount = br.ReadUInt16();
                Unk1 = br.ReadSingle();
                ParticleBoneLod = br.ReadBytes(4);
                Unk2 = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of a LDV1 Entry
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
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Unk0);
                bw.Write(LodCount);
                bw.Write(Unk1);
                bw.Write(ParticleBoneLod);
                bw.Write(Unk2);
                return ms.ToArray();
            }
        }
    }
}