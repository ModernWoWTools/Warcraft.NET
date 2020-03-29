using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks
{
    /// <summary>
    /// MCVT Chunk - Contains heightmap information
    /// The vertices are arranged as two distinct grids, one inside the other.
    /// </summary>
    public class MCVT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCVT";

        /// <summary>
        /// Gets or sets Vertices
        /// </summary>
        public float[] Vertices { get; set; } = new float[9 * 9 + 8 * 8];

        /// <summary>
        /// Initializes a new instance of the <see cref="MCVT"/> class.
        /// </summary>
        public MCVT()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCVT"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCVT(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                long verticeCount = ms.Length / sizeof(float);

                for (var i = 0; i < verticeCount; ++i)
                {
                    Vertices[i] = br.ReadSingle();
                }
            }
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (float vertice in Vertices)
                {
                    bw.Write(vertice);
                }

                return ms.ToArray();
            }
        }
    }
}
