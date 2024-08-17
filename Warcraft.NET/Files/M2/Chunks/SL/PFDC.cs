using System;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Phys;

namespace Warcraft.NET.Files.M2.Chunks.SL
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class PFDC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PFDC";

        /// <summary>
        /// Gets or sets the Physics for the M2
        /// </summary>
        public Physics Physics { get; set; } = null;

        /// <summary>
        /// Initializes a new instance of <see cref="PFDC"/>
        /// </summary>
        public PFDC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PFDC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PFDC(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            Physics = new Physics(inData);
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            return PadTo8Bytes(Physics.Serialize());
        }

        static byte[] PadTo8Bytes(byte[] input)
        {
            int paddingNeeded = 8 - (input.Length % 8);
            if (paddingNeeded == 8)
                paddingNeeded = 0;
            byte[] paddedArray = new byte[input.Length + paddingNeeded];
            Array.Copy(input, paddedArray, input.Length);
            for (int i = input.Length; i < paddedArray.Length; i++)
            {
                paddedArray[i] = 0x00;
            }
            return paddedArray;
        }
    }
}