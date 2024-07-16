using System.IO;

namespace Warcraft.NET.Files.ADT.Entries.Legion
{
    /// <summary>
    /// Blend mesh header entry.
    /// </summary>
    public class MBMHEntry
    {
        /// <summary>
        /// Unique ID of the map object.
        /// </summary>
        public uint MapObjectID { get; set; }

        /// <summary>
        /// Texture ID of linked WMO.
        /// </summary>
        public uint TextureID { get; set; }

        /// <summary>
        /// Unknown value (wiki says always 0).
        /// </summary>
        public uint Unknown0 { get; set; }

        /// <summary>
        /// Number of records in MBMI chunk for this mesh.
        /// </summary>
        public uint MBMICount { get; set; }

        /// <summary>
        /// Number of records in MBNV chunk for this mesh.
        /// </summary>
        public uint MBNVCount { get; set; }

        /// <summary>
        /// Offset to start record in MBMI chunk for this mesh.
        /// </summary>
        public uint MBMIStart { get; set; }

        /// <summary>
        /// Offset to start record in MBNV chunk for this mesh.
        /// </summary>
        public uint MBNVStart { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBMHEntry"/> class.
        /// </summary>
        public MBMHEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBMHEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MBMHEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                MapObjectID = br.ReadUInt32();
                TextureID = br.ReadUInt32();
                Unknown0 = br.ReadUInt32();
                MBMICount = br.ReadUInt32();
                MBNVCount = br.ReadUInt32();
                MBMIStart = br.ReadUInt32();
                MBNVStart = br.ReadUInt32();
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
                bw.Write(TextureID);
                bw.Write(Unknown0);
                bw.Write(MBMICount);
                bw.Write(MBNVCount);
                bw.Write(MBMIStart);
                bw.Write(MBNVStart);

                return ms.ToArray();
            }
        }
    }
}