using System;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Files.phys.Chunks;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.phys
{
    [AutoDocFile("skin")]
    public class Skin 
    {

        public UInt32 magic;

        public M2Array vertices;
        public M2Array indices;
        public M2Array bones;
        public M2Array submeshes;
        public M2Array batches;
        public UInt32 globalVertexOffset;
        public M2Array shadow_batches;

        public UInt32 unk0;
        public UInt32 unk1;



        public Skin(byte[] inData) 
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                magic = br.ReadUInt32();
                vertices = new M2Array(br.ReadUInt32(),br.ReadUInt32());
                indices = new M2Array(br.ReadUInt32(), br.ReadUInt32());
                bones = new M2Array(br.ReadUInt32(), br.ReadUInt32());
                submeshes = new M2Array(br.ReadUInt32(), br.ReadUInt32());
                batches = new M2Array(br.ReadUInt32(), br.ReadUInt32());
                globalVertexOffset = br.ReadUInt32();
                shadow_batches = new M2Array(br.ReadUInt32(), br.ReadUInt32());
                unk0 = br.ReadUInt32();
                unk1 = br.ReadUInt32();
            }
        }

        byte[] Serialize()
        {

        }
    }

}
