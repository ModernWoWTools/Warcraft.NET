using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    public class EXP2 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "EXP2";

        /// <summary>
        /// Gets or sets the EXP2 Data
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// Initializes a new instance of <see cref="EXP2"/>
        /// </summary>
        public EXP2() { }

        /// <summary>
        /// Initializes a new instance of <see cref="EXP2"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public EXP2(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            Data = inData;
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            return Data;
        }
    }
}