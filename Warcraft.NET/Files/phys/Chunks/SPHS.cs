using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class SPHS : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "SPHS";

        /// <summary>
        /// Gets or Sets the local position of the Sphere shape
        /// </summary>
        public C3Vector localPosition;

        /// <summary>
        /// Gets or Sets the radius of the Sphere shape
        /// </summary>
        public float radius;

        /// <summary>
        /// Initializes a new instance of <see cref="SPHS"/>
        /// </summary>
        public SPHS() { }

        /// <summary>
        /// Initializes a new instance of <see cref="SPHS"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public SPHS(byte[] inData) => LoadBinaryData(inData);

        /// <inheritdoc />
        public string GetSignature() { return Signature; }

        /// <inheritdoc />
        public uint GetSize() { return (uint)Serialize().Length; }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                localPosition = new C3Vector(br.ReadBytes(12));
                radius = br.ReadSingle();
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {                
                bw.Write(localPosition.asBytes());
                bw.Write(radius);
                return ms.ToArray();
            }
        }
    }
}
