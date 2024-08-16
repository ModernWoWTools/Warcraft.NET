using System;
using System.Collections.Generic;
using System.Text;

namespace Warcraft.NET.Files.Structures
{
    public struct M2Triangle //132->Left handed; 123->right handed
    {
        public ushort Vertex1;
        public ushort Vertex3;
        public ushort Vertex2;
    }
}
