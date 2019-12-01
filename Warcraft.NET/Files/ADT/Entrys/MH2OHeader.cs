using System.IO;

namespace Warcraft.NET.Files.ADT.Entrys
{
    /// <summary>
    /// Contains informations for each subchunk on the ADT.
    /// </summary>
    public class MH2OHeader
    {
        /// <summary>
        /// Gets or sets the offset of the <see cref="MH2OInstance"/>s.
        /// </summary>
        public uint OffsetInstances { get; set; }

        /// <summary>
        /// Gets or sets the amount of <see cref="MH2OInstance"/>.
        /// </summary>
        public uint LayerCount { get; set; }

        /// <summary>
        /// Gets or sets the offset of the <see cref="MH2OAttribute"/>s.
        /// </summary>
        public uint OffsetAttributes { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MH2OInstance"/>s of the current <see cref="MH2OHeader"/>.
        /// </summary>
        public MH2OInstance[] Instances { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MH2OAttribute"/> of the current <see cref="MH2OHeader"/>.
        /// </summary>
        public MH2OAttribute Attributes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2OHeader"/> class.
        /// </summary>
        /// <param name="inData"></param>
        public MH2OHeader(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                OffsetInstances = br.ReadUInt32();
                LayerCount = br.ReadUInt32();
                Instances = new MH2OInstance[LayerCount];
                OffsetAttributes = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return sizeof(uint) * 3;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(OffsetInstances);
                bw.Write(LayerCount);
                bw.Write(OffsetAttributes);
                return ms.ToArray();
            }
        }
    }
}
