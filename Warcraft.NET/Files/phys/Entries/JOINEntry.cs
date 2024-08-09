using System;
using System.IO;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class JOINEntry
    {


        public uint bodyAIdx;
        public uint bodyBIdx;
        public byte[] unk;

        public ushort jointType;
        public ushort jointId;

        /// <summary>
        /// Initializes a new instance of the <see cref="JOINEntry"/> class.
        /// </summary>
        public JOINEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="JOINEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public JOINEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                bodyAIdx = br.ReadUInt32();
                bodyBIdx = br.ReadUInt32();
                unk = br.ReadBytes(4);
                jointType = br.ReadUInt16();
                jointId = br.ReadUInt16();
            }
        }

        /// <summary>
        /// Gets the size of a JOIN entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 16;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(bodyAIdx);
                    bw.Write(bodyBIdx);
                    bw.Write(unk);
                    bw.Write(jointType);
                    bw.Write(jointId);

                }

                return ms.ToArray();
            }
        }
    }
}
