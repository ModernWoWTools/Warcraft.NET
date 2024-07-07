using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.WDT.Entrys.WoD
{
    /// <summary>
    /// An entry struct containing light information
    /// </summary>
    public class MPLTEntry
    {
        /// <summary>
        /// Light Id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileX { get; set; }

        /// <summary>
        /// Map Tile X
        /// </summary>
        public ushort TileY { get; set; }

        /// <summary>
        /// Color
        /// </summary>
        public RGBA Color { get; set; }

        /// <summary>
        /// Position
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Light radius (light radius must be smaller than blend radius)
        /// </summary>
        public float LightRadius { get; set; }

        /// <summary>
        /// Blend radius
        /// </summary>
        public float BlendRadius { get; set; }

        /// <summary>
        /// Light Intensity (0.5 - 2 is recommended)
        /// </summary>
        public float Intensity { get; set; }

        public MPLTEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPLTEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MPLTEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    Id = br.ReadUInt32();
                    TileX = br.ReadUInt16();
                    TileY = br.ReadUInt16();
                    Color = br.ReadRGBA();
                    Position = br.ReadVector3(AxisConfiguration.Native);
                    LightRadius = br.ReadSingle();
                    BlendRadius = br.ReadSingle();
                    Intensity = br.ReadSingle();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 36;
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
                bw.Write(TileX);
                bw.Write(TileY);
                bw.WriteRGBA(Color);
                bw.WriteVector3(Position, AxisConfiguration.Native);
                bw.Write(LightRadius);
                bw.Write(BlendRadius);
                bw.Write(Intensity);

                return ms.ToArray();
            }
        }
    }
}
