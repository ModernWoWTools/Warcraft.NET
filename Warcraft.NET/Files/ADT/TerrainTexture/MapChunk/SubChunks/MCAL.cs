using System;
using System.Collections.Generic;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MapChunk.SubChunks
{
    /// <summary>
    /// MCAL Chunk - Contains alpha map data in one of three forms - uncompressed 2048, uncompressed 4096 and compressed.
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAll)]
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

        public byte[] GetAlphaMapForLayer(Entries.MCLYEntry mclyEntry, bool bigAlpha = false)
        {
            if (Data != null && mclyEntry.Flags.HasFlag(Flags.MCLYFlags.UseAlpha))
            {
                byte[] alphaBuffer = (new List<byte>(Data)).GetRange((int)mclyEntry.AlphaMapOffset, Data.Length - (int)mclyEntry.AlphaMapOffset).ToArray();

                if (mclyEntry.Flags.HasFlag(Flags.MCLYFlags.CompressedAlpha))
                {
                    return ReadCompressedAlpha(alphaBuffer);
                }
                else if (bigAlpha)
                {
                    return ReadBigAlpha(alphaBuffer);
                }
                else
                {
                    return ReadUncompressedAlpha(alphaBuffer);
                }
            }

            byte[] alphaMap = new byte[64 * 64];
            alphaMap.Fill((byte)0);
            return alphaMap;
        }

        private byte[] ReadCompressedAlpha(byte[] alphaBuffer)
        {
            byte[] alphaMap = new byte[64 * 64];
            alphaMap.Fill((byte)0);

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

        private byte[] ReadBigAlpha(byte[] alphaBuffer)
        {
            byte[] alphaMap = new byte[64 * 64];
            int a = 0;
            for (int j = 0; j < 64; ++j)
            {
                for (int i = 0; i < 64; ++i)
                {
                    alphaMap[a] = alphaBuffer[a];
                    a++;
                }
            }
            Array.Copy(alphaMap, 62 * 64, alphaMap, 63 * 64, 64);

            return alphaMap;
        }

        private byte[] ReadUncompressedAlpha(byte[] alphaBuffer)
        {
            byte[] alphaMap = new byte[64 * 64];
            alphaMap.Fill((byte)0);

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