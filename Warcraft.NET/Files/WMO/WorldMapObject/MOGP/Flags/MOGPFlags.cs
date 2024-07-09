using System;

namespace Warcraft.NET.Files.WMO.WorldMapObject.MOGP.Flags
{
    [Flags]
    public enum MOGPFlags2 : uint
    {
        /// <summary>
        /// Has portal palnes to cut terrain (available since MoP)
        /// </summary>
        CanCutTerrain = 0x1,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused_0x2 = 0x2,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused_0x4 = 0x4,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused_0x8 = 0x8,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused_0x10 = 0x10,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused_0x20 = 0x20,

        /// <summary>
        /// If this group is a split group parent. Added in Shadowlands.
        /// </summary>
        IsSplitGroupParent = 0x40,

        /// <summary>
        /// If this group is a split group child. Added in Shadowlands.
        /// </summary>
        IsSplitGroupChild = 0x80,

        /// <summary>
        /// Attachment mesh. Added in Shadowlands.
        /// </summary>
        AttachmentMesh = 0x100
    }

    /// <summary>
    /// Defines various flags for a model group.
    /// </summary>
    [Flags]
    public enum MOGPFlags : uint
    {
        /// <summary>
        /// The model contains a MOBN and MOBR chunk.
        /// </summary>
        HasBSP = 0x1,

        /// <summary>
        /// The model contains a MOLM and MOLD chunk.
        /// </summary>
        SubtractAmbientColour = 0x2,

        /// <summary>
        /// The model contains a MOCV chunk.
        /// </summary>
        HasVertexColours = 0x4,

        /// <summary>
        /// The model is outdoors.
        /// </summary>
        IsOutdoors = 0x8,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused1 = 0x10,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused2 = 0x20,

        /// <summary>
        /// The model uses exterior lighting instead of local diffuse lighting. Applies to doodads and water.
        /// </summary>
        DoNotUseLocalDiffuseLighting = 0x40,

        /// <summary>
        /// The model is unreachable.
        /// </summary>
        Unreachable = 0x80,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused3 = 0x100,

        /// <summary>
        /// The model has a MOLR chunk.
        /// </summary>
        HasLights = 0x200,

        /// <summary>
        /// Has MPBV, MPBP, MPBI, and MPBG chunks. In legion, this value has something do with LOD.
        /// </summary>
        HasMPBStuff = 0x400,

        /// <summary>
        /// The model has a MODR chunk.
        /// </summary>
        HasDoodads = 0x800,

        /// <summary>
        /// The model has an MLIQ chunk.
        /// </summary>
        HasLiquids = 0x1000,

        /// <summary>
        /// The model is indoors.
        /// </summary>
        IsIndoors = 0x2000,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused4 = 0x4000,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused5 = 0x8000,

        /// <summary>
        /// The model should always be drawn.
        /// </summary>
        AlwaysDrawEvenIfOutdoors = 0x10000,

        /// <summary>
        /// An unused value. In Cata+, this indicates that the model has MORI and MORB chunks.
        /// </summary>
        Unused6 = 0x20000,

        /// <summary>
        /// The model should show the skybox when a player is inside it.
        /// </summary>
        ShowSkybox = 0x40000,

        /// <summary>
        /// The model's water is ocean water.
        /// </summary>
        IsOceanicWater = 0x80000,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused7 = 0x100000,

        /// <summary>
        /// Indicates whether a player can mount inside the model.
        /// </summary>
        IsMountAllowed = 0x200000,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused8 = 0x400000,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused9 = 0x800000,

        /// <summary>
        /// Has two MOCV chunks.
        /// </summary>
        HasTwoVertexShadingSets = 0x1000000,

        /// <summary>
        /// Has two MOTV chunks.
        /// </summary>
        HasTwoTextureCoordinateSets = 0x2000000,

        /// <summary>
        /// Indicates that this group is an antiportal group, regardless of its name.
        /// </summary>
        IsAntiportal = 0x4000000,

        /// <summary>
        /// Related to <see cref="IsAntiportal"/>. Requires the same set of flags.
        /// </summary>
        UnknownOcclusionRelated = 0x8000000,

        /// <summary>
        /// An unused value.
        /// </summary>
        Unused10 = 0x10000000,

        /// <summary>
        /// Related to culling.
        /// </summary>
        ExteriorCulling = 0x20000000,

        /// <summary>
        /// Has three MOTV chunks.
        /// </summary>
        HasThreeTextureCoordinateSets = 0x40000000,

        /// <summary>
        /// Seen in world/wmo/kultiras/human/8hu_warfronts_armory_v2_000.wmo
        /// </summary>
        Unknown = 0x80000000
    }
}
