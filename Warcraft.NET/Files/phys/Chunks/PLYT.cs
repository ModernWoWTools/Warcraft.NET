using System;
using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.phys.Entries;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Chunks
{
    [AutoDocChunk(AutoDocChunkVersionHelper.VersionAfterBfA, AutoDocChunkVersionHelper.VersionBeforeSL)]
    public class PLYT : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "PLYT";

        public List<PLYTEntry> PLYTEntries = new();

        /// <summary>
        /// Initializes a new instance of <see cref="PLYT"/>
        /// </summary>
        public PLYT() { }

        /// <summary>
        /// Initializes a new instance of <see cref="PLYT"/>
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public PLYT(byte[] inData) => LoadBinaryData(inData);

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

                var PLYTcount = br.ReadUInt32();

                for (var i = 0; i < PLYTcount; ++i)
                {
                    PLYTEntry plyt_entry = new PLYTEntry
                    {
                        header = new PLYTEntry.PLYT_HEADER
                        {
                            vertexCount = br.ReadUInt32(),
                            unk_04 = br.ReadBytes(4),
                            RUNTIME_08_ptr_data_0 = br.ReadUInt64(),
                            count_10 = br.ReadUInt32(),
                            unk_14 = br.ReadBytes(4),
                            RUNTIME_18_ptr_data_1 = br.ReadUInt64(),
                            RUNTIME_20_ptr_data_2 = br.ReadUInt64(),
                            nodeCount = br.ReadUInt32(),
                            unk_2C = br.ReadBytes(4),
                            RUNTIME_30_ptr_data_3 = br.ReadUInt64(), 
                            unk_38 = new float[6] { br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle() } 
                        }
                    };
                    
                    PLYTEntries.Add(plyt_entry);
                }
                for (var j = 0; j < PLYTcount; j++)
                {
                    PLYTEntry entry = PLYTEntries[j];
                    uint vcount = entry.header.vertexCount;
                    uint count_10 = entry.header.count_10;
                    uint nodeCount = entry.header.nodeCount;

                    entry.data.vertices = new C3Vector[vcount];
                    for (int v = 0; v < vcount; v++)
                    {
                        entry.data.vertices[v] = new C3Vector(br.ReadBytes(12));
                    }
                    entry.data.unk1 = br.ReadBytes((int)count_10 * 16);
                    entry.data.unk2 = br.ReadBytes((int)count_10);
                    entry.data.nodes = new PLYTEntry.NODE[nodeCount];
                    for (int n = 0; n < nodeCount; n++)
                    {
                        PLYTEntry.NODE node = new PLYTEntry.NODE();
                        node.unk = br.ReadByte();
                        node.vertexIndex = br.ReadByte();
                        node.unkIndex0 = br.ReadByte();
                        node.unkIndex1 = br.ReadByte();
                        entry.data.nodes[n] = node;
                    }
                }
            }
        }

        /// <inheritdoc/>
        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write((UInt32)PLYTEntries.Count);
                foreach (PLYTEntry obj in PLYTEntries)
                {
                    Console.WriteLine("Writing Plyt Header Length: "+ obj.SerializeHeader().Length);
                    bw.Write(obj.SerializeHeader());
                }
                foreach (PLYTEntry obj in PLYTEntries)
                {
                    bw.Write(obj.SerializeData());
                    Console.WriteLine("Writing Plyt Data Length:" + obj.SerializeData().Length);
                }
                return ms.ToArray();
            }


        }
    }
}
