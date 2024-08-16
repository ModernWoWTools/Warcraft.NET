using System;
using System.Collections.Generic;
using System.Text;

namespace Warcraft.NET.Files.Structures
{
    public struct M2ShadowBatch
    {
        public byte Flags;              // if auto-generated: M2Batch.Flags & 0xFF
        public byte Flags2;             // if auto-generated: (renderFlag[i].Flags & 0x04 ? 0x01 : 0x00)
                                        //                  | (!renderFlag[i].Blendingmode ? 0x02 : 0x00)
                                        //                  | (renderFlag[i].Flags & 0x80 ? 0x04 : 0x00)
                                        //                  | (renderFlag[i].Flags & 0x400 ? 0x06 : 0x00)
        public ushort Unk1;
        public ushort SubmeshId;
        public ushort TextureId;        // already looked-up
        public ushort ColorId;
        public ushort TransparencyId;   // already looked-up
    }
}
