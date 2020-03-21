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
                Header newHeader = new Header()
                {
                    Flags = Header.Flags,
                    MapIndexX = Header.MapIndexX,
                    MapIndexY = Header.MapIndexY,
                    ModelReferenceCount = Header.ModelReferenceCount,
                    HighResHoles = Header.HighResHoles,
                    AreaID = Header.AreaID,
                    LowResHoles = Header.LowResHoles,
                    Unk0 = Header.Unk0,
                    GroundEffectMap = Header.GroundEffectMap,
                    LowResTextureMap = Header.LowResTextureMap,
                    PredTex = Header.PredTex,
                    NoEffectDoodad = Header.NoEffectDoodad,
                    MapTilePosition = Header.MapTilePosition,
                    Unk1 = Header.Unk1
                };

                ms.Seek(Header.GetSize(), SeekOrigin.Begin);

                // Write MCVT
                if (Heightmap != null)
                {
                    newHeader.HeightmapOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(Heightmap);
                }

                // Write MCNR
                if (VertexNormals != null)
                {
                    newHeader.VertexNormalOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexNormals);
                }

                // Write MCCV
                if (VertexShading != null)
                {
                    newHeader.VertexShadingOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexShading);
                }

                // Write MCLV
                if (VertexLighting != null)
                {
                    newHeader.VertexLightingOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexLighting);
                }

                // Write MCLY
                if (TextureLayers != null)
                {
                    newHeader.TextureLayersOffset = (uint)ms.Position + headerAndSizeOffset;
                    newHeader.TextureLayerCount = (uint)TextureLayers.Layers.Count;
                    bw.WriteIFFChunk(VertexNormals);
                }

                // Write MCRF
                if (ModelReferences != null)
                {
                    newHeader.ModelReferencesOffset = (uint)ms.Position + headerAndSizeOffset;
                    newHeader.ModelReferenceCount = (uint)ModelReferences.ModelReferences.Count;
                    newHeader.WorldModelObjectReferenceCount = (uint)ModelReferences.WorldObjectReferences.Count;
                    bw.WriteIFFChunk(ModelReferences);
                }

                // Write MCSH
                if (newHeader.Flags.HasFlag(MCMK.Flags.MCNKFlags.HasBakedShadows) && BakedShadows != null)
                {
                    newHeader.BakedShadowsOffset = (uint)ms.Position + headerAndSizeOffset;
                    newHeader.BakedShadowsSize = ModelReferences.GetSize() + 8;
                    bw.WriteIFFChunk(ModelReferences);
                }

                // Write MCAL
                if (AlphaMaps != null)
                {
                    newHeader.AlphaMapsOffset = (uint)ms.Position + headerAndSizeOffset;
                    newHeader.AlphaMapsSize = AlphaMaps.GetSize() + 8;
                    bw.WriteIFFChunk(AlphaMaps);
                }

                // Write MCSE
                if (SoundEmitters != null)
                {
                    newHeader.SoundEmittersOffset = (uint)ms.Position + headerAndSizeOffset;
                    newHeader.SoundEmitterCount = 0;
                    bw.WriteIFFChunk(SoundEmitters);
                }

                ms.Seek(0, SeekOrigin.Begin);
                bw.Write(newHeader.Serialize());

                return ms.ToArray();
            }
        }
    }
}