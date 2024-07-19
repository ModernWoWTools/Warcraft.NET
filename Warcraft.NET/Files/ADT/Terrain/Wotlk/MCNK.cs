using System;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.ADT.Terrain.MCNK;
using Warcraft.NET.Files.ADT.Terrain.MCNK.Flags;
using Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks;
using Warcraft.NET.Files.ADT.TerrainTexture.MapChunk.Entries;
using Warcraft.NET.Files.ADT.TerrainTexture.MapChunk.SubChunks;

namespace Warcraft.NET.Files.ADT.Terrain.Wotlk
{
    /// <summary>
    /// MCNK - Wotlk MCNK chunk
    /// </summary>
    [AutoDocFile("adt", "Root ADT")]
    [AutoDocChunk(AutoDocChunkVersion.LK, AutoDocChunkVersionHelper.VersionAfterLK)]
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

                // Read MCNR
                if (Header.VertexNormalOffset > 0)
                {
                    ms.Seek(Header.VertexNormalOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    ms.Seek(4, SeekOrigin.Current); // Skip Head
                    int mcnrLength = br.ReadInt32() + MCNR.PaddingLength;
                    VertexNormals = new MCNR(br.ReadBytes(mcnrLength));
                }

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
                    Header.Flags |= MCNKFlags.HasBakedShadows;
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
                    Unk1 = Header.Unk1,
                    LiquidSize = Header.LiquidSize
                };

                ms.Seek(Header.GetSize(), SeekOrigin.Begin);

                // Write MCVT
                if (Heightmap != null)
                {
                    newHeader.HeightmapOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(Heightmap);
                }

                // Write MCCV
                if (VertexShading != null)
                {
                    newHeader.VertexShadingOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexShading);
                }

                // Write MCNR
                if (VertexNormals != null)
                {
                    byte[] padding = VertexNormals.Padding;
                    newHeader.VertexNormalOffset = (uint)ms.Position + headerAndSizeOffset;
                    VertexNormals.Padding = null;

                    bw.WriteIFFChunk(VertexNormals);

                    if (padding != null)
                    {
                        bw.Write(padding);
                    }
                }

                // Write MCLY
                if (TextureLayers != null)
                {
                    newHeader.TextureLayersOffset = (uint)ms.Position + headerAndSizeOffset;
                    newHeader.TextureLayerCount = (uint)TextureLayers.Layers.Count;
                    bw.WriteIFFChunk(TextureLayers);
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
                if (newHeader.Flags.HasFlag(MCNKFlags.HasBakedShadows) && BakedShadows != null)
                {
                    newHeader.BakedShadowsOffset = (uint)ms.Position + headerAndSizeOffset;
                    newHeader.BakedShadowsSize = ModelReferences.GetSize() + 8;
                    bw.WriteIFFChunk(BakedShadows);
                }
                else
                {
                    newHeader.Flags &= ~MCNKFlags.HasBakedShadows;
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

        public void FixGroundEffectMap(bool bigAlpha = false)
        {
            // Reset ground effect map
            Header.GroundEffectMap.Fill((byte)0);

            bool firstLayer = true;
            int layerIndex = 1;
            foreach (MCLYEntry layer in TextureLayers.Layers)
            {
                // skip first layer
                if (firstLayer)
                {
                    firstLayer = false;
                    continue;
                }

                byte[] alphaMap = AlphaMaps.GetAlphaMapForLayer(layer, bigAlpha);

                for (int y = 0; y < 8; ++y)
                {
                    for (int x = 0; x < 8; ++x)
                    {
                        int sum = 0;
                        for (int j = 0; j < 8; ++j)
                        {
                            for (int i = 0; i < 8; ++i)
                            {
                                sum += alphaMap[(y * 8 + j) * 64 + (x * 8 + i)];
                            }
                        }

                        if (sum > 120 * 8 * 8)
                        {
                            int mapIndex = (y * 8 + x) / 4;
                            int bitIndex = ((y * 8 + x) % 4) * 2; // -6

                            Header.GroundEffectMap[mapIndex] |= Convert.ToByte(((layerIndex & 3) << bitIndex));
                        }
                    }
                }

                layerIndex++;
            }
        }
    }
}