using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WMO.Chunks
{
    /// <summary>
    /// MOTX Chunk - Contains pathes to used textures
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionBeforeBfA, AutoDocChunkVersionHelper.VersionAfterLegion)]
    public class MOTX : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MOTX";

        /// <summary>
        /// Gets a dictionary of the texture offsets mapped to texture file pathes.
        /// </summary>
        public Dictionary<long, string> Textures { get; } = new();

        /// <summary>
        /// Next texture offset
        /// </summary>
        protected long NextOffset = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="MOTX"/> class.
        /// </summary>
        public MOTX()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MOTX"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MOTX(byte[] inData)
        {
            LoadBinaryData(inData);
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
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                while (ms.Position < ms.Length)
                {
                    if (ms.Position % 4 == 0)
                    {
                        long texPos = ms.Position;
                        string texPath = br.ReadNullTerminatedString();

                        if (texPath.Trim().Length > 0)
                            Textures.Add(texPos, texPath);
                    }
                    else
                    {
                        ms.Position += 4 - (ms.Position % 4);
                    }
                }
            }
        }

        public long Add(string texture)
        {
            // Return stored texture offset
            if (Textures.ContainsValue(texture))
                return Textures.FirstOrDefault(t => t.Value == texture).Key;

            // Set texture offset
            long textureOffset = NextOffset;
            Textures.Add(NextOffset, texture);

            // Calc next offset
            NextOffset += Encoding.ASCII.GetBytes(texture).LongLength + 1;
            if (NextOffset % 4 != 0)
                NextOffset += 4 - (NextOffset % 4);

            // Return texture offset
            return textureOffset;
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                // padding
                for (var i = 0; i < 4; i++)
                    bw.Write('\0');

                foreach (var texture in Textures)
                {
                    if (ms.Position % 4 == 0)
                    {
                        bw.WriteNullTerminatedString(texture.Value);
                    }
                    else
                    {
                        var stringPadCount = 4 - (ms.Position % 4);
                        for (var i = 0; i < stringPadCount; i++)
                            bw.Write('\0');

                        bw.WriteNullTerminatedString(texture.Value);
                    }
                }

                // Insert padding until next alignment
                var padCount = 4 - (ms.Position % 4);
                for (var i = 0; i < padCount; i++)
                    bw.Write('\0');

                return ms.ToArray();
            }
        }
    }
}
