using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.ADT.TerrainTexture.MCMK.Entrys;

namespace Warcraft.NET.Files.ADT.TerrainTexture.MCMK.SubChunks
{
    /// <summary>
    /// MCLY Chunk - Contains definitions for the alpha map layers.
    /// </summary>
    public class MCLY : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCLY";

        /// <summary>
        /// Gets or sets an array of alpha map layers in this MCNK.
        /// </summary>
        public List<MCLYEntry> Layers { get; set; } = new List<MCLYEntry>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MCLY"/> class.
        /// </summary>
        public MCLY()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCLY"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCLY(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                long layerCount = ms.Length / MCLYEntry.GetSize();

                for (var i = 0; i < layerCount; ++i)
                {
                    Layers.Add(new MCLYEntry(br.ReadBytes(MCLYEntry.GetSize())));
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
                foreach (MCLYEntry layer in Layers)
                {
                    bw.Write(layer.Serialize());
                }

                return ms.ToArray();
            }
        }
    }
}