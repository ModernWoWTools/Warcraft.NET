using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using System.IO;
using Warcraft.NET.Files.ADT.TerrainTexture.MCMK.SubChunks;

namespace Warcraft.NET.Files.ADT.TerrainTexture
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
        /// Gets or sets the alphamap Layer chunk.
        /// </summary>
        public MCLY TextureLayers { get; set; }

        /// <summary>
        /// Gets or sets the the baked shadows.
        /// </summary>
        public MCSH BakedShadows { get; set; }

        /// <summary>
        /// Gets or sets the alphamap chunk.
        /// </summary>
        public MCAL AlphaMaps { get; set; }

        /// <summary>
        /// Gets or sets the terrain materials chunk.
        /// </summary>
        public MCMT TerrainMaterials { get; set; }

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
                if (br.SeekChunk(MCLY.Signature))
                    TextureLayers = br.ReadIFFChunk<MCLY>();

                if (br.SeekChunk(MCSH.Signature))
                    BakedShadows = br.ReadIFFChunk<MCSH>();

                if (br.SeekChunk(MCAL.Signature))
                    AlphaMaps = br.ReadIFFChunk<MCAL>();

                if (br.SeekChunk(MCMT.Signature))
                    TerrainMaterials = br.ReadIFFChunk<MCMT>();
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
                if (TextureLayers != null)
                   bw.WriteIFFChunk(TextureLayers);

                if (BakedShadows != null)
                    bw.WriteIFFChunk(BakedShadows);

                if (AlphaMaps != null)
                    bw.WriteIFFChunk(AlphaMaps);

                if (TerrainMaterials != null)
                    bw.WriteIFFChunk(TerrainMaterials);

                return ms.ToArray();
            }
        }
    }
}