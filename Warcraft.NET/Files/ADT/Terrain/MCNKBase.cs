using Warcraft.NET.Files.Interfaces;
using System.IO;
using Warcraft.NET.Files.ADT.Terrain.MCMK;
using Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.ADT.Terrain
{
    /// <summary>
    /// MCNK
    /// </summary>
    public abstract class MCNKBase : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCNK";

        /// <summary>
        /// Gets or sets the header, which contains information about the MCNK and its subchunks such as offsets,
        /// position and flags.
        /// </summary>
        public Header Header { get; set; } = new Header();

        /// <summary>
        /// Gets or sets the heightmap chunk.
        /// </summary>
        public MCVT Heightmap { get; set; }

        /// <summary>
        /// Gets or sets the normal map chunk.
        /// </summary>
        public MCNR VertexNormals { get; set; }

        /// <summary>
        /// Gets or sets the vertex shading chunk.
        /// </summary>
        public MCCV VertexShading { get; set; }

        /// <summary>
        /// Gets or sets the vertex lighting chunk.
        /// </summary>
        public MCLV VertexLighting { get; set; }

        /// <summary>
        /// Gets or sets the sound emitters chunk.
        /// </summary>
        public MCSE SoundEmitters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCNKBase"/> class.
        /// </summary>
        public MCNKBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCNKBase"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCNKBase(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public virtual void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                long headerAndSizeOffset = -8;
                Header = new Header(br.ReadBytes(Header.GetSize()));

                // Read MCVT
                if (Header.HeightmapOffset > 0)
                {
                    ms.Seek(Header.HeightmapOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    Heightmap = br.ReadIFFChunk<MCVT>(false, false);
                }

                // Read MCNR
                if (Header.VertexNormalOffset > 0)
                {
                    ms.Seek(Header.VertexNormalOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    VertexNormals = br.ReadIFFChunk<MCNR>(false, false);
                }

                // Read MCCV
                if (Header.VertexShadingOffset > 0)
                {
                    ms.Seek(Header.VertexShadingOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    VertexShading = br.ReadIFFChunk<MCCV>(false, false);
                }

                // Read MCLV
                if (Header.VertexLightingOffset > 0)
                {
                    ms.Seek(Header.VertexShadingOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    VertexLighting = br.ReadIFFChunk<MCLV>(false, false);
                }

                // Read MCSE
                if (Header.SoundEmittersOffset > 0)
                {
                    ms.Seek(Header.SoundEmittersOffset + headerAndSizeOffset, SeekOrigin.Begin);
                    SoundEmitters = br.ReadIFFChunk<MCSE>(false, false);
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
            return (uint)Serialize().Length;
        }

        /// <inheritdoc/>
        public abstract byte[] Serialize(long offset = 0);
    }
}