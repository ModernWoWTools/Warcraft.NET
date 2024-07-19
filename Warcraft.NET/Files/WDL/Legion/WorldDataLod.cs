using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.WDL.Chunks;

#nullable enable
namespace Warcraft.NET.Files.WDL.Legion
{
    [AutoDocFile("wdl")]
    public class WorldDataLod : WorldDataLodBase
    {
        /// <summary>
        /// Contains position information for all M2 models in this ADT.
        /// </summary>
        [ChunkOrder(2)]
        public ADT.Chunks.Legion.MLDD LevelDoodadDetail { get; set; } = new();

        /// <summary>
        /// Contains M2 model bounding information. Same count as <see cref="ADT.Chunks.Legion.MLDD"/>
        /// </summary>
        [ChunkOrder(3)]
        public ADT.Chunks.Legion.MLDX LevelDoodadExtent { get; set; } = new();

        /// <summary>
        /// Contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(4)]
        public ADT.Chunks.Legion.MLMD LevelWorldObjectDetail { get; set; } = new();

        /// <summary>
        /// Contains WMO model bounding information. Same count as <see cref="ADT.Chunks.Legion.MLMD"/>
        /// </summary>
        [ChunkOrder(5)]
        public ADT.Chunks.Legion.MLMX LevelWorldObjectExtent { get; set; } = new();

        /// <summary>
        /// Gets or sets the map unkown ocean values.
        /// </summary>
        public List<MAOE?> MapAreaOcean { get; set; } = new List<MAOE?>(4096);

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataLod"/> class.
        /// </summary>
        public WorldDataLod() : base()
        {
            // Set up the two area lists with default values
            for (var i = 0; i < 4096; ++i)
            {
                MapAreaOcean.Add(null);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataLod"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public WorldDataLod(byte[] inData)
        {
            using var ms = new MemoryStream(inData);
            using var br = new BinaryReader(ms);

            Version = br.ReadIFFChunk<MVER>(false, false);

            LevelDoodadDetail = br.ReadIFFChunk<ADT.Chunks.Legion.MLDD>(false, false);
            LevelDoodadExtent = br.ReadIFFChunk<ADT.Chunks.Legion.MLDX>(false, false);
            LevelWorldObjectDetail = br.ReadIFFChunk<ADT.Chunks.Legion.MLMD>(false, false);
            LevelWorldObjectExtent = br.ReadIFFChunk<ADT.Chunks.Legion.MLMX>(false, false);
            MapAreaOffsets = br.ReadIFFChunk<MAOF>(false, true);

            // Set up the two area lists with default values
            for (var i = 0; i < 4096; ++i)
            {
                MapAreas.Add(null);
                MapAreaOcean.Add(null);
                MapAreaHoles.Add(null);
            }

            // Read the map areas and their holes
            for (var y = 0; y < 64; ++y)
            {
                for (var x = 0; x < 64; ++x)
                {
                    var mapAreaOffsetIndex = (y * 64) + x;
                    var mapAreaOffset = MapAreaOffsets.MapAreaOffsets[mapAreaOffsetIndex];

                    if (mapAreaOffset > 0)
                    {
                        br.BaseStream.Position = mapAreaOffset;
                        MapAreas[mapAreaOffsetIndex] = br.ReadIFFChunk<MARE>(false, false);

                        if (br.PeekChunkSignature() == MAOE.Signature)
                        {
                            MapAreaOcean[mapAreaOffsetIndex] = br.ReadIFFChunk<MAOE>(false, false);
                        }

                        if (br.PeekChunkSignature() == MAHO.Signature)
                        {
                            MapAreaHoles[mapAreaOffsetIndex] = br.ReadIFFChunk<MAHO>(false, false);
                        }
                        else
                        {
                            MapAreaHoles[mapAreaOffsetIndex] = MAHO.CreateEmpty();
                        }
                    }
                    else
                    {
                        MapAreas[mapAreaOffsetIndex] = null;
                        MapAreaOcean[mapAreaOffsetIndex] = null;
                        MapAreaHoles[mapAreaOffsetIndex] = null;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public override byte[] Serialize()
        {
            using var ms = new MemoryStream();
            using (var bw = new BinaryWriter(ms))
            {
                bw.WriteIFFChunk(Version);
                bw.WriteIFFChunk(LevelDoodadDetail);
                bw.WriteIFFChunk(LevelDoodadExtent);
                bw.WriteIFFChunk(LevelWorldObjectDetail);
                bw.WriteIFFChunk(LevelWorldObjectExtent);

                // Populate the offset table
                long writtenMapAreaSize = 0;
                for (var y = 0; y < 64; ++y)
                {
                    for (var x = 0; x < 64; ++x)
                    {
                        var mapAreaOffsetIndex = (y * 64) + x;
                        const uint offsetChunkHeaderSize = 8;

                        if (MapAreas[mapAreaOffsetIndex] != null)
                        {
                            // This tile is populated, so we update the offset table
                            var newOffset = (uint)(ms.Position + offsetChunkHeaderSize + MAOF.GetSizeStatic() + writtenMapAreaSize);
                            MapAreaOffsets.MapAreaOffsets[mapAreaOffsetIndex] = newOffset;

                            writtenMapAreaSize += MARE.GetSizeStatic() + offsetChunkHeaderSize;
                            writtenMapAreaSize += MAHO.GetSizeStatic() + offsetChunkHeaderSize;
                        }

                        if (MapAreaOcean[mapAreaOffsetIndex] != null)
                        {
                            writtenMapAreaSize += MAOE.GetSizeStatic() + offsetChunkHeaderSize;
                        }
                    }
                }

                // Write the offset table
                bw.WriteIFFChunk(MapAreaOffsets);

                // Write the valid entries
                for (var y = 0; y < 64; ++y)
                {
                    for (var x = 0; x < 64; ++x)
                    {
                        var mapAreaOffsetIndex = (y * 64) + x;

                        if (MapAreas[mapAreaOffsetIndex] != null)
                        {
                            bw.WriteIFFChunk(MapAreas[mapAreaOffsetIndex]);
                            bw.WriteIFFChunk(MapAreaHoles[mapAreaOffsetIndex] ?? MAHO.CreateEmpty());
                        }

                        if (MapAreaOcean[mapAreaOffsetIndex] != null)
                        {
                            bw.WriteIFFChunk(MapAreaOcean[mapAreaOffsetIndex]);
                        }
                    }
                }
            }

            return ms.ToArray();
        }

        public static explicit operator WorldDataLod(Wotlk.WorldDataLod wdlWotlk)
        {
            return new WorldDataLod()
            {
                Version = wdlWotlk.Version,
                MapAreaOffsets = wdlWotlk.MapAreaOffsets,
                MapAreas = wdlWotlk.MapAreas,
                MapAreaHoles = wdlWotlk.MapAreaHoles,
            };
        }
    }
}
#nullable disable