using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.Interfaces;
using System.Collections.Generic;

namespace Warcraft.NET.Files.M2.Chunks.Legion
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterWoD, AutoDocChunkVersionHelper.VersionBeforeLegion)]
    public class TXAC : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "TXAC";

        /// <summary>
        /// 2 byte array
        /// From Wiki: 
        /// likely used in CM2SceneRender::SetupTextureTransforms and uploaded to the shader directly. 0 otherwise.
        /// This chunk doesn't seem to be used directly. Inside CParticleEmitter2 class there are non-null checks that deal with selection of VertexBufferFormat for particles. Apart from that, the usage of these fields is unknown
        /// </summary>
        public List<byte[]> TXACEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="TXAC"/>
        /// </summary>
        public TXAC() { }

        /// <summary>
        /// Initializes a new instance of <see cref="TXAC"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public TXAC(byte[] inData) => LoadBinaryData(inData);

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
                for (int i = 0; i < inData.Length / 2; i++)
                {
                    var entry = new byte[2];
                    entry[0] = br.ReadByte();
                    entry[1] = br.ReadByte();
                    TXACEntries.Add(entry);
                }
            }
        }

        /// <inheritdoc />
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var entry in TXACEntries)
                {
                    bw.Write(entry);
                }
                return ms.ToArray();
            }
        }
    }
}