using Warcraft.NET.Files.ADT.Flags;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks
{
    /// <summary>
    /// MTFX Chunk - Contains the ADT version.
    /// </summary>
    public class MTFX : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MTXF";

        /// <summary>
        /// Gets the texture flags.
        /// </summary>
        public List<MTXFFlags> TextureFlags { get; } = new List<MTXFFlags>();

        public MTFX()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MTFX"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MTFX(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var entryCount = br.BaseStream.Length / 4;

                for (var i = 0; i < entryCount; ++i)
                {
                    TextureFlags.Add((MTXFFlags)br.ReadUInt32());
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
        public byte[] Serialize()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach(MTXFFlags textureFlag in TextureFlags)
                {
                    bw.Write((uint)textureFlag);
                }

                return ms.ToArray();
            }
        }
    }
}