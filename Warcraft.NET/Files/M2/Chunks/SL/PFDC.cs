using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;
using Warcraft.NET.Files.phys;

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
        /// Gets or sets the Physics of the chunk
        /// </summary>
        public Physics physics { get; set; } = null;

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
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                physics = new Physics(inData);
                //convert inData into >physics<
            }
        }

         /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(physics.Serialize());
                //convert >physics< into byte[]
                return ms.ToArray();
            }
        }
    }
}