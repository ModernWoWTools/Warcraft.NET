using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.WDL.Chunks;

namespace Warcraft.NET.Files.WDL.Wotlk
{
    public class WorldDataLod : WorldDataLodBase
    {
        /// <summary>
        /// Contains a list of all referenced WMOs
        /// </summary>
        [ChunkOrder(2)]
        public ADT.Chunks.MWMO LevelWorldObjectNames { get; set; } = new();

        /// <summary>
        /// Contains a list of indexes for the WMO names
        /// </summary>
        [ChunkOrder(3)]
        public ADT.Chunks.MWID LevelWorldObjectIndex { get; set; } = new();

        /// <summary>
        /// Contains position information for all WMO models
        /// </summary>
        [ChunkOrder(3)]
        public ADT.Chunks.MODF LevelWorldObjectDetail { get; set; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldDataLod"/> class.
        /// </summary>
        public WorldDataLod() : base()
        {
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
            LevelWorldObjectNames = br.ReadIFFChunk<ADT.Chunks.MWMO>(false, false);
            LevelWorldObjectIndex = br.ReadIFFChunk<ADT.Chunks.MWID>(false, false);
            LevelWorldObjectDetail = br.ReadIFFChunk<ADT.Chunks.MODF>(false, false);
            MapAreaOffsets = br.ReadIFFChunk<MAOF>(false, true);

            // Set up the two area lists with default values
            for (var i = 0; i < 4096; ++i)
            {
                MapAreas.Add(null);
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
                bw.WriteIFFChunk(LevelWorldObjectNames);
                bw.WriteIFFChunk(LevelWorldObjectIndex);
                bw.WriteIFFChunk(LevelWorldObjectDetail);

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
