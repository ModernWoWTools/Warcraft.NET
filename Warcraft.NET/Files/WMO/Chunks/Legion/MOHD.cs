using SharpDX;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;
using Warcraft.NET.Files.WMO.Flags.Legion;

namespace Warcraft.NET.Files.WMO.Chunks.Legion
{
    /// <summary>
    /// MOHD Chunk - Header chunk of the WMO root file that contains informations about it's data
    /// </summary>
    public class MOHD : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MOHD";

        /// <summary>
        /// Gets or sets the number of materials.
        /// </summary>
        public uint Materials { get; set; }
        
        /// <summary>
        /// Gets or sets the number of groups.
        /// </summary>
        public uint Groups { get; set; }

        /// <summary>
        /// Gets or sets the number of portals.
        /// </summary>
        public uint Portals { get; set; }

        /// <summary>
        /// Gets or sets the number of lights.
        /// </summary>
        public uint Lights { get; set; }

        /// <summary>
        /// Gets or sets the number of doodad names.
        /// </summary>
        public uint DoodadNames { get; set; }

        /// <summary>
        /// Gets or sets the number of doodad definitions.
        /// </summary>
        public uint DoodadDefinitions { get; set; }

        /// <summary>
        /// Gets or sets the number of doodad sets.
        /// </summary>
        public uint DoodadSets { get; set; }

        /// <summary>
        /// Gets or sets the base (ambient) color.
        /// </summary>
        public RGBA Color { get; set; }

        /// <summary>
        /// Gets or sets the WMO id.
        /// </summary>
        public uint WMOId { get; set; }

        /// <summary>
        /// Gets or sets the bounding box.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the WMO flags.
        /// </summary>
        public MOHDFlags Flags { get; set; }

        /// <summary>
        /// Gets or sets the lod number. Includes base lod (<see cref="NumLod"/> = 3 means '.wmo', 'lod0.wmo' and 'lod1.wmo')
        /// </summary>
        public ushort NumLod { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MOHD"/> class.
        /// </summary>
        public MOHD()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MOHD"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MOHD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Materials = br.ReadUInt32();
                Groups = br.ReadUInt32();
                Portals = br.ReadUInt32();
                Lights = br.ReadUInt32();
                DoodadNames = br.ReadUInt32();
                DoodadDefinitions = br.ReadUInt32();
                DoodadSets = br.ReadUInt32();
                Color = br.ReadRGBA();
                WMOId = br.ReadUInt32();
                BoundingBox = br.ReadBoundingBox();
                Flags = (MOHDFlags)br.ReadUInt16();
                NumLod = br.ReadUInt16();
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Materials);
                bw.Write(Groups);
                bw.Write(Portals);
                bw.Write(Lights);
                bw.Write(DoodadNames);
                bw.Write(DoodadDefinitions);
                bw.Write(DoodadSets);
                bw.WriteRGBA(Color);
                bw.Write(WMOId);
                bw.WriteBoundingBox(BoundingBox);
                bw.Write((ushort)Flags);
                bw.Write(NumLod);
                return ms.ToArray();
            }
        }
    }
}
