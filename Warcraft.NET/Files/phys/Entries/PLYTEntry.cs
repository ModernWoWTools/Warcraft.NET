using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys.Entries
{
    public class PLYTEntry
    {
        public PLYT_HEADER header;
        public PLYT_DATA data;

        [Serializable]
        public struct PLYT_HEADER
        {
            public UInt32 vertexCount;               // Mostly 8
            public byte[] unk_04;
            public UInt64 RUNTIME_08_ptr_data_0;  // = &data[i].unk_0
            public UInt32 count_10;               // Mostly 6
            public byte[] unk_14;
            public UInt64 RUNTIME_18_ptr_data_1;  // = &data[i].unk_1
            public UInt64 RUNTIME_20_ptr_data_2;  // = &data[i].unk_2
            public UInt32 nodeCount;               // Mostly 24
            public byte[] unk_2C;
            public UInt64 RUNTIME_30_ptr_data_3;  // = &data[i].unk_3
            public float[] unk_38;                 // not sure if floats: has e-08 values
        }

        [Serializable]
        public struct PLYT_DATA
        {
            public C3Vector[] vertices; //amount = vertexCount in header
            public byte[] unk1; //multiple of 16 -> amount = count_10 in header
            public byte[] unk2; //amount = count_10 in header
            public NODE[] nodes;//amount = node_count in header
        }

        [Serializable]
        public struct NODE
        {
            public byte unk;                              // 1 or -1
            public byte vertexIndex;                      // index in vertex list
            public byte unkIndex0;                        // index into the nodes
            public byte unkIndex1;                        // index into the nodes
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PLYTEntry"/> class.
        /// </summary>
        public PLYTEntry() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PLYTEntry"/> class.
        /// </summary>
        /// <param name="data">ExtendedData.</param>
        public PLYTEntry(byte[] data)
        {

        }

        /// <summary>
        /// Gets the size of a PLYT entry.
        /// </summary>
        /// <returns>The size.</returns>
        public static int GetSize()
        {
            return 0;
        }

        /// <inheritdoc/>
        public byte[] SerializeHeader(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    
                    bw.Write(header.vertexCount);
                    bw.Write(header.unk_04);
                    
                    bw.Write(header.RUNTIME_08_ptr_data_0);

                    bw.Write(header.count_10);
                    bw.Write(header.unk_14);

                    bw.Write(header.RUNTIME_18_ptr_data_1);

                    bw.Write(header.RUNTIME_20_ptr_data_2);

                    bw.Write(header.nodeCount);
                    
                    bw.Write(header.unk_2C);
                    
                    bw.Write(header.RUNTIME_30_ptr_data_3);
                    foreach (float f in header.unk_38)
                    {
                        bw.Write(f);
                    }
                    
                    
                }
                return ms.ToArray();
            }
        }
        public byte[] SerializeData(long offset = 0)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    foreach (var vertex in data.vertices)
                    {
                        bw.Write(vertex.asBytes());
                    }
                    bw.Write(data.unk1);
                    bw.Write(data.unk2);
                     foreach (var node in data.nodes)
                    {
                        bw.Write(node.unk);
                        bw.Write(node.vertexIndex);
                        bw.Write(node.unkIndex0);
                        bw.Write(node.unkIndex1);
                    }
                }
                return ms.ToArray();
            }
        }


    }
}
