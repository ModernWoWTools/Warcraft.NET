using Warcraft.NET.Attribute;
using Warcraft.NET.Files.WDL.Chunks;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Extensions;
using System;

namespace Warcraft.NET.Files.WDL
{
    public class WorldDataLod : ChunkedFile
    {
        /// <summary>
        /// Gets or sets the contains the WDL version.
        /// </summary>
        [ChunkOrder(1)]
        public MVER Version { get; set; } = new MVER(18);

        /// <summary>
        /// Contains position information for all M2 models in this ADT.
        /// </summary>
        [ChunkOrder(2)]
        public ADT.Chunks.Legion.MLDD LevelDoodadDetail { get; set; } = new ADT.Chunks.Legion.MLDD();

        /// <summary>
        /// Contains M2 model bounding information. Same count as <see cref="ADT.Chunks.Legion.MLDD"/>
        /// </summary>
        [ChunkOrder(3)]
        public ADT.Chunks.Legion.MLDX LevelDoodadExtent { get; set; } = new ADT.Chunks.Legion.MLDX();

        /// <summary>
        /// Contains position information for all WMO models in this ADT.
        /// </summary>
        [ChunkOrder(4)]
        public ADT.Chunks.Legion.MLMD LevelWorldObjectDetail { get; set; } = new ADT.Chunks.Legion.MLMD();

        /// <summary>
        /// Contains WMO model bounding information. Same count as <see cref="ADT.Chunks.Legion.MLMD"/>
        /// </summary>
        [ChunkOrder(5)]
        public ADT.Chunks.Legion.MLMX LevelWorldObjectExtent { get; set; } = new ADT.Chunks.Legion.MLMX();

        /// <summary>
        /// Gets the map area offsets.
        /// </summary>
        [ChunkOrder(6)]
        public MAOF MapAreaOffsets { get; set; } = MAOF.CreateEmpty();

        /// <summary>
        /// Gets or sets the map areas.
        /// </summary>
        [ChunkOrder(7)]
        public List<MARE?> MapAreas { get; set; } = new List<MARE?>(4096);

        /// <summary>
        /// Gets or sets the map unkown ocean values.
        /// </summary>
        [ChunkOrder(8)]
        public List<MAOE?> MapAreaOcean { get; set; } = new List<MAOE?>(4096);

        /// <summary>
        /// Gets or sets the map area holes.
        /// </summary>
        [ChunkOrder(9)]
        public List<MAHO?> MapAreaHoles { get; set; } = new List<MAHO?>(4096);

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
        /// </summary>
        public WorldDataLod()
        {
            // Set up the two area lists with default values
            for (var i = 0; i < 4096; ++i)
            {
                MapAreas.Add(null);
                MapAreaOcean.Add(null);
                MapAreaHoles.Add(null);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataTableBase"/> class.
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
            MapAreaOffsets = br.ReadIFFChunk<MAOF>(false, false);

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

        /// <summary>
        /// Determines if the LOD world has an entry at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>true if the LOD world has an entry at the given coordinate; otherwise, false.</returns>
        public bool HasEntry(int x, int y)
        {
            if (x < 0 || y < 0 || x > 63 || y > 63)
                return false;

            var index = x + (y * 64);
            return MapAreas[index] != null;
        }

        /// <summary>
        /// Gets the entry at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The entry.</returns>
        public MARE GetEntry(int x, int y)
        {
            if (x < 0 || y < 0 || x > 63 || y > 63)
                throw new ArgumentException();

            var index = x + (y * 64);
            var entry = MapAreas[index];
            if (entry is null)
                throw new InvalidOperationException();

            return entry;
        }

        /// <inheritdoc/>
        public byte[] Serialize()
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
                        }

                        if (MapAreaOcean[mapAreaOffsetIndex] != null)
                        {
                            writtenMapAreaSize += MAOE.GetSizeStatic() + offsetChunkHeaderSize;
                        }

                        if (MapAreaHoles[mapAreaOffsetIndex] != null)
                        {
                            writtenMapAreaSize += MAHO.GetSizeStatic() + offsetChunkHeaderSize;
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
                        }

                        if (MapAreaOcean[mapAreaOffsetIndex] != null)
                        {
                            bw.WriteIFFChunk(MapAreaOcean[mapAreaOffsetIndex]);
                        }

                        if (MapAreaHoles[mapAreaOffsetIndex] != null)
                        {
                            bw.WriteIFFChunk(MapAreaHoles[mapAreaOffsetIndex]);
                        }
                    }
                }
            }

            return ms.ToArray();
        }
    }
}
