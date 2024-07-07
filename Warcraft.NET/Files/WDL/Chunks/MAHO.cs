using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WDL.Chunks
{
    /// <summary>
    /// MAHO Chunk - Represents the holes in a LOD level of a map area.
    /// </summary>
    public class MAHO : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MAHO";

        /// <summary>
        /// Gets the hole masks.
        /// </summary>
        public List<short> HoleMasks { get; } = new();

        /// <summary>
        /// Gets a value indicating whether the area has no holes.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return HoleMasks.TrueForAll(sh => sh == 0);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAHO"/> class.
        /// </summary>
        public MAHO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAHO"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MAHO(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using var ms = new MemoryStream(inData);
            using var br = new BinaryReader(ms);
            for (var i = 0; i < 16; ++i)
            {
                HoleMasks.Add(br.ReadInt16());
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
            return GetSizeStatic();
        }

        /// <inheritdoc/>
        public static uint GetSizeStatic()
        {
            return 16 * sizeof(short);
        }

        /// <summary>
        /// Creates an empty hole chunk, where all values are set to 0.
        /// </summary>
        /// <returns>An empty chunk.</returns>
        public static MAHO CreateEmpty()
        {
            return new MAHO(new byte[32]);
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var holeMask in HoleMasks)
                {
                    bw.Write(holeMask);
                }
            }

            return ms.ToArray();
        }
    }
}