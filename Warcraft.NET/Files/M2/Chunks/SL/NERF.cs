using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class NERF : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "NERF";

        /// <summary>
        /// Gets or sets the NERF Entries
        /// </summary>
        public List<Vector2> NERFEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="NERF"/>
        /// </summary>
        public NERF() { }

        /// <summary>
        /// Initializes a new instance of <see cref="NERF"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public NERF(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc />
        public void LoadBinaryData(byte[] inData)
        {
            {
                using (var ms = new MemoryStream(inData))
                using (var br = new BinaryReader(ms))
                {
                    var NERFcount = br.BaseStream.Length / 8;
                    for (var i = 0; i < NERFcount; ++i)
                    {
                        NERFEntries.Add(new Vector2(br.ReadSingle(), br.ReadSingle()));
                    }
                }
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (Vector2 obj in NERFEntries)
                {
                    bw.Write(obj.X);
                    bw.Write(obj.Y);
                }
                return ms.ToArray();
            }
        }
    }
}