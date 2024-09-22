using System;
using System.Numerics;

namespace Warcraft.NET.Files.Phys.Structures
{
    [Serializable]
    public struct PlytData
    {
        /// <summary>
        /// gets or sets the vertices. Amount is defined by header.VertexCount
        /// </summary>
        public Vector3[] Vertices; //amount = VertexCount in Header

        /// <summary>
        /// gets or sets the Unk1 array. Unknown purpose. Amount is defined by header.Count10
        /// </summary>
        public byte[] Unk1; //multiple of 16 -> amount = count_10 in Header

        /// <summary>
        /// gets or sets the Unk2 array. Unknown purpose.Amount is defined by header.Count10
        /// </summary>
        public byte[] Unk2; //amount = count_10 in Header

        /// <summary>
        /// gets or sets the Unk2 array. Unknown purpose.Amount is defined by header.NodeCount
        /// </summary>
        public PlytNode[] Nodes;
    }
}
