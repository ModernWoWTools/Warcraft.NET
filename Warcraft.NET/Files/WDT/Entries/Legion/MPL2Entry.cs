using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;
using Warcraft.NET.Files.WDT.Entries.WoD;

namespace Warcraft.NET.Files.WDT.Entries.Legion
{
    /// <summary>
    /// An entry struct containing point light information
    /// </summary>
    public class MPL2Entry : MPLTEntry
    {
        /// <summary>
        /// Contains unknow light data
        /// </summary>
        public float[] UnknowLightData { get; set; } = new float[3] { 0, 0, 0 };

        /// <summary>
        /// Index to MLTA chunk entry
        /// </summary>
        public short MLTAIndex { get; set; } = -1;

        /// <summary>
        /// Index to MLTA chunk entry
        /// </summary>
        public short UnknowIndex { get; set; } = -1;

        public MPL2Entry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPL2Entry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MPL2Entry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    Id = br.ReadUInt32();
                    Color = br.ReadBGRA();
                    Position = br.ReadVector3(AxisConfiguration.Native);
                    LightRadius = br.ReadSingle();
                    BlendRadius = br.ReadSingle();
                    Intensity = br.ReadSingle();
                    
                    // UnknowLightData
                    UnknowLightData[0] = br.ReadSingle();
                    UnknowLightData[1] = br.ReadSingle();
                    UnknowLightData[2] = br.ReadSingle();

                    TileX = br.ReadUInt16();
                    TileY = br.ReadUInt16();
                    MLTAIndex = br.ReadInt16();
                    UnknowIndex = br.ReadInt16();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public new static int GetSize()
        {
            return 52;
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public new byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Id);
                bw.WriteBGRA(Color);
                bw.WriteVector3(Position, AxisConfiguration.Native);
                bw.Write(LightRadius);
                bw.Write(BlendRadius);
                bw.Write(Intensity);

                // UnknowLightData
                bw.Write(UnknowLightData[0]);
                bw.Write(UnknowLightData[1]);
                bw.Write(UnknowLightData[2]);

                bw.Write(TileX);
                bw.Write(TileY);
                bw.Write(MLTAIndex);
                bw.Write(UnknowIndex);

                return ms.ToArray();
            }
        }
    }
}
