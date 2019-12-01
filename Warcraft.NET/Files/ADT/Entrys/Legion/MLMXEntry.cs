using Warcraft.NET.Extensions;
using SharpDX;
using System.IO;

namespace Warcraft.NET.Files.ADT.Entrys.Legion
{
    /// <summary>
    /// An entry struct containing information about model bounding.
    /// </summary>
    public class MLMXEntry
    {
        /// <summary>
        /// Gets or sets the bounding box of the model.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the radius of the model.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLMXEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MLMXEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                BoundingBox = br.ReadBoundingBox();
                Radius = br.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return sizeof(float) * 7;
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
                bw.WriteBoundingBox(BoundingBox);
                bw.Write(Radius);

                return ms.ToArray();
            }
        }
    }
}