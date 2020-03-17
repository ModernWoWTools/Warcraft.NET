using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using System.IO;
using Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Chunks;
using Warcraft.NET.Files.ADT.TerrainObject.Zero.MCMK.Chunks;

namespace Warcraft.NET.Files.ADT.TerrainObject.Zero
{
    /// <summary>
    /// MCNK
    /// </summary>
    public class MCNK : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCNK";

        /// <summary>
        /// Gets or sets the map model references chunk.
        /// </summary>
        public MCRD ModelReferences { get; set; }

        /// <summary>
        /// Gets or sets the map world object references chunk.
        /// </summary>
        public MCRW WorldObjectReferences { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCNK"/> class.
        /// </summary>
        public MCNK()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCNK"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCNK(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                if (br.SeekChunk(MCRD.Signature))
                    ModelReferences = br.ReadIFFChunk<MCRD>();

                if (br.SeekChunk(MCRW.Signature))
                    WorldObjectReferences = br.ReadIFFChunk<MCRW>();
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
                if (ModelReferences != null && ModelReferences.ModelReferences.Length > 0)
                    bw.WriteIFFChunk(ModelReferences);

                if (WorldObjectReferences != null && WorldObjectReferences.WorldObjectReferences.Length > 0)
                    bw.WriteIFFChunk(WorldObjectReferences);

                return ms.ToArray();
            }
        }
    }
}