using System;

namespace Warcraft.NET.Files.Phys.Structures
{

    [Serializable]
    public struct PlytNode
    {
        /// <summary>
        /// Unknown byte. Always 1 or -1
        /// </summary>
        public byte Unk;                              // 1 or -1

        /// <summary>
        /// Index into the data.Vertices
        /// </summary>
        public byte VertexIndex;                      // index in vertex list

        /// <summary>
        /// Index into data.Nodes
        /// </summary>
        public byte UnkIndex0;                        // index into the nodes

        /// <summary>
        /// Index into data.Nodes
        /// </summary>
        public byte UnkIndex1;                        // index into the nodes
    }
}