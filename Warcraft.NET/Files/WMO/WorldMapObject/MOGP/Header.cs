using SharpDX;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.WMO.WorldMapObject.MOGP.Flags;

namespace Warcraft.NET.Files.WMO.WorldMapObject.MOGP
{
    public class Header
    {
        /// <summary>
        /// Gets or sets the offset of the group name.
        /// </summary>
        public uint GroupNameOffset { get; set; }

        /// <summary>
        /// Gets or sets the offset of the descriptive name.
        /// </summary>
        public uint DescriptiveGroupNameOffset { get; set; }

        /// <summary>
        /// Gets or sets the flags of the group.
        /// </summary>
        public MOGPFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the bounding box of the group.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the first index of the portal references.
        /// </summary>
        public ushort FirstPortalReferenceIndex { get; set; }

        /// <summary>
        /// Gets or sets the number of portal references.
        /// </summary>
        public ushort PortalReferenceCount { get; set; }

        /// <summary>
        /// Gets or sets the rendering batch count.
        /// </summary>
        public ushort RenderBatchCountA { get; set; }

        /// <summary>
        /// Gets or sets the number of interior render batches.
        /// </summary>
        public ushort RenderBatchCountInterior { get; set; }

        /// <summary>
        /// Gets or sets the number of exterior render batches.
        /// </summary>
        public ushort RenderBatchCountExterior { get; set; }

        /// <summary>
        /// Gets or sets an unknown value.
        /// </summary>
        public ushort Unknown { get; set; }

        /// <summary>
        /// Gets the fog indexes.
        /// </summary>
        public byte[] FogIndices { get; set; } = new byte[4];

        /// <summary>
        /// Gets or sets the model's liquid type.
        /// </summary>
        public uint LiquidType { get; set; }

        /// <summary>
        /// Gets or sets the model's group ID.
        /// </summary>
        public uint GroupID { get; set; }

        /// <summary>
        /// Gets or sets a set of unknown flags.
        /// </summary>
        public MOGPTerrainFlags TerrainFlags { get; set; }

        /// <summary>
        /// Gets or sets an unused value.
        /// </summary>
        public uint Unused { get; set; }

        public Header()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public Header(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    GroupNameOffset = br.ReadUInt32();
                    DescriptiveGroupNameOffset = br.ReadUInt32();
                    Flags = (MOGPFlags)br.ReadUInt32();
                    BoundingBox = br.ReadBoundingBox();
                    FirstPortalReferenceIndex = br.ReadUInt16();
                    PortalReferenceCount = br.ReadUInt16();
                    RenderBatchCountA = br.ReadUInt16();
                    RenderBatchCountInterior = br.ReadUInt16();
                    RenderBatchCountExterior = br.ReadUInt16();
                    Unknown = br.ReadUInt16();
                    FogIndices = br.ReadBytes(4);
                    LiquidType = br.ReadUInt32();
                    GroupID = br.ReadUInt32();
                    TerrainFlags = (MOGPTerrainFlags)br.ReadUInt32();
                    Unused = br.ReadUInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of the data contained in this chunk.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 68;
        }

        /// <summary>
        /// Serialize chunk data to byte array
        /// </summary>
        /// <returns>byte array</returns>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(GroupNameOffset);
                bw.Write(DescriptiveGroupNameOffset);
                bw.Write((uint)Flags);
                bw.WriteBoundingBox(BoundingBox);
                bw.Write(FirstPortalReferenceIndex);
                bw.Write(PortalReferenceCount);
                bw.Write(RenderBatchCountA);
                bw.Write(RenderBatchCountInterior);
                bw.Write(RenderBatchCountExterior);
                bw.Write(Unknown);
                bw.Write(FogIndices);
                bw.Write(LiquidType);
                bw.Write(GroupID);
                bw.Write((uint)TerrainFlags);
                bw.Write(Unused);

                return ms.ToArray();
            }
        }
    }
}
