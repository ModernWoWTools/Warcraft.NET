using System;

namespace Warcraft.NET.Files.Phys.Structures
{
    public struct PlytHeader
    {
        /// <summary>
        /// gets or sets the amount of vertices for this polytope
        /// </summary>
        public uint VertexCount;

        /// <summary>
        /// gets or sets unknown data
        /// </summary>
        public byte[] Unk0;

        /// <summary>
        /// gets or sets certain runtime data. Unknown purpose
        /// </summary>
        public UInt64 RUNTIME08ptrData0;

        /// <summary>
        /// gets or sets the count10, which defines how many Unk1[] and Unk2[] there are inside the Data.
        /// </summary>
        public uint Count10;

        /// <summary>
        /// gets or sets the Unk1 array. Unknown purpose
        /// </summary>
        public byte[] Unk1;

        /// <summary>
        /// gets or sets certain runtime data. Unknown purpose
        /// </summary>
        public UInt64 RUNTIME18ptrData1;

        /// <summary>
        /// gets or sets certain runtime data. Unknown purpose
        /// </summary>
        public UInt64 RUNTIME20ptrData2;

        /// <summary>
        /// gets or sets the amount of nodes. Defines how Vertices are connected
        /// </summary>
        public uint NodeCount;

        /// <summary>
        /// gets or sets the Unk2 array. Unknown purpose
        /// </summary>
        public byte[] Unk2;

        /// <summary>
        /// gets or sets certain runtime data. Unknown purpose
        /// </summary>
        public UInt64 RUNTIME30ptrData3;

        /// <summary>
        /// gets or sets the Unk3 array. Unknown purpose
        /// </summary>
        public float[] Unk3;
    }
}