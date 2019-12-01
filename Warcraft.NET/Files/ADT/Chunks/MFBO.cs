using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks
{
    /// <summary>
    /// MFBO chunk - holds a bounding box for the terrain chunk.
    /// </summary>
    public class MFBO : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MFBO";

        /// <summary>
        /// Gets or sets the maximum bounding plane.
        /// </summary>
        public ShortPlane Maximum { get; set; }

        /// <summary>
        /// Gets or sets the minimum bounding plane.
        /// </summary>
        public ShortPlane Minimum { get; set; }

        public MFBO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MFBO"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public MFBO(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Maximum = br.ReadShortPlane();
                Minimum = br.ReadShortPlane();
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
                bw.WriteShortPlane(Maximum);
                bw.WriteShortPlane(Minimum);

                return ms.ToArray();
            }
        }
    }
}
