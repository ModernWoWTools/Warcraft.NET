using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.WDT.Entries.BfA
{
    /// <summary>
    /// An entry struct containing volume fog information
    /// </summary>
    public class VFOGEntry
    {
        /// <summary>
        /// Color.
        /// </summary>
        public Vector3 Color { get; set; }

        /// <summary>
        /// Radius-related intensity. Unconfirmed.
        /// </summary>
        public float[] RadiusRelatedIntensity { get; set; }

        /// <summary>
        /// Unknown 0.
        /// </summary>
        public float Unknown0 { get; set; }

        /// <summary>
        /// Position (server coordinates).
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Unknown 1. Set to 1.0f on loading.
        /// </summary>
        public float Unknown1 { get; set; }

        /// <summary>
        /// Rotation.
        /// </summary>
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// Fog start radius. Unconfirmed.
        /// </summary>
        public float[] StartRadius { get; set; }

        /// <summary>
        /// Fog levels. Unconfirmed.
        /// </summary>
        public uint[] FogLevels { get; set; }

        /// <summary>
        /// Model FileDataID.
        /// </summary>
        public uint ModelFileDataID { get; set; }

        /// <summary>
        /// Unknown 2. Always 1?
        /// </summary>
        public uint Unknown2 { get; set; }

        /// <summary>
        /// ID.
        /// </summary>
        public uint ID { get; set; }


        public VFOGEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="VFOGEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public VFOGEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var br = new BinaryReader(ms))
                {
                    Color = br.ReadVector3(Structures.AxisConfiguration.Native);
                    RadiusRelatedIntensity = [br.ReadSingle(), br.ReadSingle(), br.ReadSingle()];
                    Unknown0 = br.ReadSingle();
                    Position = br.ReadVector3(Structures.AxisConfiguration.Native);
                    Unknown1 = br.ReadSingle();
                    Rotation = br.ReadQuaternion();
                    StartRadius = [br.ReadSingle(), br.ReadSingle(), br.ReadSingle()];
                    FogLevels = [br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32(), br.ReadUInt32()];
                    ModelFileDataID = br.ReadUInt32();
                    Unknown2 = br.ReadUInt32();
                    ID = br.ReadUInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 104;
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
                bw.WriteVector3(Color, Structures.AxisConfiguration.Native);
                bw.Write(RadiusRelatedIntensity[0]);
                bw.Write(RadiusRelatedIntensity[1]);
                bw.Write(RadiusRelatedIntensity[2]);
                bw.Write(Unknown0);
                bw.WriteVector3(Position, Structures.AxisConfiguration.Native);
                bw.Write(Unknown1);
                bw.WriteQuaternion(Rotation);
                bw.Write(StartRadius[0]);
                bw.Write(StartRadius[1]);
                bw.Write(StartRadius[2]);
                bw.Write(FogLevels[0]);
                bw.Write(FogLevels[1]);
                bw.Write(FogLevels[2]);
                bw.Write(FogLevels[3]);
                bw.Write(FogLevels[4]);
                bw.Write(ModelFileDataID);
                bw.Write(Unknown2);
                bw.Write(ID);

                return ms.ToArray();
            }
        }
    }
}
