using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Chunks.Legion
{
    /// <summary>
    /// MLVH Chunk - Level of detail heightmap
    /// </summary>
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterLK, AutoDocChunkVersion.LK)]
    public class MLVH : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MLVH";

        /// <summary>
        /// Height data.
        /// </summary>
        public float[] HeightData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLVH"/> class.
        /// </summary>
        public MLVH()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MLVH"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MLVH(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var heightCount = br.BaseStream.Length / sizeof(float);

                HeightData = new float[heightCount];

                for (var i = 0; i < heightCount; i++)
                {
                    HeightData[i] = br.ReadSingle();
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
                foreach (var height in HeightData)
                {
                    bw.Write(height);
                }
                return ms.ToArray();
            }
        }
    }
}