using System.Collections.Generic;
using System.IO;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.SKIN
{
    [AutoDocFile("skin")]
    public class Skin
    {
        public List<ushort> Vertices { get; set; } //index into M2s vertex list with offset <globalVertexOffset>
        public List<M2Triangle> Triangles { get; set; } //3 indices per entry into vertices
        public List<M2SkinBoneStruct> BoneIndices { get; set; }
        public List<M2SkinSection> Submeshes { get; set; }
        public List<M2Batch> TextureUnits { get; set; }

        public uint GlobalVertexOffset; //start offset into M2.Vertices -> something else in wotlk

        public List<M2ShadowBatch> ShadowBatches;

        public byte[] Unk0; //Padding?

        public bool Wotlk = false;

        public Skin(byte[] inData)
        {
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                var magic = br.ReadUInt32();
                var nVertices = br.ReadUInt32();
                var ofsVertices = br.ReadUInt32();
                var nIndices = br.ReadUInt32();
                var ofsIndices = br.ReadUInt32();
                if (ofsVertices == 48)
                {
                    Wotlk = true;
                }
                var nBones = br.ReadUInt32();
                var ofsBones = br.ReadUInt32();
                var nSubmeshes = br.ReadUInt32();
                var ofsSubmeshes = br.ReadUInt32();
                var nBatches = br.ReadUInt32();
                var ofsBatches = br.ReadUInt32();
                GlobalVertexOffset = br.ReadUInt32();

                ShadowBatches = new List<M2ShadowBatch>();
                Unk0 = new byte[8];
                if (!Wotlk)
                {
                    var nShadow_batches = br.ReadUInt32();
                    var ofsShadow_batches = br.ReadUInt32();
                    Unk0 = br.ReadBytes(8);
                    ShadowBatches = ReadStructList<M2ShadowBatch>(nShadow_batches, ofsShadow_batches, br);
                }
                Vertices = ReadStructList<ushort>(nVertices, ofsVertices, br);
                Triangles = ReadStructList<M2Triangle>(nIndices / 3, ofsIndices, br);
                BoneIndices = ReadStructList<M2SkinBoneStruct>(nBones, ofsBones, br);
                Submeshes = ReadStructList<M2SkinSection>(nSubmeshes, ofsSubmeshes, br);
                TextureUnits = ReadStructList<M2Batch>(nBatches, ofsBatches, br);

            }
        }
        private List<T> ReadStructList<T>(uint count, uint offset, BinaryReader br) where T : struct
        {
            br.BaseStream.Position = offset;
            List<T> list = new List<T>();

            for (var i = 0; i < count; i++)
                list.Add(br.ReadStruct<T>());

            return list;
        }

        public byte[] Serialize(long offset = 0)
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                //Writing Empty Header -> Filling with data after writing the data
                if (Wotlk)
                {
                    bw.Write(new byte[48]);
                }
                else
                {
                    bw.Write(new byte[64]);
                }
                
                foreach (ushort vertex in Vertices)
                {
                    bw.Write(vertex);
                }

                int _ofsTriangles = (int)bw.BaseStream.Position;
                foreach (M2Triangle triangle in Triangles)
                {
                    bw.WriteStruct(triangle);
                }

                int _ofsBones = (int)bw.BaseStream.Position;
                foreach (M2SkinBoneStruct bone in BoneIndices)
                {
                    bw.WriteStruct(bone);
                }

                int _ofsSubmeshes = (int)bw.BaseStream.Position;
                foreach (M2SkinSection submesh in Submeshes)
                {
                    bw.WriteStruct(submesh);
                }

                int _ofsTexUnits = (int)bw.BaseStream.Position;
                foreach (M2Batch texUnit in TextureUnits)
                {
                    bw.WriteStruct(texUnit);
                }

                int _ofsShadowBatches = (int)bw.BaseStream.Position;
                if (!Wotlk)
                {
                    foreach (M2ShadowBatch shadowBatch in ShadowBatches)
                    {
                        bw.WriteStruct(shadowBatch);
                    }
                }
                //Writing actual header data
                bw.BaseStream.Position = 0;
                bw.Write('S');
                bw.Write('K');
                bw.Write('I');
                bw.Write('N');

                bw.Write(Vertices.Count);
                var _ofsVertices = 64;
                if (Wotlk)
                    _ofsVertices = 48;
                bw.Write(_ofsVertices);

                bw.Write(Triangles.Count * 3);
                bw.Write(_ofsTriangles);

                bw.Write(BoneIndices.Count);
                bw.Write(_ofsBones);

                bw.Write(Submeshes.Count);
                bw.Write(_ofsSubmeshes);

                bw.Write(TextureUnits.Count);
                bw.Write(_ofsTexUnits);

                bw.Write(GlobalVertexOffset);

                if (!Wotlk)
                {
                    bw.Write(ShadowBatches.Count);
                    bw.Write(_ofsShadowBatches);
                    bw.Write(Unk0);
                }
                return ms.ToArray();
            }
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }
    }
}


