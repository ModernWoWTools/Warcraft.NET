using System;
using System.Numerics;
using Warcraft.NET.Files.Structures;

namespace Warcraft.NET.Files.M2.Entrys
{
    public struct SequenceStruct
    {
        public uint Timestamp;
    }

    public struct AnimationStruct
    {
        public ushort AnimationID;
        public ushort SubAnimationID;
        public uint Length;
        public float MovingSpeed;
        public AnimFlags Flags;
        public short Probability;
        public ushort Unused;
        public uint Unknown1;
        public uint Unknown2;
        public uint PlaybackSpeed;
        public BoundingBox BoundingBox;
        public float BoundingBoxRadius;
        public short NextAnimation;
        public ushort Index;
    }

    public struct AnimationLookupStruct
    {
        public ushort AnimationID;
    }

    public struct BoneStruct
    {
        public int BoneId;
        public uint Flags;
        public short ParentBone;
        private ushort Unknown0;
        private ushort Unknown1;
        private ushort Unknown2;
        public ABlock<Vector3> Translation;
        public ABlock<Quaternion> Rotation;
        public ABlock<Vector3> Scale;
        public Vector3 Pivot;
    }

    public struct KeyBoneLookupStruct
    {
        public ushort Bone;
    }

    public struct VerticeStruct
    {
        public Vector3 Position;
        public byte BoneWeight0;
        public byte BoneWeight1;
        public byte BoneWeight2;
        public byte BoneWeight3;
        public byte BoneIndices0;
        public byte BoneIndices1;
        public byte BoneIndices2;
        public byte BoneIndices3;
        public Vector3 Normal;
        public float TextureCoordX;
        public float TextureCoordY;
        public float TextureCoordX2;
        public float TextureCoordY2;
    }

    public struct ColorStruct
    {
        public ABlock<RGB> Color;
        public ABlock<short> Alpha;
    }

    public struct TextureStruct
    {
        public TextureType Type;
        public TextureFlags Flags;
        public string Filename;
    }

    public struct TransparencyStruct
    {
        public ABlock<short> Alpha;
    }

    public struct UVAnimationStruct
    {
        public ABlock<Vector3> Translation;
        public ABlock<Quaternion> Rotation;
        public ABlock<Vector3> Scaling;
    }

    public struct TextureReplaceStruct
    {
        public short TextureID;
    }

    public struct RenderFlagStruct
    {
        public ushort Flags;
        public ushort BlendingMode;
    }

    public struct BoneLookupTableStruct
    {
        public ushort Bone;
    }

    public struct TextureLookupStruct
    {
        public ushort TextureID;
    }

    public struct TransparencyLookupStruct
    {
        public ushort TransparencyID;
    }

    public struct UVAnimLookupStruct
    {
        public ushort AnimatedTextureID;
    }

    public struct BoundingTriangleStruct
    {
        public ushort Index0;
        public ushort Index1;
        public ushort Index2;
    }

    public struct BoundingVertexStruct
    {
        public Vector3 Vertex;
    }

    public struct BoundingNormalStruct
    {
        public Vector3 Normal;
    }

    public struct AttachmentStruct
    {
        public uint Id;
        public uint Bone;
        public Vector3 Position;
        public ABlock<int> Data;
    }

    public struct AttachLookupStruct
    {
        public ushort Attachment;
    }

    public struct EventStruct
    {
        public char Identifier0;
        public char Identifier1;
        public char Identifier2;
        public char Identifier3;
        public uint Data;
        public uint Bone;
        public Vector3 Position;
        public ushort InterpolationType;
        public ushort GlobalSequence;
        public uint TimestampEntryCount;
        public uint TimestampListOffset;
    }

    public struct LightStruct
    {
        public short Type;
        public short Bone;
        public Vector3 Position;
        public ABlock<RGB> AmbientColor;
        public ABlock<float> AmbientIntensity;
        public ABlock<RGB> DiffuseColor;
        public ABlock<float> DiffuseIntensity;
        public ABlock<int> AttenuationStart;
        public ABlock<int> AttenuationEnd;
        public ABlock<int> Unknown;
    }

    public struct CameraStruct
    {
        public uint Type;
        public float FarClipping;
        public float NearClipping;
        public ABlock<CameraPositionStruct> TranslationPos;
        public Vector3 Position;
        public ABlock<CameraPositionStruct> TranslationTar;
        public Vector3 Target;
        public ABlock<Vector3> Scaling;
        public ABlock<float> UnknownABlock;
    }

    public struct CameraLookupStruct
    {
        public ushort CameraID;
    }

    public struct CameraPositionStruct
    {
        public float CameraPos0;
        public float CameraPos1;
        public float CameraPos2;
        public float CameraPos3;
        public float CameraPos4;
        public float CameraPos5;
        public float CameraPos6;
        public float CameraPos7;
        public float CameraPos8;
    }

