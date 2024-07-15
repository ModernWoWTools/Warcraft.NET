using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.ADT.Entries.Legion
{
    /// <summary>
    /// Blend mesh vertices entry.
    /// </summary>
    public class MBNVEntry
    {
        /// <summary>
        /// Vertex position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Vertex normal.
        /// </summary>
        public Vector3 Normal { get; set; }

        /// <summary>
        /// Texture/UV coordinate.
        /// </summary>
        public Vector2 TextureCoordinates { get; set; }

        /// <summary>
        /// Color.
        /// </summary>
        public uint[] Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBNVEntry"/> class.
        /// </summary>
        public MBNVEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MBNVEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public MBNVEntry(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                Normal = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                TextureCoordinates = new Vector2(br.ReadSingle(), br.ReadSingle());
                Color = new uint[3];
                for (var i = 0; i < 3; i++)
                {
                    Color[i] = br.ReadUInt32();
                }
            }
        }

        /// <summary>
        /// Gets the size of an entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 44;
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
                bw.WriteVector3(Position);
                bw.WriteVector3(Normal);
                bw.Write(TextureCoordinates.X);
                bw.Write(TextureCoordinates.Y);

                for (var i = 0; i < 3; i++)
                {
                    bw.Write(Color[i]);
                }

                return ms.ToArray();
            }
        }
    }
}