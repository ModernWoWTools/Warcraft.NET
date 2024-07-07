using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MapChunk.SubChunks
{
    /// <summary>
    /// MCMT chunk - Terrain material record id.
    /// </summary>
    public class MCMT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCMT";

        /// <summary>
        /// Gets or sets an array of terrain material ids.
        /// </summary>
        public byte[] TerrainMaterialIds;

        /// <summary>
        /// Initializes a new instance of the <see cref="MCMT"/> class.
        /// </summary>
        public MCMT()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCMT"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCMT(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                TerrainMaterialIds = new byte[ms.Length];

                for (var i = 0; i < ms.Length; ++i)
                {
                    TerrainMaterialIds[i] = br.ReadByte();
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
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach(byte terrainMatrialId in TerrainMaterialIds)
                {
                    bw.Write(terrainMatrialId);
                }

                return ms.ToArray();
            }
        }
    }
}