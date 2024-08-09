using System.IO;

namespace Warcraft.NET.Files.M2.Entries
{
    public class AFIDEntry
    {
        /// <summary>
        /// Gets or sets the animation id.
        /// </summary>
        public ushort AnimationId { get; set; }


        /// <summary>
        /// Gets or sets the sub animation id.
        /// </summary>
        public ushort SubAnimationId { get; set; }

        /// <summary>
        /// Gets or sets the sub file data id. Might be 0 for "none" (so this is probably not sparse, even if it could be)
        /// </summary>
        public uint FileDataId { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="AFIDEntry"/> class.
        /// </summary>
        public AFIDEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AFIDEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public AFIDEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                AnimationId = br.ReadUInt16();
                SubAnimationId = br.ReadUInt16();
                FileDataId = br.ReadUInt32();
            }
        }

        /// <summary>
        /// Gets the size of a animation file id entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 8;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(AnimationId);
                    bw.Write(SubAnimationId);
                    bw.Write(FileDataId);
                }

                return ms.ToArray();
            }
        }
    }
}
