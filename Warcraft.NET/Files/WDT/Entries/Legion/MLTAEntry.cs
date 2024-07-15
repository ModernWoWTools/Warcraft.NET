using System.IO;

namespace Warcraft.NET.Files.WDT.Entries.Legion
{
    /// <summary>
    /// An entry struct containing light animations
    /// </summary>
    public class MLTAEntry
    {
        /// <summary>
        /// Flicker intensity
        /// </summary>
        public float FlickerIntensity { get; set; }

        /// <summary>
        /// Flicker intensity
        /// </summary>
        public float FlickerSpeed { get; set; }

        /// <summary>
        /// Flicker mode
        /// 0 = off, 1 = sine curve, 2 = noise curve, 3 = noise step curve
        /// </summary>
        public int FlickerMode { get; set; }

        public MLTAEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLTAEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MLTAEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    FlickerIntensity = br.ReadSingle();
                    FlickerSpeed = br.ReadSingle();
                    FlickerMode = br.ReadInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 12;
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
                bw.Write(FlickerIntensity);
                bw.Write(FlickerSpeed);
                bw.Write(FlickerMode);

                return ms.ToArray();
            }
        }
    }
}
