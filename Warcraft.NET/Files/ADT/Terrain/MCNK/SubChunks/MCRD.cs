using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Files.Interfaces;

namespace Warcraft.NET.Files.ADT.Terrain.MCNK.SubChunks
{
    /// <summary>
    /// MCRF Chunk - Holds model and world object references
    /// </summary>
    public class MCRF : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MCRF";

        /// <summary>
        /// Gets or sets Vertices
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Holds model references
        /// </summary>
        public List<uint> ModelReferences = new();

        /// <summary>
        /// Holds world object references
        /// </summary>
        public List<uint> WorldObjectReferences = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MCRF"/> class.
        /// </summary>
        public MCRF()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCRF"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MCRF(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            Data = inData;
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
            if (ModelReferences.Count > 0 || WorldObjectReferences.Count > 0)
            {
                using (var ms = new MemoryStream())
                using (var bw = new BinaryWriter(ms))
                {
                    foreach (uint model in ModelReferences)
                    {
                        bw.Write(model);
                    }

                    foreach (uint worldObject in WorldObjectReferences)
                    {
                        bw.Write(worldObject);
                    }

                    return ms.ToArray();
                }
            }
         
            return Data;
        }

        /// <summary>
        /// Performs post-creation loading of data reliant on external sources.
        /// </summary>
        /// <param name="modelReferenceCount">The number of game model objects in the chunk.</param>
        /// <param name="worldModelObjectReferenceCount">The number of world model objects in the chunk.</param>
        public void PostLoadReferences(uint modelReferenceCount, uint worldModelObjectReferenceCount)
        {
            using (var ms = new MemoryStream(Data))
            {
                using (var br = new BinaryReader(ms))
                {
                    for (var i = 0; i < modelReferenceCount; ++i)
                    {
                        ModelReferences.Add(br.ReadUInt32());
                    }

                    for (var i = 0; i < worldModelObjectReferenceCount; ++i)
                    {
                        WorldObjectReferences.Add(br.ReadUInt32());
                    }
                }
            }
        }
    }
}
