using SharpDX;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;
using Warcraft.NET.Files.WMO.Flags;

namespace Warcraft.NET.Files.WMO.Entries
{
    /// <summary>
    /// An entry struct containing information about the doodads.
    /// </summary>
    public class MODDEntry
    {
        /// <summary>
        /// Gets or sets doodad name index.
        /// </summary>
        public uint NameIndex { get; set; }

        /// <summary>
        /// Gets or sets doodad position
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the file id of the first texture.
        /// </summary>
        public Quaternion Orientation { get; set; }

        /// <summary>
        /// Gets or sets doodad scale factor
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Gets or sets doodad color
        /// </summary>
        public RGBA Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODDEntry"/> class.
        /// </summary>
        public MODDEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MODDEntry"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MODDEntry(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public static int GetSize()
        {
            return 40;
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                NameIndex = br.ReadUInt32();
                Position = br.ReadVector3();
                Orientation = br.ReadQuaternion();
                Scale = br.ReadSingle();
                Color = br.ReadBGRA();
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(NameIndex);
                bw.WriteVector3(Position);
                bw.WriteQuaternion(Orientation);
                bw.Write(Scale);
                bw.WriteBGRA(Color);

                return ms.ToArray();
            }
        }
    }
}
