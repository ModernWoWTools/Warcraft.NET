using System.IO;
using System.Linq;

namespace Warcraft.NET.Files.ADT.Entrys
{
    public class MH2OAttribute
    {
        /// <summary>
        /// Seems to be useable as visibility information
        /// </summary>
        public byte[] Fishable { get; set; } = new byte[8];

        /// <summary>
        /// Gets or sets the deepness.
        /// </summary>
        public byte[] Deep { get; set; } = new byte[8];

        /// <summary>
        /// Gets if current <see cref="MH2OAttribute"/> can be ommitted.
        /// </summary>
        public bool HasOnlyZeroes => Fishable.All(b => b == 0) && Deep.All(b => b == 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="MH2OAttribute"/> class.
        /// </summary>
        /// <param name="inData"></param>
        public MH2OAttribute(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Fishable = br.ReadBytes(8);
                Deep = br.ReadBytes(8);
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return sizeof(byte) * 16;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Fishable);
                bw.Write(Deep);

                return ms.ToArray();
            }
        }
    }
}