    public struct RibbonEmitterStruct
    {
        public uint Unknown1;
        public uint BoneID;
        public Vector3 Position;
        public int TextureCount;
        public int TextureOffset;
        public int BlendRefCount;
        public int BlendRefOffset;
        public ABlock<RGB> Color;
        public ABlock<short> Opacity;
        public ABlock<int> Above;
        public ABlock<int> Below;
        public float Resolution;
        public float Length;
        public float EmissionAngle;
        public short RenderFlags;
        public ABlock<short> UnknownABlock;
        public ABlock<bool> UnknownABlock2;
        public int Unknown2;
    }

    public struct ParticleEmitterStruct
    {
        // needs filling in
    }

    [Flags]
    public enum AnimFlags : uint
    {
        Unknown1 = 0x1,
        Unknown2 = 0x2,
        Unknown4 = 0x4,
        Unknown8 = 0x8,
        Unknown10 = 0x10,
        Unknown20 = 0x20,
        Unknown40 = 0x40,
        Unknown80 = 0x80,
        Unknown100 = 0x100,
        Unknown200 = 0x200,
        Unknown400 = 0x400,
        Unknown800 = 0x800,
    }

    public enum TextureType : uint
    {
        None = 0,                     // NONE -- Texture given in filename
        ComponentSkin = 1,            // TEX_COMPONENT_SKIN -- Skin -- Body + clothes
        ComponentObjectSkin = 2,      // TEX_COMPONENT_OBJECT_SKIN -- Object Skin -- Item, Capes ("Item\ObjectComponents\Cape\*.blp")
        ComponentWeaponBlade = 3,     // TEX_COMPONENT_WEAPON_BLADE -- Weapon Blade -- Used on several models but not used in the client as far as I see. Armor Reflect?
        ComponentWeaponHandle = 4,    // TEX_COMPONENT_WEAPON_HANDLE -- Weapon Handle
        ComponentEnvironment = 5,     // TEX_COMPONENT_ENVIRONMENT -- (OBSOLETE) Environment (Please remove from source art)
        ComponentCharHair = 6,        // TEX_COMPONENT_CHAR_HAIR -- Character Hair
        ComponentSkinFacialHair = 7,  // TEX_COMPONENT_CHAR_FACIAL_HAIR -- (OBSOLETE) Character Facial Hair (Please remove from source art)
        ComponentSkinExtra = 8,       // TEX_COMPONENT_SKIN_EXTRA -- Skin Extra
        ComponentUiSkin = 9,          // TEX_COMPONENT_UI_SKIN -- UI Skin -- Used on inventory art M2s (1): inventoryartgeometry.m2 and inventoryartgeometryold.m2
        ComponentTaurenMane = 10,     // TEX_COMPONENT_TAUREN_MANE -- Character Misc // (OBSOLETE) Tauren Mane (Please remove from source art)
        ComponentMonster1 = 11,       // TEX_COMPONENT_MONSTER_1 -- Monster Skin 1 -- Skin for creatures or gameobjects #1
        ComponentMonster2 = 12,       // TEX_COMPONENT_MONSTER_2 -- Monster Skin 2 -- Skin for creatures or gameobjects #2
        ComponentMonster3 = 13,       // TEX_COMPONENT_MONSTER_3 -- Monster Skin 3 -- Skin for creatures or gameobjects #3
        ComponentItemIcon = 14,       // TEX_COMPONENT_ITEM_ICON -- Item Icon -- Used on inventory art M2s (2): ui-button.m2 and forcedbackpackitem.m2 (CSimpleModel_ReplaceIconTexture("texture"))
        GuildBackgroundColor = 15,    // (Cata+) Guild Background Color
        GuildEmblemColor = 16,        // (Cata+) Guild Emblem Color
        GuildBorderColor = 17,        // (Cata+) Guild Border Color
        GuildEmblem = 18,             // (Cata+) Guild Emblem
        CharacterEyes = 19,           // (SL+) Character Eyes
        CharacterAccessory = 20,      // (SL+) Character Jewelry // Accessory
        CharacterSecondarySkin = 21,  // (SL+) Character Secondary Skin
        CharacterSecondaryHair = 22,  // (SL+) Character Secondary Hair
        CharacterSecondaryArmor = 23, // (SL+) Character Secondary Armor
        Unk0 = 24,                    // (SL+)
        Unk1 = 25,                    // (DF+) Seen in DracthyrDragon.m2
        Unk2 = 26,                    // (DF+) Seen in DracthyrDragon.m2
    }                                 

    [Flags]
    public enum TextureFlags
    {
        Flag_0x1_WrapX = 0x1,
        Flag_0x2_WrapY = 0x2,
    }
}
