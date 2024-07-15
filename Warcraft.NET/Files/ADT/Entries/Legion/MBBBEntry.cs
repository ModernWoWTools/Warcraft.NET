using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.ADT.Entries.Legion
{
    /// <summary>
    /// Blend mesh bounding box entry.
    /// </summary>
    public class MBBBEntry
    {
        /// <summary>
        /// Unique ID of the map object.
        /// </summary>
        public uint MapObjectID { get; set; }

        /// <summary>
        /// Bounding box.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBBBEntry"/> class.
        /// </summary>
        public MBBBEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBBBEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MBBBEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                MapObjectID = br.ReadUInt32();
                BoundingBox = new BoundingBox(br.ReadVector3(AxisConfiguration.Native), br.ReadVector3(AxisConfiguration.Native));
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 28;
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
                bw.Write(MapObjectID);
                bw.WriteVector3(BoundingBox.Minimum);
                bw.WriteVector3(BoundingBox.Maximum);
                return ms.ToArray();
            }
        }
    }
}