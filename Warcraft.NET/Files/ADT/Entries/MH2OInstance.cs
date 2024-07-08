using System.IO;

namespace Warcraft.NET.Files.ADT.Entrys
{
    /// <summary>
    /// Contains informations about the liquids on the current subchunk.
    /// </summary>
    public class MH2OInstance
    {
        /// <summary>
        /// Gets or sets the liquid type.
        /// </summary>
        public ushort LiquidTypeId { get; set; }

        /// <summary>
        /// Gets or sets the liquid vertex format.
        /// </summary>
        public ushort LiquidObjectOrVertexFormat { get; set; }

        /// <summary>
        /// Gets or sets the minimum height level.
        /// </summary>
        public float MinHeightLevel { get; set; }

        /// <summary>
        /// Gets or sets the maximum height level.
        /// </summary>
        public float MaxHeightLevel { get; set; }

        /// <summary>
        /// Gets or sets the X axis offset.
        /// </summary>
        public byte OffsetX { get; set; }

        /// <summary>
        /// Gets or sets the Y axis offset.
        /// </summary>
        public byte OffsetY { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public byte Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public byte Height { get; set; }

        /// <summary>
        /// Gets or sets the offset of ExistsBitmap.
        /// </summary>
        public uint OffsetExistsBitmap { get; set; }

        /// <summary>
        /// Gets or sets the offset of the vertex data.
        /// </summary>
        public uint OffsetVertexData { get; set; }

        /// <summary>
        /// Gets or sets the render bitmap bytes.
        /// </summary>
        public byte[] RenderBitmapBytes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MH2OInstanceVertexData"/> of the current <see cref="MH2OInstance"/>.
        /// </summary>
        public MH2OInstanceVertexData VertexData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2OInstance"/> class.
        /// </summary>
        /// <param name="inData"></param>
        public MH2OInstance(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                LiquidTypeId = br.ReadUInt16();
                LiquidObjectOrVertexFormat = br.ReadUInt16();
                MinHeightLevel = br.ReadSingle();
                MaxHeightLevel = br.ReadSingle();
                OffsetX = br.ReadByte();
                OffsetY = br.ReadByte();
                Width = br.ReadByte();
                Height = br.ReadByte();
                OffsetExistsBitmap = br.ReadUInt32();
                OffsetVertexData = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return (sizeof(ushort) * 2) + (sizeof(float) * 2) + (sizeof(byte) * 4) + (sizeof(uint) * 2);
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(LiquidTypeId);
                if (OffsetVertexData == 0 && LiquidTypeId != 2)
                    LiquidObjectOrVertexFormat = 2;
                bw.Write(LiquidObjectOrVertexFormat);
                bw.Write(MinHeightLevel);
                bw.Write(MaxHeightLevel);
                bw.Write(OffsetX);
                bw.Write(OffsetY);
                bw.Write(Width);
                bw.Write(Height);
                bw.Write(OffsetExistsBitmap);
                bw.Write(OffsetVertexData);
                return ms.ToArray();
            }
        }
    }
}
