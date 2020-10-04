using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.ADT.Terrain.MCMK;

namespace Warcraft.NET.Files.ADT.Terrain.BfA
{
    /// <summary>
    /// MCNK - BfA MCNK chunk
    /// </summary>
    public class MCNK : MCNKBase
    {
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

                // Write MCLV
                if (VertexLighting != null)
                {
                    newHeader.VertexLightingOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexLighting);
                }

                // Write MCNR
                if (VertexNormals != null)
                {
                    newHeader.VertexNormalOffset = (uint)ms.Position + headerAndSizeOffset;
                    bw.WriteIFFChunk(VertexNormals);
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