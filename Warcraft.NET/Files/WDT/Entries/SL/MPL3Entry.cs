using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.WDT.Entries.SL
{
    /// <summary>
    /// An entry struct containing point light information (v3)
    /// </summary>
    public class MPL3Entry
    {
        /// <summary>
        /// Light Id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Color
        /// </summary>
        public uint Color { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Attenuation Start
        /// </summary>
        public float AttenuationStart { get; set; }

        /// <summary>
        /// Attenuation End
        /// </summary>
        public float AttenuationEnd { get; set; }

        /// <summary>
        /// Intensity
        /// </summary>
        public float Intensity { get; set; }

        /// <summary>
        /// Unknown/unused vector3, likely rotation from another struct but unused for point lights.
        /// </summary>
        public Vector3 Unused0 { get; set; }

        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileX { get; set; }

        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileY { get; set; }

        /// <summary>
        /// Index to MLTA chunk entry
        /// Defaults to -1 if not used
        /// </summary>
        public short MLTAIndex { get; set; } = -1;

        /// <summary>
        /// Index to MTEX chunk entry (note: not related to old ADT MTEX)
        /// Default to -1 if not used
        /// </summary>
        public short MTEXIndex { get; set; } = -1;

        /// <summary>
        /// Flags
        /// </summary>
        public ushort Flags { get; set; }

        /// <summary>
        /// Unknown value, wiki mentions it is "a packed value". 14336 appears to be the most common value.
        /// </summary>
        public ushort Unknown1 { get; set; } = 14336;

        public MPL3Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPL3Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MPL3Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    Id = br.ReadUInt32();
                    Color = br.ReadUInt32();
                    Position = br.ReadVector3(AxisConfiguration.Native);
                    AttenuationStart = br.ReadSingle();
                    AttenuationEnd = br.ReadSingle();
                    Intensity = br.ReadSingle();
                    Unused0 = br.ReadVector3();
                    TileX = br.ReadUInt16();
                    TileY = br.ReadUInt16();
                    MLTAIndex = br.ReadInt16();
                    MTEXIndex = br.ReadInt16();
                    Flags = br.ReadUInt16();
                    Unknown1 = br.ReadUInt16();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 56;
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Id);
                bw.Write(Color);
                bw.WriteVector3(Position, AxisConfiguration.Native);
                bw.Write(AttenuationStart);
                bw.Write(AttenuationEnd);
                bw.Write(Intensity);
                bw.WriteVector3(Unused0);
                bw.Write(TileX);
                bw.Write(TileY);
                bw.Write(MLTAIndex);
                bw.Write(MTEXIndex);
                bw.Write(Flags);
                bw.Write(Unknown1);

                return ms.ToArray();
            }
        }
    }
}
