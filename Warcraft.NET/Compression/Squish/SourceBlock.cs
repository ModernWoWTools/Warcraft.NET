namespace Warcraft.NET.Compression.Squish
{
    public struct SourceBlock
    {
        public byte start;
        public byte end;
        public byte error;

        public SourceBlock(byte s, byte e, byte err)
        {
            start = s;
            end = e;
            error = err;
        }
    };
}
