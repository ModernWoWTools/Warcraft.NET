using System.IO;
using System.Numerics;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Phys.Structures;

namespace Warcraft.NET.Files.Phys.Entries
{
    public class PLYTEntry
    {
        /// <summary>
        /// gets or set the Polytope Header
        /// </summary>
        public PlytHeader Header;

        /// <summary>
        /// gets or set the Polytope Data
        /// </summary>
        public PlytData Data;

        /// <summary>
        /// gets or set the Polytope Data
        /// </summary>
        public int DataSize { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PLYTEntry"/> class.
        /// </summary>
        public PLYTEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PLYTEntry"/> class.
        /// </summary>
        /// <param name="Data">Header Data</param>
        public PLYTEntry(byte[] header)
        {
            using (var ms = new MemoryStream(header))
            using (var br = new BinaryReader(ms))
            {
                Header = new PlytHeader
                {
                    VertexCount = br.ReadUInt32(),
                    Unk0 = br.ReadBytes(4),
                    RUNTIME08ptrData0 = br.ReadUInt64(),
                    Count10 = br.ReadUInt32(),
                    Unk1 = br.ReadBytes(4),
                    RUNTIME18ptrData1 = br.ReadUInt64(),
                    RUNTIME20ptrData2 = br.ReadUInt64(),
                    NodeCount = br.ReadUInt32(),
                    Unk2 = br.ReadBytes(4),
                    RUNTIME30ptrData3 = br.ReadUInt64(),
                    Unk3 = [br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle()]
                };
                int size = 0;
                size += (int)Header.VertexCount * 12;
                size += (int)Header.Count10 * 16;
                size += (int)Header.Count10;
                size += (int)Header.NodeCount * 4;
                DataSize = size;
            }
        }

        /// <summary>
        /// Deserialization of the Data Chunks
        /// </summary>
        /// <param name="data"></param>
        public void DeserializeData(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                uint vcount = Header.VertexCount;
                uint count_10 = Header.Count10;
                uint nodeCount = Header.NodeCount;

                Data.Vertices = new Vector3[vcount];
                for (int v = 0; v < vcount; v++)
                {
                    Data.Vertices[v] = br.ReadVector3();
                }
                Data.Unk1 = br.ReadBytes((int)count_10 * 16);
                Data.Unk2 = br.ReadBytes((int)count_10);
                Data.Nodes = new PlytNode[nodeCount];
                for (int n = 0; n < nodeCount; n++)
                {
                    PlytNode node = new();
                    node.Unk = br.ReadByte();
                    node.VertexIndex = br.ReadByte();
                    node.UnkIndex0 = br.ReadByte();
                    node.UnkIndex1 = br.ReadByte();
                    Data.Nodes[n] = node;
                }
            }
        }

        /// <summary>
        /// gets the data size of the polytope.
        /// </summary>
        /// <returns>The size.</returns>
        public int GetDataSize()
        {
            return SerializeData().Length;
        }

        /// <summary>
        /// gets the header size of the polytope.
        /// </summary>
        /// <returns>The size.</returns>
        public int GetHeaderSize()
        {
            return SerializeHeader().Length;
        }

        /// <summary>
        /// gets the full size of the polytope.
        /// </summary>
        /// <returns>The size.</returns>
        public int GetSize()
        {
            return SerializeHeader().Length + SerializeData().Length;
        }

        /// <summary>
        /// Serialize the Header
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public byte[] SerializeHeader(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(Header.VertexCount);
                    bw.Write(Header.Unk0);

                    bw.Write(Header.RUNTIME08ptrData0);

                    bw.Write(Header.Count10);
                    bw.Write(Header.Unk1);

                    bw.Write(Header.RUNTIME18ptrData1);

                    bw.Write(Header.RUNTIME20ptrData2);

                    bw.Write(Header.NodeCount);

                    bw.Write(Header.Unk2);

                    bw.Write(Header.RUNTIME30ptrData3);
                    foreach (float f in Header.Unk3)
                    {
                        bw.Write(f);
                    }
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Serialize the Data
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public byte[] SerializeData(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    foreach (var vertex in Data.Vertices)
                    {
                        bw.WriteVector3(vertex);
                    }
                    bw.Write(Data.Unk1);
                    bw.Write(Data.Unk2);
                    foreach (var node in Data.Nodes)
                    {
                        bw.Write(node.Unk);
                        bw.Write(node.VertexIndex);
                        bw.Write(node.UnkIndex0);
                        bw.Write(node.UnkIndex1);
                    }
                }
                return ms.ToArray();
            }
        }
    }
}