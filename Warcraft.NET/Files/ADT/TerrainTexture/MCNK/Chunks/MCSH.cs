using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Entrys;
using System.Collections;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Chunks
{
    /// <summary>
    /// MCSH chunk - holds baked terrain shadows.
    /// </summary>
    public class MCSH : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCSH";

        /// <summary>
        /// Gets or sets an array of alpha map layers in this MCNK.
        /// </summary>
        public byte[] ShadowMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="MCSH"/> class.
        /// </summary>
        public MCSH()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCSH"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCSH(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                ShadowMap = new byte[ms.Length];
                for (ushort i = 0; i < ms.Length; i++)
                {
                    ShadowMap[i] = br.ReadByte();
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
                foreach(byte shadow in ShadowMap)
                {
                    bw.Write(shadow);
                }

                return ms.ToArray();
            }
        }
    }
}