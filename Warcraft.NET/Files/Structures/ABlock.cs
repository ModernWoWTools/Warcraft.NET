using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Warcraft.NET.Extensions;

namespace Warcraft.NET.Files.Structures
{
    public struct ABlock<T> where T : struct
    {
        public ushort GlobalSequence;
        public ushort InterpolationType;
        public ArrayReference<ArrayReference<uint>> Timestamps;
        public ArrayReference<ArrayReference<T>> Values;
    }

    public struct ArrayReference<T> where T : struct
    {
        public uint Number;
        private uint elementsOffset;

        public IEnumerable<T> GetElements(BinaryReader br)
        {
            var type = typeof(T);
            for (int i = 0; i < Number; i++)
            {
                var offset = elementsOffset + (i * Marshal.SizeOf(type));
                br.BaseStream.Position += offset;
                yield return (T)br.ReadStruct<T>();
            }
        }
    }
}
