using System;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.ADT.Terrain.MCMK.Flags;
using Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Entrys;
using Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Flags;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MCMK.SubChunks
{
    /// <summary>
    /// MCAL Chunk - Contains alpha map data in one of three forms - uncompressed 2048, uncompressed 4096 and compressed.
    /// </summary>
    public class MCAL : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCAL";

        /// <summary>
        /// Holds unformatted data contained in the chunk.
        /// </summary>
        private byte[] Data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MCAL"/> class.
        /// </summary>
        public MCAL()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCAL"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCAL(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            Data = inData;
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
            return Data;
        }

        public byte[] GetAlphaMapForLayer(MCLYEntry mclyEntry)
        {
            if (Data != null && mclyEntry.Flags.HasFlag(MCLYFlags.UseAlpha))
            {
                byte[] alphaBuffer = (new List<byte>(Data)).GetRange((int)mclyEntry.AlphaMapOffset, Data.Length - (int)mclyEntry.AlphaMapOffset).ToArray();

                if (mclyEntry.Flags.HasFlag(MCLYFlags.CompressedAlpha))
                {
                    return ReadCompressedAlpha(alphaBuffer);
                }
                else
                {
                    return ReadUncompressedAlpha(alphaBuffer);
                }
            }

            byte[] alphaMap = new byte[64 * 64];
            Array.Fill(alphaMap, (byte)0);
            return alphaMap;
        }

        private byte[] ReadCompressedAlpha(byte[] alphaBuffer)
        {
            byte[] alphaMap = new byte[64 * 64];
            Array.Fill(alphaMap, (byte)0);

            int offInner = 0;
            int offOuter = 0;

            while (offOuter < 4096)
            {
                bool fill = (alphaBuffer[offInner] & 0x80 /*128*/) != 0;
                int num = (alphaBuffer[offInner] & 0x7F /*127*/);
                ++offInner;

                for (int k = 0; k < num; ++k)
                {
                    if (offOuter == 4096)
                        break;

                    alphaBuffer[offOuter] = alphaBuffer[offInner];
                    ++offOuter;

                    if (!fill)
                    {
                        ++offInner;
                    }
                }

                if (fill)
                {
                    ++offInner;
                }
            }

            return alphaMap;
        }

        private byte[] ReadUncompressedAlpha(byte[] alphaBuffer)
        {
            byte[] alphaMap = new byte[64 * 64];
            Array.Fill(alphaMap, (byte)0);

            int inner = 0;
            int outer = 0;
            for (int j = 0; j < 64; ++j)
            {
                for (int i = 0; i < 32; ++i)
                {
                    alphaMap[inner] = (byte)((255 * ((int)(alphaBuffer[outer] & 0x0f))) / 0x0f);
                    inner++;

                    if (i != 31)
                    {
                        alphaMap[inner] = (byte)((255 * ((int)(alphaBuffer[outer] & 0xf0))) / 0xf0);
                        inner++;
                    }
                    else
                    {
                        alphaMap[inner] = (byte)((255 * ((int)(alphaBuffer[outer] & 0x0f))) / 0x0f);
                        inner++;
                    }

                    outer++;
                }
            }

            Array.Copy(alphaMap, 62 * 64, alphaMap, 63 * 64, 64);

            return alphaMap;
        }
    }
}