using System;
using System.Collections.Generic;
using System.Text;

namespace Warcraft.NET.Files.Structures
{
    /// <summary>
    /// A structure that represents an M2Array.
    /// </summary>
    public struct M2Array
    {
        /// <summary>
        /// Size of the Block.
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        /// Offset of the Block.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="M2Array"/>.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        public M2Array(uint size, uint offset)
        {
            Size = size;
            Offset = offset;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="M2Array"/>.
        /// </summary>
        /// <param name="array">In array.</param>
        public M2Array(M2Array array) : this(array.Size, array.Offset)
        {

        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Size: {Size}, Offset: {Offset}";
        }
    }
}
