using Warcraft.NET.Extensions;
using System.IO;
using System.Numerics;
using Warcraft.NET.Files.Structures;
using Warcraft.NET.Files.ADT.Flags;

namespace Warcraft.NET.Files.ADT.Entrys
{
    /// <summary>
    /// An entry struct containing information about the model.
    /// </summary>
    public class MDDFEntry 
    {
        /// <summary>
        /// Gets or sets the specifies which model to use via the MMID chunk.
        /// </summary>
        public uint NameId { get; set; }

        /// <summary>
        /// Gets or sets the a unique actor ID for the model. Blizzard implements this as game global, but it's only
        /// checked in loaded tile.
        /// </summary>
        public uint UniqueID { get; set; }

        /// <summary>
        /// Gets or sets the position of the model.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the rotation of the model.
        /// </summary>
        public Rotator Rotation { get; set; }

        /// <summary>
        /// Gets or sets the scale of the model. 1024 is the default value, equating to 1.0f. There is no uneven scaling
        /// of objects.
        /// </summary>
        public ushort ScalingFactor { get; set; }

        /// <summary>
        /// Gets or sets the MMDF flags for the object.
        /// </summary>
        public MDDFFlags Flags { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDDFEntry"/> class.
        /// </summary>
        public MDDFEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MDDFEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MDDFEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    NameId = br.ReadUInt32();
                    UniqueID = br.ReadUInt32();
                    Position = br.ReadVector3(AxisConfiguration.Native);
                    Rotation = br.ReadRotator();

                    ScalingFactor = br.ReadUInt16();
                    Flags = (MDDFFlags)br.ReadUInt16();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 36;
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
                bw.Write(NameId);
                bw.Write(UniqueID);
                bw.WriteVector3(Position, AxisConfiguration.Native);
                bw.WriteRotator(Rotation);
                bw.Write(ScalingFactor);
                bw.Write((ushort)Flags);

                return ms.ToArray();
            }
        }
    }
}
