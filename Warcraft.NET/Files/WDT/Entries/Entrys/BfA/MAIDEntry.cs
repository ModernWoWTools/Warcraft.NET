using System.IO;

namespace Warcraft.NET.Files.WDT.Entries.BfA
{
    /// <summary>
    /// An entry struct containing adt part file ids.
    /// </summary>
    public class MAIDEntry
    {
        /// <summary>
        /// Reference to file id of mapname_xx_yy.adt
        /// </summary>
        public uint RootAdtFileId { get; set; } = 0;

        /// <summary>
        /// Reference to file id of mapname_xx_yy_obj0.adt
        /// </summary>
        public uint Obj0AdtFileId { get; set; } = 0;

        /// <summary>
        /// Reference to file id of mapname_xx_yy_obj1.adt
        /// </summary>
        public uint Obj1AdtFileId { get; set; } = 0;

        /// <summary>
        /// Reference to file id of mapname_xx_yy_tex0.adt
        /// </summary>
        public uint Tex0AdtFileId { get; set; } = 0;

        /// <summary>
        /// Reference to file id of mapname_xx_yy_lod.adt
        /// </summary>
        public uint LodAdtFileId { get; set; } = 0;

        /// <summary>
        /// Reference to file id of mapname_xx_yy.blp
        /// </summary>
        public uint MapTextureFileId { get; set; } = 0;

        /// <summary>
        /// Reference to file id of mapname_xx_yy_n.blp
        /// </summary>
        public uint MapTextureNFileId { get; set; } = 0;

        /// <summary>
        /// Reference to file id of mapxx_yy.blp
        /// </summary>
        public uint MinimapTextureFileId { get; set; } = 0;

        public MAIDEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAIDEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MAIDEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    RootAdtFileId = br.ReadUInt32();
                    Obj0AdtFileId = br.ReadUInt32();
                    Obj1AdtFileId = br.ReadUInt32();
                    Tex0AdtFileId = br.ReadUInt32();
                    LodAdtFileId = br.ReadUInt32();
                    MapTextureFileId = br.ReadUInt32();
                    MapTextureNFileId = br.ReadUInt32();
                    MinimapTextureFileId = br.ReadUInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 32;
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
                bw.Write(RootAdtFileId);
                bw.Write(Obj0AdtFileId);
                bw.Write(Obj1AdtFileId);
                bw.Write(Tex0AdtFileId);
                bw.Write(LodAdtFileId);
                bw.Write(MapTextureFileId);
                bw.Write(MapTextureNFileId);
                bw.Write(MinimapTextureFileId);

                return ms.ToArray();
            }
        }
    }
}
