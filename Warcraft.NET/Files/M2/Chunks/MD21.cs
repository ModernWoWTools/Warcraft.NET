using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Warcraft.NET.Attribute;
using Warcraft.NET.Extensions;
using Warcraft.NET.Files.Interfaces;
using Warcraft.NET.Files.M2.Entries;
using Warcraft.NET.Files.M2.Flags;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.M2.Chunks
{
    /// <summary>
    /// MD21 Chunk - Contains model base information
    /// </summary>
    [AutoDocChunk(0, AutoDocChunkVersionHelper.VersionBeforeLegion, AutoDocChunkVersionHelper.VersionAfterWoD)]
    public class MD21 : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MD21";

        public uint Version { get; set; }
        public string Name { get; set; }
        public MD21Flags Flags { get; set; }
        public uint ViewCount { get; set; }
        public BoundingBox VertexBox { get; set; }
        public float VertexBoxRadius { get; set; }
        public BoundingBox BoundingBox { get; set; }
        public float BoundingBoxRadius { get; set; }
        public List<SequenceStruct> Sequences { get; set; }
        public List<AnimationStruct> Animations { get; set; }
        public List<AnimationLookupStruct> AnimationLookups { get; set; }
        public List<BoneStruct> Bones { get; set; }
        public List<KeyBoneLookupStruct> KeyBoneLookup { get; set; }
        public List<VerticeStruct> Vertices { get; set; }
        public List<ColorStruct> Colors { get; set; }
        public List<TextureStruct> Textures { get; set; }
        public List<TransparencyStruct> Transparency { get; set; }
        public List<UVAnimationStruct> UVAnimations { get; set; }
        public List<TextureReplaceStruct> TextureReplace { get; set; }
        public List<RenderFlagStruct> RenderFlags { get; set; }
        public List<BoneLookupTableStruct> BoneLookupTable { get; set; }
        public List<TextureLookupStruct> TextrueLookup { get; set; }
        public List<TransparencyLookupStruct> TransparencyLookup { get; set; }
        public List<UVAnimLookupStruct> UVAnimLookup { get; set; }
        public List<BoundingTriangleStruct> BoundingTriangles { get; set; }
        public List<BoundingVertexStruct> BoundingVertices { get; set; }
        public List<BoundingNormalStruct> BoundingNormals { get; set; }
        public List<AttachmentStruct> Attachments { get; set; }
        public List<AttachLookupStruct> AttachLookup { get; set; }
        public List<EventStruct> Events { get; set; }
        public List<LightStruct> Lights { get; set; }
        public List<CameraStruct> Cameras { get; set; }
        public List<CameraLookupStruct> CameraLookup { get; set; }
        public List<RibbonEmitterStruct> RibbonEmitters { get; set; }
        public List<ParticleEmitterStruct> ParticleEmitters { get; set; }

        private byte[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="MD21"/> class.
        /// </summary>
        public MD21()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MD21"/> class.
        /// </summary>
        /// <param name="inData">ExtendedData.</param>
        public MD21(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            data = inData;
            using (var ms = new MemoryStream(inData))
            using (var br = new BinaryReader(ms))
            {
                if (br.ReadBinarySignature(false) != "MD20")
                    throw new Exception("Wrong M2 Header");

                Version = br.ReadUInt32();

                var lenModelname = br.ReadUInt32();
                var ofsModelname = br.ReadUInt32();
                Flags = (MD21Flags)br.ReadUInt32();
                var nSequences = br.ReadUInt32();
                var ofsSequences = br.ReadUInt32();
                var nAnimations = br.ReadUInt32();
                var ofsAnimations = br.ReadUInt32();
                var nAnimationLookup = br.ReadUInt32();
                var ofsAnimationLookup = br.ReadUInt32();
                var nBones = br.ReadUInt32();
                var ofsBones = br.ReadUInt32();
                var nKeyboneLookup = br.ReadUInt32();
                var ofsKeyboneLookup = br.ReadUInt32();
                var nVertices = br.ReadUInt32();
                var ofsVertices = br.ReadUInt32();
                ViewCount = br.ReadUInt32();
                var nColors = br.ReadUInt32();
                var ofsColors = br.ReadUInt32();
                var nTextures = br.ReadUInt32();
                var ofsTextures = br.ReadUInt32();
                var nTransparency = br.ReadUInt32();
                var ofsTransparency = br.ReadUInt32();
                var nUVAnimation = br.ReadUInt32();
                var ofsUVAnimation = br.ReadUInt32();
                var nTexReplace = br.ReadUInt32();
                var ofsTexReplace = br.ReadUInt32();
                var nRenderFlags = br.ReadUInt32();
                var ofsRenderFlags = br.ReadUInt32();
                var nBoneLookupTable = br.ReadUInt32();
                var ofsBoneLookupTable = br.ReadUInt32();
                var nTexLookup = br.ReadUInt32();
                var ofsTexLookup = br.ReadUInt32();
                var nUnk1 = br.ReadUInt32();
                var ofsUnk1 = br.ReadUInt32();
                var nTransLookup = br.ReadUInt32();
                var ofsTranslookup = br.ReadUInt32();
                var nUVAnimLookup = br.ReadUInt32();
                var ofsUVAnimLookup = br.ReadUInt32();
                BoundingBox = br.ReadBoundingBox(AxisConfiguration.Native);
                BoundingBoxRadius = br.ReadSingle();
                VertexBox = br.ReadBoundingBox(AxisConfiguration.Native);
                VertexBoxRadius = br.ReadSingle();
                var nBoundingTriangles = br.ReadUInt32();
                var ofsBoundingTriangles = br.ReadUInt32();
                var nBoundingVertices = br.ReadUInt32();
                var ofsBoundingVertices = br.ReadUInt32();
                var nBoundingNormals = br.ReadUInt32();
                var ofsBoundingNormals = br.ReadUInt32();
                var nAttachments = br.ReadUInt32();
                var ofsAttachments = br.ReadUInt32();
                var nAttachLookup = br.ReadUInt32();
                var ofsAttachLookup = br.ReadUInt32();
                var nEvents = br.ReadUInt32();
                var ofsEvents = br.ReadUInt32();
                var nLights = br.ReadUInt32();
                var ofsLights = br.ReadUInt32();
                var nCameras = br.ReadUInt32();
                var ofsCameras = br.ReadUInt32();
                var nCameraLookup = br.ReadUInt32();
                var ofsCameraLookup = br.ReadUInt32();
                var nRibbonEmitters = br.ReadUInt32();
                var ofsRibbonEmitters = br.ReadUInt32();
                var nParticleEmitters = br.ReadUInt32();
                var ofsParticleEmitters = br.ReadUInt32();

                // Model with flag 8 have extra field
                if (Flags.HasFlag(MD21Flags.UseTextureCombinerCombos))
                {
                    var nUnk2 = br.ReadUInt32();
                    var ofsUnk2 = br.ReadUInt32();
                }

                br.BaseStream.Position = ofsModelname;
                if (lenModelname > 0)
                {
                    Name = new string(br.ReadChars((int)lenModelname));
                    Name = Name.Remove(Name.Length - 1);
                }
                Sequences = ReadStructList<SequenceStruct>(nSequences, ofsSequences, br);
                Animations = ReadStructList<AnimationStruct>(nAnimations, ofsAnimations, br);
                AnimationLookups = ReadStructList<AnimationLookupStruct>(nAnimationLookup, ofsAnimationLookup, br);
                Bones = ReadStructList<BoneStruct>(nBones, ofsBones, br);
                KeyBoneLookup = ReadStructList<KeyBoneLookupStruct>(nKeyboneLookup, ofsKeyboneLookup, br);
                Vertices = ReadStructList<VerticeStruct>(nVertices, ofsVertices, br);
                Colors = ReadStructList<ColorStruct>(nColors, ofsColors, br);
                Textures = ReadTextures(nTextures, ofsTextures, br);
                Transparency = ReadStructList<TransparencyStruct>(nTransparency, ofsTransparency, br);
                UVAnimations = ReadStructList<UVAnimationStruct>(nUVAnimation, ofsUVAnimation, br);
                TextureReplace = ReadStructList<TextureReplaceStruct>(nTexReplace, ofsTexReplace, br);
                RenderFlags = ReadStructList<RenderFlagStruct>(nRenderFlags, ofsRenderFlags, br);
                BoneLookupTable = ReadStructList<BoneLookupTableStruct>(nBoneLookupTable, ofsBoneLookupTable, br);
                TextrueLookup = ReadStructList<TextureLookupStruct>(nTexLookup, ofsTexLookup, br);
                TransparencyLookup = ReadStructList<TransparencyLookupStruct>(nTransLookup, ofsTranslookup, br);
                UVAnimLookup = ReadStructList<UVAnimLookupStruct>(nUVAnimLookup, ofsUVAnimLookup, br);
                BoundingTriangles = ReadStructList<BoundingTriangleStruct>(nBoundingTriangles, ofsBoundingTriangles, br);
                BoundingVertices = ReadStructList<BoundingVertexStruct>(nBoundingVertices, ofsBoundingVertices, br);
                BoundingNormals = ReadStructList<BoundingNormalStruct>(nBoundingNormals, ofsBoundingNormals, br);
                Attachments = ReadStructList<AttachmentStruct>(nAttachments, ofsAttachments, br);
                AttachLookup = ReadStructList<AttachLookupStruct>(nAttachLookup, ofsAttachLookup, br);
                Events = ReadStructList<EventStruct>(nEvents, ofsEvents, br);
                Lights = ReadStructList<LightStruct>(nLights, ofsLights, br);
                Cameras = ReadStructList<CameraStruct>(nCameras, ofsCameras, br);
                CameraLookup = ReadStructList<CameraLookupStruct>(nCameraLookup, ofsCameraLookup, br);
                RibbonEmitters = ReadStructList<RibbonEmitterStruct>(nRibbonEmitters, ofsRibbonEmitters, br);
                ParticleEmitters = ReadStructList<ParticleEmitterStruct>(nParticleEmitters, ofsParticleEmitters, br);
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

        private List<TextureStruct> ReadTextures(uint count, uint offset, BinaryReader br)
        {
            br.BaseStream.Position = offset;
            var textures = new TextureStruct[count];

            for (var i = 0; i < count; i++)
            {
                textures[i].Type = (TextureType)br.ReadUInt32();
                textures[i].Flags = (TextureFlags)br.ReadUInt32();
                textures[i].Filename = "";

                var lenFilename = br.ReadUInt32();
                var ofsFilename = br.ReadUInt32();

                if (textures[i].Type == TextureType.None)
                {
                    if (ofsFilename >= 10)
                    {
                        var preFilenamePosition = br.BaseStream.Position; // probably a better way to do all this
                        br.BaseStream.Position = ofsFilename;
                        var filename = new string(br.ReadChars(int.Parse(lenFilename.ToString())));
                        filename = filename.Replace("\0", "");
                        if (!filename.Equals(""))
                        {
                            textures[i].Filename = filename;
                        }

                        br.BaseStream.Position = preFilenamePosition;
                    }
                }
            }

            return textures.ToList();
        }

        private List<AnimationStruct> ReadAnimations(uint nAnimations, uint ofsAnimations, BinaryReader br)
        {
            br.BaseStream.Position = ofsAnimations;
            Dictionary<ushort, AnimationStruct> animations = new Dictionary<ushort, AnimationStruct>();

            for (var i = 0; i < nAnimations; i++)
            {
                AnimationStruct animation = br.ReadStruct<AnimationStruct>();
                animations.TryAdd(animation.AnimationID, animation);
            }

            return animations.Values.ToList();
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public uint GetSize()
        {
            return (uint)Serialize().Length;
        }

        /// <summary>
        /// Serializes the current object into a byte array.
        /// WARNING: The serializer just write back the original MD21 content!
        /// </summary>
        /// <returns>The serialized object.</returns>
        public byte[] Serialize(long offset = 0)
        {
            return data;
        }
    }
}