using Warcraft.NET.Files.Interfaces;
using System.IO;

namespace Warcraft.NET.Files.ADT.Chunks.Cata
{
    /// <summary>
    /// MAMP Chunk
    /// </summary>
    public class MAMP : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Signature.
        /// </summary>
        public const string Signature = "MAMP";

        /// <summary>
        /// Fred
        /// </summary>
        public char[] Fred = new char[4] { (char)0, (char)0, (char)0, (char)0 };

        /// <summary>
        /// Initializes a new instance of the <see cref="MAMP"/> class.
        /// </summary>
        public MAMP()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MAMP"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MAMP(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                Fred = br.ReadChars(4);
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
                bw.Write(Fred);
                return ms.ToArray();
            }
        }
    }
}