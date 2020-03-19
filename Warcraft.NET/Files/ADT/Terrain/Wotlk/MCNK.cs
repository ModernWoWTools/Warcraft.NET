using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.ADT.Terrain.MCMK;
using Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks;
using Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Chunks;

namespace Warcraft.NET.Files.ADT.Terrain.Wotlk
{
    /// <summary>
    /// MCNK - Wotlk MCNK chunk
    /// </summary>
    public class MCNK : MCNKBase
    {
        /// <summary>
        /// Gets or sets the alphamap Layer chunk.
        /// </summary>
        public MCLY TextureLayers { get; set; }

        /// <summary>
        /// Gets or sets model and world object references
        /// </summary>
        public MCRF ModelReferences { get; set; }

        /// <summary>
        /// Gets or sets the the baked shadows.
        /// </summary>
        public MCSH BakedShadows { get; set; }

        /// <summary>
        /// Gets or sets the alphamap chunk.
        /// </summary>
        public MCAL AlphaMaps { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCNK"/> class.
        /// </summary>
        public MCNK()
        {
            var a = 2;
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
        public override void LoadBinaryData(byte[] inData)
        {
            base.LoadBinaryData(inData);

            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                long headerAndSizeOffset = -8;

                // Read MCLY
                if (Header.TextureLayersOffset > 0)
                {
                    ms.Seek(Header.TextureLayersOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    TextureLayers = br.ReadIFFChunk<MCLY>(false, false);
                }

                // Read MCRF
                if (Header.ModelReferencesOffset > 0)
                {
                    ms.Seek(Header.TextureLayersOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    ModelReferences = br.ReadIFFChunk<MCRF>(false, false);
                    ModelReferences.PostLoadReferences(Header.ModelReferenceCount, Header.WorldModelObjectReferenceCount);
                }

                // Read MCSH
                if (Header.BakedShadowsOffset > 0)
                {
                    ms.Seek(Header.BakedShadowsOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    BakedShadows = br.ReadIFFChunk<MCSH>(false, false);
                }

                // Read MCAL
                if (Header.AlphaMapsOffset > 0)
                {
                    ms.Seek(Header.AlphaMapsOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    AlphaMaps = br.ReadIFFChunk<MCAL>(false, false);
                }
            }
        }

        /// <inheritdoc/>
        public override byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                uint headerAndSizeOffset = 8;
                ms.Seek(Header.GetSize(), SeekOrigin.Begin);

                // Write MCVT
                Header.HeightmapOffset = 0;
                if (Heightmap != null)
                {
                    Header.HeightmapOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(Heightmap);
                }

                // ...

                // Write MCCV
                Header.VertexShadingOffset = 0;
                if (VertexShading != null)
                {
                    Header.VertexShadingOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexShading);
                }

                // Write MCLV
                Header.VertexLightingOffset = 0;
                if (VertexLighting != null)
                {
                    Header.VertexLightingOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexLighting);
                }

                // Write MCNR
                Header.VertexNormalOffset = 0;
                if (VertexNormals != null)
                {
                    Header.VertexNormalOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexNormals);
                }

                // Write MCSE
                Header.SoundEmittersOffset = 0;
                Header.SoundEmitterCount = Header.SoundEmitterCount;
                if (VertexNormals != null)
                {
                    Header.SoundEmitterCount = Header.SoundEmitterCount;
                    Header.SoundEmittersOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(SoundEmitters);
                }

                ms.Seek(0, SeekOrigin.Begin);
                bw.Write(Header.Serialize());

                return ms.ToArray();
            }
        }
    }
}