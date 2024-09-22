using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class BOXSEntry
    {
        /// <summary>
        /// Gets or Sets the transformation matrix of the box shape
        /// </summary>
        public Matrix3x4 Dimensions;

        /// <summary>
        /// Gets or Sets the local position of the box shape
        /// </summary>
        public Vector3 Position;

        public BOXSEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                Dimensions = br.ReadMatrix3x4();
                Position = br.ReadVector3();
            }
        }

        /// <summary>
        /// Gets the size of a box entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 60;
        }
        
        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.WriteMatrix3x4(Dimensions);
                    bw.WriteVector3(Position);
                }
                return ms.ToArray();
            }
        }
    }
}