using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.ADT.Chunks;
using Warcraft.NET.Files.ADT.Chunks.Legion;
using Warcraft.NET.Types;

namespace Warcraft.NET.Files.ADT.TerrainLOD.Legion
{
#nullable enable
    [AutoDocFile("adt", "_lod ADT")]
    public class TerrainLOD : TerrainLODBase
    {
        /// <summary>
        /// LOD header.
        /// </summary>
        [ChunkOrder(2)]
        public MLHD Header { get; set; }

        /// <summary>
        /// LOD heightmap.
        /// </summary>
        [ChunkOrder(3)]
        public MLVH Heightmap { get; set; }

        /// <summary>
        /// LOD levels.
        /// </summary>
        [ChunkOrder(4)]
        public MLLL Levels { get; set; }

        /// <summary>
        /// LOD quad-tree.
        /// </summary>
        [ChunkOrder(5)]
        public MLND QuadTree { get; set; }

        /// <summary>
        /// LOD Vertex Indices.
        /// </summary>
        [ChunkOrder(6)]
        public MLVI VertexIndices { get; set; }

        /// <summary>
        /// LOD Skirt Indices.
        /// </summary>
        [ChunkOrder(7)]
        public MLSI SkirtIndices { get; set; }

        /// <summary>
        /// Blend mesh headers.
        /// </summary>
        [ChunkOrder(8), ChunkOptional]
        public MBMH BlendMeshHeaders { get; set; }

        /// <summary>
        /// Blend mesh bounding boxes.
        /// </summary>
        [ChunkOrder(9), ChunkOptional]
        public MBBB BlendMeshBoundingBoxes { get; set; }

        /// <summary>
        /// Blend mesh indices.
        /// </summary>
        [ChunkOrder(10), ChunkOptional]
        public MBMI BlendMeshIndices { get; set; }

        /// <summary>
        /// Blend mesh vertices.
        /// </summary>
        [ChunkOrder(11), ChunkOptional]
        public MBNV BlendMeshVertices { get; set; }

        /// <summary>
        /// Blend mesh batches.
        /// </summary>
        [ChunkOrder(12), ChunkOptional]
        public MBMB BlendMeshBatches { get; set; }

        /// <summary>
        /// Liquid data.
        /// TODO: Requires additional implementation for decompressing as well as properly reading the last chunk.
        /// </summary>
        [ChunkOrder(13), ChunkOptional]
        public MLLD LiquidData { get; set; }

        /// <summary>
        /// Unknown name. Wiki says "MLLN introduces a new liquid".
        /// </summary>
        public SynchronizedList<MLLN?> LiquidN { get; set; } = new SynchronizedList<MLLN?>(new List<MLLN?>(4096));

        /// <summary>
        /// Liquid indices.
        /// </summary>
        public SynchronizedList<MLLI?> LiquidIndices { get; set; } = new SynchronizedList<MLLI?>(new List<MLLI?>(4096));

        /// <summary>
        /// Liquid veritices.
        /// </summary>
        public SynchronizedList<MLLV?> LiquidVertices { get; set; } = new SynchronizedList<MLLV?>(new List<MLLV?>(4096));

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainLOD"/> class.
        /// </summary>
        public TerrainLOD() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TerrainLOD"/> class.
        /// </summary>
        /// <param name="inData">The binary data.</param>
        public TerrainLOD(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using var ms = new MemoryStream(inData);
            using var br = new BinaryReader(ms);

            Version = br.ReadIFFChunk<MVER>(false, false);
            Header = br.ReadIFFChunk<MLHD>(false, false);
            Heightmap = br.ReadIFFChunk<MLVH>(false, false);
            Levels = br.ReadIFFChunk<MLLL>(false, false);
            QuadTree = br.ReadIFFChunk<MLND>(false, false);
            VertexIndices = br.ReadIFFChunk<MLVI>(false, false);
            SkirtIndices = br.ReadIFFChunk<MLSI>(false, false);

            while (br.BaseStream.Position < br.BaseStream.Length)
            {
                var chunk = br.PeekChunkSignature();

                switch (chunk)
                {
                    case "MBMH":
                        BlendMeshHeaders = br.ReadIFFChunk<MBMH>(false, false);
                        break;
                    case "MBBB":
                        BlendMeshBoundingBoxes = br.ReadIFFChunk<MBBB>(false, false);
                        break;
                    case "MBMI":
                        BlendMeshIndices = br.ReadIFFChunk<MBMI>(false, false);
                        break;
                    case "MBNV":
                        BlendMeshVertices = br.ReadIFFChunk<MBNV>(false, false);
                        break;
                    case "MBMB":
                        BlendMeshBatches = br.ReadIFFChunk<MBMB>(false, false);
                        break;
                    case "MLLD":
                        LiquidData = br.ReadIFFChunk<MLLD>(false, false);
                        break;
                    case "MLLN":
                        LiquidN.Add(br.ReadIFFChunk<MLLN>(false, false));
                        break;
                    case "MLLI":
                        LiquidIndices.Add(br.ReadIFFChunk<MLLI>(false, false));
                        break;
                    case "MLLV":
                        LiquidVertices.Add(br.ReadIFFChunk<MLLV>(false, false));
                        break;
                }
            }
        }
    }
#nullable disable
}
