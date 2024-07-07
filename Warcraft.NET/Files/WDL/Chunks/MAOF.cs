using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WDL.Chunks
{
    /// <summary>
    /// MAOF Chunk - Represents the offsets of a map area.
    /// </summary>
    public class MAOF : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MAOF";

        /// <summary>
        /// Gets the map area offsets.
        /// </summary>
        public List<uint> MapAreaOffsets = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOF"/> class.
        /// </summary>
        public MAOF() { }

        public static MAOF CreateEmpty()
        {
            MAOF maof = new MAOF();
            // Set up with default values
            for (var i = 0; i < 4096; ++i)
            {
                maof.MapAreaOffsets.Add(0);
            }

            return maof;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAOF"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MAOF(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using var ms = new MemoryStream(inData);
            using var br = new BinaryReader(ms);
            for (var y = 0; y < 64; ++y)
            {
                for (var x = 0; x < 64; ++x)
                {
                    MapAreaOffsets.Add(br.ReadUInt32());
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
            return GetSizeStatic();
        }

        /// <inheritdoc/>
        public static uint GetSizeStatic()
        {
            return (64 * 64) * sizeof(uint);
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var mapAreaOffset in MapAreaOffsets)
                {
                    bw.Write(mapAreaOffset);
                }

                return ms.ToArray();
            }
        }
    }
}