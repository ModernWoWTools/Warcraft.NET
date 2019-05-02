using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.WMO.Chunks
{
    /// <summary>
    /// MOTX Chunk - Contains pathes to used textures
    /// </summary>
    public class MOTX : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MOTX";

        /// <summary>
        /// Gets a dictionary of the trxture offsets mapped to texture file pathes.
        /// </summary>
        public Dictionary<long, string> Textures { get; } = new Dictionary<long, string>();

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
                        Textures.Add(ms.Position, br.ReadNullTerminatedString());
                    else
                        ms.Position += 4 - (ms.Position % 4);
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var texture in Textures)
                {
                    if (ms.Position % 4 == 0)
                        bw.WriteNullTerminatedString(texture.Value);
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
