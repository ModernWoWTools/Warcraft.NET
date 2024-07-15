using System.IO;

namespace Warcraft.NET.Files.ADT.Entries.Legion
{
    /// <summary>
    /// LOD level entry.
    /// </summary>
    public class MLLLEntry
    {
        /// <summary>
        /// LOD bands.
        /// </summary>
        public float LODBands { get; set; }

        /// <summary>
        /// Height length.
        /// </summary>
        public uint HeightLength { get; set; }

        /// <summary>
        /// Height index.
        /// </summary>
        public uint HeightIndex { get; set; }

        /// <summary>
        /// MapAreaLow (WDL) length.
        /// </summary>
        public uint MapAreaLowLength { get; set; }

        /// <summary>
        /// MapAreaLow (WDL) index.
        /// </summary>
        public uint MapAreaLowIndex { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLLEntry"/> class.
        /// </summary>
        public MLLLEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLLLEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MLLLEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                LODBands = br.ReadSingle();
                HeightLength = br.ReadUInt32();
                HeightIndex = br.ReadUInt32();
                MapAreaLowLength = br.ReadUInt32();
                MapAreaLowIndex = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 20;
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
                bw.Write(LODBands);
                bw.Write(HeightLength);
                bw.Write(HeightIndex);
                bw.Write(MapAreaLowLength);
                bw.Write(MapAreaLowIndex);
                return ms.ToArray();
            }
        }
    }
}