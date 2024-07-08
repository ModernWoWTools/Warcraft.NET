using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Entrys
{
    /// <summary>
    /// Contains informations about the vertex data in the current MH2O instance subchunk.
    /// </summary>
    public class MH2OInstanceVertexData
    {
        /// <summary>
        /// Gets or sets the height map.
        /// </summary>
        public float[,] HeightMap { get; set; } = new float[0, 0];

        /// <summary>
        /// Gets or sets the depth map.
        /// </summary>
        public byte[,] DepthMap { get; set; } = new byte[0, 0];

        /// <summary>
        /// Gets or sets the UV map.
        /// </summary>
        public UVMapEntry[,] UVMap { get; set; } = new UVMapEntry[0, 0];

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2OInstanceVertexData"/> class.
        /// </summary>
        /// <param name="inData"></param>
        /// <param name="instance"></param>
        public MH2OInstanceVertexData(byte[] inData, MH2OInstance instance)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                switch (instance.LiquidObjectOrVertexFormat)
                {
                    case 0:
                        ReadHeightMap(br, instance);
                        ReadDepthMap(br, instance);
                        break;
                    case 1:
                        ReadHeightMap(br, instance);
                        ReadUVMapEntries(br, instance);
                        break;
                    case 2:
                        ReadDepthMap(br, instance);
                        break;
                    case 3:
                        ReadHeightMap(br, instance);
                        ReadUVMapEntries(br, instance);
                        ReadDepthMap(br, instance);
                        break;
                    default: // The value is probably >= 42 so it's a LiquidObject taken out of a DBC file
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize(MH2OInstance instance)
        {
            return (sizeof(float) * (instance.Height + 1) * (instance.Width + 1)) + (sizeof(byte) * (instance.Height + 1) * (instance.Width + 1));
        }

        /// <inheritdoc/>
        public byte[] Serialize(MH2OInstance instance)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                switch (instance.LiquidObjectOrVertexFormat)
                {
                    case 0:
                        WriteHeightMap(bw, instance);
                        WriteDepthMap(bw, instance);
                        break;
                    case 1:
                        WriteHeightMap(bw, instance);
                        WriteUVMapEntries(bw, instance);
                        break;
                    case 2:
                        WriteDepthMap(bw, instance);
                        break;
                    case 3:
                        WriteHeightMap(bw, instance);
                        WriteUVMapEntries(bw, instance);
                        WriteDepthMap(bw, instance);
                        break;
                    default: // The value is probably >= 42 so it's a LiquidObject taken out of a DBC file
                        break;
                }
                return ms.ToArray();
            }
        }

        private void ReadHeightMap(BinaryReader br, MH2OInstance instance)
        {
            HeightMap = new float[instance.Height + 1, instance.Width + 1];
            for (byte y = 0; y < instance.Height + 1; y++)
                for (byte x = 0; x < instance.Width + 1; x++)
                    HeightMap[y, x] = br.ReadSingle();
        }

        private void ReadDepthMap(BinaryReader br, MH2OInstance instance)
        {
            DepthMap = new byte[instance.Height + 1, instance.Width + 1];
            for (byte y = 0; y < instance.Height + 1; y++)
                for (byte x = 0; x < instance.Width + 1; x++)
                    DepthMap[y, x] = br.ReadByte();
        }

        private void ReadUVMapEntries(BinaryReader br, MH2OInstance instance)
        {
            UVMap = new UVMapEntry[instance.Height + 1, instance.Width + 1];
            for (byte y = 0; y < instance.Height + 1; y++)
                for (byte x = 0; x < instance.Width + 1; x++)
                    UVMap[y, x] = br.ReadUVMapEntry();
        }

        private void WriteHeightMap(BinaryWriter bw, MH2OInstance instance)
        {
            for (byte y = 0; y < instance.Height + 1; y++)
                for (byte x = 0; x < instance.Width + 1; x++)
                    bw.Write(HeightMap[y, x]);
        }

        private void WriteDepthMap(BinaryWriter bw, MH2OInstance instance)
        {
            for (byte y = 0; y < instance.Height + 1; y++)
                for (byte x = 0; x < instance.Width + 1; x++)
                    bw.Write(DepthMap[y, x]);
        }

        private void WriteUVMapEntries(BinaryWriter bw, MH2OInstance instance)
        {
            for (byte y = 0; y < instance.Height + 1; y++)
                for (byte x = 0; x < instance.Width + 1; x++)
                    bw.WriteUVMapEntry(UVMap[y, x]);
        }
    }
}
