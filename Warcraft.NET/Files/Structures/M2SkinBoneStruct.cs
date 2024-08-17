namespace Warcraft.NET.Files.Structures
{
    public struct M2SkinBoneStruct
    {
        /// <summary>
        /// Bone 1 Index into BoneLookupTable from M2. see wiki for exact calculation
        /// </summary>
        public byte Bone1ID;

        /// <summary>
        /// Bone 2 Index into BoneLookupTable from M2. see wiki for exact calculation
        /// </summary>
        public byte Bone2ID;

        /// <summary>
        /// Bone 3 Index into BoneLookupTable from M2. see wiki for exact calculation
        /// </summary>
        public byte Bone3ID;

        /// <summary>
        /// Bone 4 Index into BoneLookupTable from M2. see wiki for exact calculation
        /// </summary>
        public byte Bone4ID;
    }
}
